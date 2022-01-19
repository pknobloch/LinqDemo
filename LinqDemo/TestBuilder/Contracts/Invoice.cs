namespace LinqDemo.TestBuilder.Contracts;

public class Invoice
{
    public Recipient Recipient
    {
        get;
        // The setter is not meant to be here but I wanted the example to compile.
        set;
    }
    public IReadOnlyCollection<InvoiceLine> Lines { get; }
 
    public Invoice(Recipient recipient, IReadOnlyCollection<InvoiceLine> lines)
    {
        if (recipient == null)
            throw new ArgumentNullException(nameof(recipient));
        if (lines == null)
            throw new ArgumentNullException(nameof(lines));
 
        Recipient = recipient;
        Lines = lines;
    }
 
    public Invoice WithRecipient(Recipient newRecipient)
    {
        return new Invoice(newRecipient, Lines);
    }
 
    public Invoice WithLines(IReadOnlyCollection<InvoiceLine> newLines)
    {
        return new Invoice(Recipient, newLines);
    }
 
    public override bool Equals(object obj)
    {
        var other = obj as Invoice;
        if (other == null)
            return base.Equals(obj);
 
        return Equals(Recipient, other.Recipient)
                && Enumerable.SequenceEqual(
                        Lines.OrderBy(l => l.Name),
                        other.Lines.OrderBy(l => l.Name));
    }
 
    public override int GetHashCode()
    {
        return
                Recipient.GetHashCode() ^
                Lines.GetHashCode();
    }
}