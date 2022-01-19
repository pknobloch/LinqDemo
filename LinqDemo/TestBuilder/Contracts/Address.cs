namespace LinqDemo.TestBuilder.Contracts;

public class Address
{
    public string Street { get; }
    public string City {
        get; 
        // The setter is not meant to be here but I wanted the example to compile.
        set;
    }
    public PostCode PostCode { get; }

    public Address(string street, string city, PostCode postCode)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
        PostCode = postCode ?? throw new ArgumentNullException(nameof(postCode));
    }

    public Address WithStreet(string newStreet)
    {
        return new Address(newStreet, City, PostCode);
    }

    public Address WithCity(string newCity)
    {
        return new Address(Street, newCity, PostCode);
    }

    public Address WithPostCode(PostCode newPostCode)
    {
        return new Address(Street, City, newPostCode);
    }

    public override bool Equals(object? obj)
    {
        var other = obj as Address;
        if (other == null)
                // ReSharper disable once BaseObjectEqualsIsObjectEquals
            return base.Equals(obj);

        return object.Equals(Street, other.Street)
                && object.Equals(City, other.City)
                && object.Equals(PostCode, other.PostCode);
    }

    public override int GetHashCode()
    {
        return
                Street.GetHashCode() ^
                City.GetHashCode() ^
                PostCode.GetHashCode();
    }
}