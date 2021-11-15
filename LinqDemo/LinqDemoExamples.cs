using System.Collections;

namespace LinqDemo;

//Reference: Deferred execution sample: http://jesseliberty.com/2011/05/16/linq-deferred-executionoops/
//Reference: 101 Linq Samples: http://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b

public class DeferredExecution
{
    public void IterateThroughAList()
    {
        int counter = 0;
        var evenNumbersInSeriesList =
            Enumerable.Range(0, 10).Select(
                current =>
                {
                    int result = current + counter;
                    counter++;
                    return result;
                }
            ).ToList();

        // List the numbers in the series.
        Console.WriteLine("First Try:");
        foreach (int i in evenNumbersInSeriesList)
        {
            Console.WriteLine(i);
        }

        // List them again.
        Console.WriteLine("Second Try:");
        foreach (int i in evenNumbersInSeriesList)
        {
            Console.WriteLine(i);
        }
    }

    public void IterateThroughAnEnumerable()
    {
        int counter = 0;
        var evenNumbersInSeries =
            Enumerable.Range(0, 10).Select(
                current =>
                {
                    int result = current + counter;
                    counter++;
                    return result;
                }
            );

        // List the numbers in the series.
        Console.WriteLine("First Try:");
        foreach (int i in evenNumbersInSeries)
        {
            Console.WriteLine(i);
        }

        // List them again.
        Console.WriteLine("Second Try:");
        foreach (int i in evenNumbersInSeries)
        {
            Console.WriteLine(i);
        }
    }

    public enum AccountType
    {
        Personal,
        Business
    }

    public void Useful_Deferred_Execution_Example(IEnumerable<Account> accounts, AccountType accountType)
    {
        // Does NOT execute here.
        var query =
            from account in accounts
            where account.Balance < 0
            select account;

        // Still not executing.
        if (accountType == AccountType.Personal)
        {
            query = query.Where(account => account.Name.StartsWith("A"));
        }

        // Here is when we actually execute.
        foreach (var account in query)
        {
            Console.WriteLine(account.Name);
        }
    }

    public IEnumerable Samples101_Anonymous_Types()
    {
        string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

        var upperLowerWords =
            from word in words
            select new { Upper = word.ToUpper(), Lower = word.ToLower() };

        foreach (var pair in upperLowerWords)
        {
            Console.WriteLine("Uppercase: {0}, Lowercase: {1}", pair.Upper, pair.Lower);
        }

        return upperLowerWords;
    }

    public void Test_Anonymous_Return()
    {
        // NOTE: trying to access columns does not work.
        var output = Samples101_Anonymous_Types();
        foreach (var obj in output)
        {
            // Uncomment here and try to auto-complete.
            // Console.WriteLine(obj);
        }
    }

    public void Samples101_Skip_Take()
    {
        var range = Enumerable.Range(1, 20);

        Console.WriteLine("First range:");
        // Should output 6 to 10.
        foreach (var number in range
                     .Skip(5)
                     .Take(5))
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("Now with new index syntax:");
        // Should also output 6 to 10.
        foreach (var number in range
                     .Take(5..^10))
        {
            Console.WriteLine(number);
        }
    }

    public void Samples101_ThenBy()
    {
        string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var sortedDigits =
            from digit in digits
            orderby digit.Length, digit
            select digit;

        var anotherWayOfDoingThis =
            digits
                .OrderBy(d => d.Length)
                .ThenBy(d => d);

        Console.WriteLine("Sorted digits:");
        foreach (var digit in sortedDigits)
        {
            Console.WriteLine(digit);
        }
    }

    public void Samples101_GroupBy()
    {
        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        var numberGroups =
            from number in numbers
            group number by number % 5
            into grouping
            select new { Remainder = grouping.Key, Numbers = grouping };

        foreach (var group in numberGroups)
        {
            Console.WriteLine("Numbers with a remainder of {0} when divided by 5:", group.Remainder);
            foreach (var number in group.Numbers)
            {
                Console.WriteLine(number);
            }
        }
    }

    public void AnotherGroupExample()
    {
        var customerAccounts = new List<Account>
        {
            new("The Same Customer", "1", 1),
            new("The Same Customer", "2", 2),
            new("The Same Customer", "3", 3),
            new("The Same Customer", "4", 4),
            new("The Same Customer", "5", 5),
        };

        var groupedByName = customerAccounts
            .GroupBy(customer => customer.Name)
            .Select(grouping => grouping);

        foreach (var accountGroup in groupedByName)
        {
            var name = accountGroup.Key;
            foreach (var account in accountGroup)
            {
                // Do something with the account.
            }
        }
    }

    public void Samples101_ToDictionary()
    {
        // Notice the anonymous type.
        var results = new[]
        {
            new { Name = "Alice", Score = 50, Gender = 1, },
            new { Name = "Bob", Score = 40, Gender = 2, },
            new { Name = "Cathy", Score = 45, Gender = 1, },
        };

        var nameToScoreMap = 
            results.ToDictionary(
                result => result.Name,
                result => result.Score);
        // Bob's score: 40
        Console.WriteLine("Bob's score: {0}", nameToScoreMap["Bob"]);

        var nameToResultsMap = 
            results.ToDictionary(
                result => result.Name);
        // Bob's score: { Name = Bob, Score = 40, Gender = 2, }
        Console.WriteLine("Bob's score: {0}", nameToResultsMap["Bob"]);
    }
}