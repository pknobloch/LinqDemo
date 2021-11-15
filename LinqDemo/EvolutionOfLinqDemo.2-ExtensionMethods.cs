namespace LinqDemo;

public static class AccountEnumerableExtensions
{
    //Please note the "static" modifier and the keyword "this".
    public static IEnumerable<Account> AccountFilterExtension(
        this IEnumerable<Account> accounts,
        FilterAccountDelegate filter)
    {
        return LambdaExpressions.Filter_Accounts(accounts, filter);
    }
}

public class ExtensionMethods
{
    /// <summary>
    /// Filter using a delegate and extension
    /// </summary>
    public IEnumerable<Account> Find_Negative_Balances_Anonymous_Delegate(
        IEnumerable<Account> accounts)
    {
        return accounts.AccountFilterExtension(
            delegate(Account account) { return account.Balance < 0; });
    }

    /// <summary>
    /// Filter using a lambda expression
    /// </summary>
    public IEnumerable<Account> Find_Positive_Balances_Lambda(
        IEnumerable<Account> accounts)
    {
        return accounts.AccountFilterExtension(
            (Account account) => { return account.Balance > 0; });
    }

    public IEnumerable<Account> Find_Zero_Balances_Lambda_Simpler(
        IEnumerable<Account> accounts)
    {
        return accounts.Where((Account account) => account.Balance == 0);
    }
}