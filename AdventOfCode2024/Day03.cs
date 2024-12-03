using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode2024;

public sealed partial class Day03 : BaseDay
{
    private readonly string _input;
    private readonly Regex _mulRegex;
    private readonly Regex _mulDoDontRegex;
    
    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
        _mulRegex = MulRegex();
        _mulDoDontRegex = MulDoDontRegex();
    }
    
    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        
        var matches = _mulRegex.Matches(_input);
        foreach (Match match in matches)
        {
            var splits = match.Value.Remove(0, 4).Remove(match.Value.Length - 5, 1).Split(",");
            var n1 = splits[0];
            var n2 = splits[1];
            sum += int.Parse(n1) * int.Parse(n2);
        }
        
        return new ValueTask<string>($"{sum}");
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        var skip = false;
        
        var matches = _mulDoDontRegex.Matches(_input);
        foreach (Match match in matches)
        {
            if (match.Value == "do()")
            {
                skip = false;
                continue;
            }
            
            if (match.Value == "don't()")
            {
                skip = true;
                continue;
            }
            
            if(skip) continue;
            
            var splits = match.Value.Remove(0, 4).Remove(match.Value.Length - 5, 1).Split(",");
            var n1 = splits[0];
            var n2 = splits[1];
            sum += int.Parse(n1) * int.Parse(n2);
        }
        
        return new ValueTask<string>($"{sum}");
    }

    [GeneratedRegex(@"mul\(\d*,\d*\)", RegexOptions.Multiline)]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"mul\(\d*,\d*\)|do\(\)|don't\(\)", RegexOptions.Multiline)]
    private static partial Regex MulDoDontRegex();
}