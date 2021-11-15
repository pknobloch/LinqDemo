using System.Collections;

namespace LinqDemo;

// Reference: The Evolution Of LINQ And Its Impact On The Design Of C# http://msdn.microsoft.com/en-us/magazine/cc163400.aspx
public class WhatWillWeSee
{
    /// <summary>
    /// The way grandpa used to write code.
    /// </summary>
    public List<Account> Find_Negative_Balances_Old(
        List<Account> accounts)
    {
        var results = new List<Account>();

        foreach (Account account in accounts)
        {
            if (account.Balance < 0)
            {
                results.Add(account);
            }
        }

        return results;
    }

    public IEnumerable Find_Negative_Balances_New(
        IEnumerable<Account> accounts)
    {
        return from account in accounts
            where account.Balance < 0
            select new { account.Name, account.Address };
    }
}