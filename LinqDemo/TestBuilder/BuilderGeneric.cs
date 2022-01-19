namespace LinqDemo.TestBuilder;

/*
 As an alternative to replicating the Test Data Builder pattern exactly, 
 you can define a single generically typed Builder class.
 
 The Builder<T> class reduces the Test Data Builder design patterns to the essentials:

    * A constructor that initialises the Builder with default data.
    * A single fluent interface Select method, which returns a new Builder object.
    * A Build method, which returns the built object.

Note the Select method.
 */
public class Builder<T>
{
    private readonly T _item;
 
    public Builder(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
 
        _item = item;
    }
 
    public Builder<T1> Select<T1>(Func<T, T1> f)
    {
        var newItem = f(_item);
        return new Builder<T1>(newItem);
    }
 
    public T Build()
    {
        return _item;
    }
 
    public override bool Equals(object obj)
    {
        var other = obj as Builder<T>;
        if (other == null)
            return base.Equals(obj);
 
        return Equals(_item, other._item);
    }
 
    public override int GetHashCode()
    {
        return _item.GetHashCode();
    }
    
    // Abracadabra!
    public static implicit operator T(Builder<T> b)
    {
        return b._item;
    }
}