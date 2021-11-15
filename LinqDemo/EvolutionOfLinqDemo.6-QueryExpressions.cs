namespace LinqDemo;

public class QueryExpressions
{
    public void And_Finally_____Query_Expressions(
        IEnumerable<Account> accounts)
    {
        var brokeCustomers = 
            from account in accounts
            where account.Balance < 0
            select new { Name = account.Name, Address = account.Address };
    }
}