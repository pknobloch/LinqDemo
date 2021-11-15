namespace LinqDemo;

public class ImplicitlyTypedLocalVariables
{

    public void Implicit_Example(IEnumerable<Account> accounts)
    {
        var integer = 1;
        //integer = "hello"; //compiler error

        // Nifty, right?
        var customerListLookup = new Dictionary<string, List<CustomerTuple>>();

        // Notice that we don't specify the type of account.
        var query = 
            accounts
            .Where(account => account.Balance < 0)
            .Select(account => new { account.Name, account.Address });
    }
}
