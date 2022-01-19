using LinqDemo.TestBuilder.Contracts;

namespace LinqDemo.TestBuilder;

/* This static Natural class is a test-specific container of 'good' default values.
 * Notice how, once more, the Address value uses the PostCode value to fill in the
 * PostCode property of the default Address value.
 */
public static class Natural
{
    public static PostCode PostCode = new PostCode();
    public static Address Address = new Address("", "", PostCode);
    public static Recipient Recipient = new Recipient("", Address);
    public static InvoiceLine InvoiceLine = new InvoiceLine("", PoundsShillingsPence.Zero);
    public static Invoice Invoice = new Invoice(Recipient, new InvoiceLine[0]);
}