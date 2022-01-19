using LinqDemo.TestBuilder.Contracts;

namespace LinqDemo.TestBuilder.Builders;

public class InvoiceLineBuilder
{
    private string name;
    private PoundsShillingsPence amount;
    public InvoiceLineBuilder()
    {
        this.name = "";
        this.amount = PoundsShillingsPence.Zero;
    }
 
    public InvoiceLineBuilder WithName(string newName)
    {
        this.name = newName;
        return this;
    }
 
    public InvoiceLineBuilder WithAmount(PoundsShillingsPence newAmount)
    {
        this.amount = newAmount;
        return this;
    }
 
    public InvoiceLine Build()
    {
        return new InvoiceLine(this.name, this.amount);
    }
}