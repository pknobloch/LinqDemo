using LinqDemo.TestBuilder.Contracts;

namespace LinqDemo.TestBuilder.Builders;

public class RecipientBuilder
{
    private string name;
    private Address address;
 
    public RecipientBuilder()
    {
        this.name = "";
        this.address = new AddressBuilder().Build();
    }
 
    public RecipientBuilder WithName(string newName)
    {
        this.name = newName;
        return this;
    }
 
    public RecipientBuilder WithAddress(Address newAddress)
    {
        this.address = newAddress;
        return this;
    }
 
    public Recipient Build()
    {
        return new Recipient(this.name, this.address);
    }
}