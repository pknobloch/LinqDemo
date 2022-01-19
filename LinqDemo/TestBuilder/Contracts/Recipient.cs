namespace LinqDemo.TestBuilder.Contracts;

public class Recipient
{
    public string Name { get; }

    public Address Address
    {
        get;
        // The setter is not meant to be here but I wanted the example to compile.
        set;
    }

    public Recipient(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public Recipient WithAddress(Address address)
    {
        return new Recipient(Name, address);
    }
}