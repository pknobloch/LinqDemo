using System.Collections;

namespace LinqDemo;

public class CustomerTuple
{
    public string? Name { get; set; }
    public string? Address { get; set; }


    public CustomerTuple()
    {
    }
    public CustomerTuple(string? name, string? address)
    {
        Name = name;
        Address = address;
    }
}

public class AnonymousTypes
{

    public IEnumerable<string> Find_Negative_Balances_Customer_Names(
        IEnumerable<Account> accounts)
    {
        return accounts
            .Where((Account account) => account.Balance < 0)
            .Select((Account account) => account.Name);
    }

    public IEnumerable<CustomerTuple> Find_Positive_Balances_CustomerTuples(
        IEnumerable<Account> accounts)
    {
        return accounts
            .Where((Account account) => account.Balance > 0)
            .Select((Account account) => new CustomerTuple(account.Name, account.Address));
    }

    public IEnumerable Find_Zero_Balances_Linq_Customer_Details_With_Anonymous_Type(
        IEnumerable<Account> accounts)
    {
        return accounts
            .Where((Account account) => account.Balance == 0)
            .Select((Account account) => new { account.Name, account.Address });
    }

    public IEnumerable Find_Negative_Balances_Linq_Customer_Details_With_Anonymous_Type_And_Explicit_Names(
        IEnumerable<Account> accounts)
    {
        var query = 
            accounts
            .Where((Account account) => account.Balance < 0)
            .Select((Account account) => new { FullName = account.Name, HomeAddress = account.Address });

        foreach (var customer in query)
        {
            Console.WriteLine(customer.FullName + customer.HomeAddress);
        }

        return query;
    }
}