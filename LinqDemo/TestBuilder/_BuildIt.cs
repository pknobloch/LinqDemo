using LinqDemo.TestBuilder.Builders;
using LinqDemo.TestBuilder.Contracts;

namespace LinqDemo.TestBuilder;

public partial class BuildIt
{
    public void Why_you_might_use_it()
    {
        // From https://blog.ploeh.dk/2017/08/15/test-data-builders-in-c/

        // The purpose of a Test Data Builder is to make it easy to
        // create input data (or objects) for unit tests.

        var address = new AddressBuilder().WithCity("Paris").Build();

        /* Given that AddressBuilder is more complicated than Address itself,
         * the benefit of the pattern may seem obscure, but one of the benefits
         * is that Test Data Builders easily compose.
         */
        var invoice = new InvoiceBuilder()
                .WithRecipient(new RecipientBuilder()
                    .WithAddress(new AddressBuilder()
                        .WithNoPostcode()
                        .Build())
                    .Build())
                .Build();

        // But the alternative looks worse! The important parts are hard to spot.

        var invoiceTheHardWay = new Invoice(
            new Recipient("Sherlock Holmes",
                new Address("221b Baker Street", 
                            "London",
                            new PostCode())),
            new List<InvoiceLine>
            {
                new InvoiceLine("Deerstalker Hat",
                    new PoundsShillingsPence(0, 3, 10)),
                new InvoiceLine("Tweed Cape",
                    new PoundsShillingsPence(0, 4, 12))
            });
    }

    public void Let_us_build_something()
    {
        // Don't mind the compile error, please.
        var address = Build.Address().Select(a =>
        {
            a.City = "Paris";
            return a;
        }).Build();
    }
}

/* Contrary to Builder<T>, which is a reusable, general-purpose class,
 * the static Build class is an example of a collection of Test Utility Methods
 * specific to the domain model you're testing. Notice how the Build.Address() method
 * uses Build.PostCode().Build() to create a default value for the
 * initial Address object's post code. 
 */
public static class Build
{
    public static Builder<PostCode> PostCode()
    {
        return new Builder<PostCode>(new PostCode());
    }

    public static Builder<Recipient> Recipient()
    {
        return new Builder<Recipient>(new Recipient("", Address().Build()));
    }

    public static Builder<Address> Address()
    {
        return new Builder<Address>(new Address("", "", PostCode().Build()));
    }

    public static Builder<Invoice> Invoice()
    {
        return new Builder<Invoice>(new Invoice(Recipient().Build(), new List<InvoiceLine>()));
    }
}

public partial class BuildIt
{
    public void This_looks_awkward()
    {
        // And its hard to read too.

        var invoice = Build.Invoice().Select(i =>
        {
            i.Recipient = Build.Recipient().Select(r =>
            {
                r.Address = Build.Address().WithNoPostCode().Build();
                return r;
            }).Build();
            return i;
        }).Build();
    }

    public void Lets_see_how_to_improve()
    {
        /* Notice that this example looks a little better than the previous examples.
         * Instead of having to supply a C# code block, with return statement and all,
         * this call to Select passes a proper (lambda) expression. 
         */

        var address = Build.PostCode()
                .Select(pc => new Address("Rue Morgue", "Paris", pc))
                .Build();
    }

    public void Named_extension_methods_are_bae()
    {
        var address = Build.Address().Select(a => a.WithCity("Paris")).Build();

        // But it's not webscale.

        var invoice =
            Build.Invoice()
                .Select(i => i
                    .WithRecipient(Build.Recipient()
                        .Select(r => r
                            .WithAddress(
                                Build.Address()
                                    .WithNoPostCode()
                                    .Build()))
                    .Build()))
                .Build();
    }

    public void Default_Builders_as_values()
    {
        /* To be clear: such a static Builder class is a Test Utility API
         * specific to your unit tests.
         * It would often be defined in a completely different file
         * than the Builder<T> class, perhaps even in separate libraries.
         */
        var address = Builder.Address.Select(a => a.WithCity("Paris")).Build();
    }

    public void Query_syntax()
    {
        Address address = from a in Builder.Address
                            select a.WithCity("Paris");
        
        /* There is some magic going on above.
         * It has to do with the generic builder...
         */
    }

    public void Some_better_examples()
    {
        Address address =
                from pc in Builder.PostCode
                select new Address("Rue Morgue", "Paris", pc);
        
        Invoice invoice =
            from i in Builder.Invoice
            select i.WithRecipient(
                from r in Builder.Recipient
                select r.WithAddress(Builder.Address.WithNoPostCode()));
    }

    // [Fact]
    public void Builder_Obeys_First_Functor_Law()
    {
        Func<int, int> id = x => x;
        var sut = new Builder<int>(42);
 
        var actual = sut.Select(id);
 
        // Assert.Equal(sut, actual);
    }
    
    // [Fact]
    public void Builder_Obeys_Second_Functor_Law()
    {
        Func<int, string> g = i => i.ToString();
        Func<string, string> f = s => new string(s.Reverse().ToArray());
        var sut = new Builder<int>(1337);
 
        var actual = sut.Select(i => f(g(i)));
 
        var expected = sut.Select(g).Select(f);
        // Assert.Equal(expected, actual);
    }

    public void Use_Natural_for_default_testing_purposes()
    {
        var parisAddress = Natural.Address.WithCity("Paris");
        
        var address = new Address("Rue Morgue", "Paris", Natural.PostCode);
    }

    public void Conclusion()
    {
        /* From https://blog.ploeh.dk/2017/09/11/test-data-without-builders/
         
           Using a fluent domain model obviates the need for Test Data Builders.
           There's a tendency among functional programmers to overbearingly state that
           design patterns are nothing but recipes to overcome deficiencies in
           particular programming languages or paradigms.
           If you believe such a claim, at least it ought to go both ways,
           I hope I've been able to demonstrate that this is true for the Test Data Builder pattern.
           You only need it for 'classic', mutable, object-oriented domain models.

            1. For mutable object models, use Test Data Builders.
            2. Consider, however, modelling your domain with Value Objects and
                 'copy and update' instance methods.
            3. Even better, consider using a programming language with built-in
                 'copy and update' expressions.

            If you're stuck with a language like C# or Java, you don't get language-level support 
            for 'copy and update' expressions. This means that you'll still need to incur the cost
            of adding and maintaining all those With[...] methods.
             
            That may seem like quite a maintenance burden (and it is), but consider that
            it has the same degree of complexity and overhead as defining a Test Data Builder
            for each domain object.
            At least, by putting this extra code in your domain model, you make all of that API
            (all the With[...] methods, and the structural equality) available to other production code.
            In my experience, that's a better return of investment than isolating 
            such useful features only to test code. 
         */
    }
}