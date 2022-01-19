namespace LinqDemo.TestBuilder.Contracts;

public class InvoiceLine
{
    public string Name { get; }
    public PoundsShillingsPence Amount { get; }

    public InvoiceLine(string name, PoundsShillingsPence amount)
    {
        Name = name;
        Amount = amount;
    }
}