using LinqDemo.TestBuilder.Contracts;

namespace LinqDemo.TestBuilder;

public static class Builder
{
    public readonly static Builder<Address> Address;
    public readonly static Builder<Invoice> Invoice;
    public readonly static Builder<InvoiceLine> InvoiceLine;
    public readonly static Builder<PostCode> PostCode;
    public readonly static Builder<PoundsShillingsPence> PoundsShillingsPence;
    public readonly static Builder<Recipient> Recipient;

    static Builder()
    {
        PoundsShillingsPence = new Builder<PoundsShillingsPence>(
                Contracts.PoundsShillingsPence.Zero);
        PostCode = new Builder<PostCode>(new PostCode());
        Address =
                new Builder<Address>(new Address("", "", PostCode.Build()));
        Recipient =
                new Builder<Recipient>(new Recipient("", Address.Build()));
        Invoice = new Builder<Invoice>(
                new Invoice(Recipient.Build(), new List<InvoiceLine>()));
        InvoiceLine = new Builder<InvoiceLine>(
                new InvoiceLine("", PoundsShillingsPence.Build()));
    }

    public static Builder<Address> WithNoPostCode(this Builder<Address> b)
    {
        return b.Select(a => a.WithPostCode(new PostCode()));
    }
}