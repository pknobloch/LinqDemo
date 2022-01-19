using LinqDemo.TestBuilder.Contracts;

namespace LinqDemo.TestBuilder.Builders;

public class InvoiceBuilder
{
    private Recipient recipient;
    private IReadOnlyCollection<InvoiceLine> lines;
 
    public InvoiceBuilder()
    {
        this.recipient = new RecipientBuilder().Build();
        this.lines = new List<InvoiceLine> { new InvoiceLineBuilder().Build() };
    }
 
    public InvoiceBuilder WithRecipient(Recipient newRecipient)
    {
        this.recipient = newRecipient;
        return this;
    }
 
    public InvoiceBuilder WithInvoiceLines(
            IReadOnlyCollection<InvoiceLine> newLines)
    {
        this.lines = newLines;
        return this;
    }
 
    public Invoice Build()
    {
        return new Invoice(recipient, lines);
    }
}