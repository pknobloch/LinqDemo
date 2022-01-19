using LinqDemo.TestBuilder.Contracts;

namespace LinqDemo.TestBuilder.Builders;

public class AddressBuilder
{
    private string _street;
    private string _city;
    private PostCode _postCode;
 
    public AddressBuilder()
    {
        _street = "";
        _city = "";
        _postCode = new PostCodeBuilder().Build();
    }
 
    public AddressBuilder WithStreet(string newStreet)
    {
        _street = newStreet;
        return this;
    }
 
    public AddressBuilder WithCity(string newCity)
    {
        _city = newCity;
        return this;
    }
 
    public AddressBuilder WithPostCode(PostCode newPostCode)
    {
        _postCode = newPostCode;
        return this;
    }
 
    public AddressBuilder WithNoPostcode()
    {
        _postCode = new PostCode();
        return this;
    }
 
    public Address Build()
    {
        return new Address(_street, _city, _postCode);
    }
}