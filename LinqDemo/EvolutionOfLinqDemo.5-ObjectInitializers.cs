namespace LinqDemo;

public class ObjectInitializers
{
    public IEnumerable<CustomerTuple> Object_Initializers_Example(
        IEnumerable<Account> accounts)
    {
        var customerLongWay = new CustomerTuple();
        customerLongWay.Name = "Roger";
        customerLongWay.Address = "1 Wilco Way";

        var customerEasyWay = new CustomerTuple { Name = "Roger", Address = "1 Wilco Way" };

        var brokeCustomers = 
            accounts
            .Where(account => account.Balance < 0)
            .Select(account => new CustomerTuple { Name = account.Name, Address = account.Address });

        return brokeCustomers;
    }
}