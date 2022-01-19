namespace LinqDemo.TestBuilder.Contracts;

public class PoundsShillingsPence
{
    public int Pounds { get; }
    public int Shillings { get; }
    public int Pence { get; }

    public PoundsShillingsPence(int pounds, int shillings, int pence)
    {
        Pounds = pounds;
        Shillings = shillings;
        Pence = pence;
    }

    public static PoundsShillingsPence Zero { get; }
}