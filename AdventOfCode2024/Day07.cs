using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day07 : BaseDay
{
    private readonly Dictionary<long, List<long>> _input = new();

    public Day07()
    {
        foreach (var line in File.ReadAllLines(InputFilePath))
        {
            var split = line.Split(":");
            var key = long.Parse(split[0]);
            var values = split[1].Split(" ").Skip(1).Select(long.Parse).ToList();
            _input[key] = values;
        }
    }

    public override ValueTask<string> Solve_1()
    {
        long sum = 0;
        foreach (var kv in _input)
        {
            var operators = new List<List<string>>();
            GetOperatorsSimple(kv.Value, kv.Key, [], operators);

            // sum when there are at least one solution
            if (operators.Count > 0)
            {
                sum += kv.Key;
            }
        }

        return new ValueTask<string>($"{sum}");
    }

    private static void GetOperatorsSimple(List<long> values, long target, List<string> operators,
        List<List<string>> results)
    {
        if (values.Count == 1)
        {
            // check if the value is equal to the target
            if (values[0] == target)
            {
                results.Add([..operators]);
            }

            return;
        }

        var a = values[0];
        var b = values[1];
        var rest = values.Skip(2).ToList();

        // add
        rest.Insert(0, a + b);
        operators.Add("+");
        GetOperatorsSimple(rest, target, operators, results);
        operators.RemoveAt(operators.Count - 1);
        rest.RemoveAt(0);

        // multiply
        rest.Insert(0, a * b);
        operators.Add("*");
        GetOperatorsSimple(rest, target, operators, results);
        operators.RemoveAt(operators.Count - 1);
        rest.RemoveAt(0);
    }

    public override ValueTask<string> Solve_2()
    {
        long sum = 0;
        foreach (var kv in _input)
        {
            var operators = new List<List<string>>();
            GetOperatorsComplex(kv.Value, kv.Key, [], operators);

            // sum when there are at least one solution
            if (operators.Count > 0)
            {
                sum += kv.Key;
            }
        }

        return new ValueTask<string>($"{sum}");
    }

    private static void GetOperatorsComplex(List<long> values, long target, List<string> operators,
        List<List<string>> results)
    {
        if (values.Count == 1)
        {
            // check if the value is equal to the target
            if (values[0] == target)
            {
                results.Add([..operators]);
            }

            return;
        }

        var a = values[0];
        var b = values[1];
        var rest = values.Skip(2).ToList();

        // add
        rest.Insert(0, a + b);
        operators.Add("+");
        GetOperatorsComplex(rest, target, operators, results);
        operators.RemoveAt(operators.Count - 1);
        rest.RemoveAt(0);

        // multiply
        rest.Insert(0, a * b);
        operators.Add("*");
        GetOperatorsComplex(rest, target, operators, results);
        operators.RemoveAt(operators.Count - 1);
        rest.RemoveAt(0);

        // concatenate
        rest.Insert(0, long.Parse($"{a}{b}"));
        operators.Add("||");
        GetOperatorsComplex(rest, target, operators, results);
        operators.RemoveAt(operators.Count - 1);
        rest.RemoveAt(0);
    }
}