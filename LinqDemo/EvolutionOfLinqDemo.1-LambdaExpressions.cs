namespace LinqDemo;

public delegate bool FilterAccountDelegate(Account account);

public class LambdaExpressions
{
    /// <summary>
    /// Note the second parameter which is a delegate.
    /// </summary>
    public static IEnumerable<Account> Filter_Accounts(
        IEnumerable<Account> accounts,
        FilterAccountDelegate filter)
    {
        var results = new List<Account>();

        foreach (Account account in accounts)
        {
            if (filter(account))
            {
                results.Add(account);
            }
        }

        return results;
    }

    public IEnumerable<Account> Find_Positive_Balances_Using_Delegate(
        IEnumerable<Account> accounts)
    {
        return Filter_Accounts(
            accounts,
            delegate(Account account) { return account.Balance > 0; });
    }

    public IEnumerable<Account> Find_Negative_Balances_Using_Lambda(
        IEnumerable<Account> accounts)
    {
        return Filter_Accounts(
            accounts,
            (Account account) => { return account.Balance < 0; });
    }

    public IEnumerable<Account> Find_Zero_Balances_Using_Lambda_Simplified(
        IEnumerable<Account> accounts)
    {
        return Filter_Accounts(
            accounts,
            (Account account) => account.Balance == 0);
    }
}