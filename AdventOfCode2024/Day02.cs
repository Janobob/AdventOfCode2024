using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day02 : BaseDay
{
    private readonly List<List<int>> _reports = [];
    
    public Day02()
    {
        var input = File.ReadAllLines(InputFilePath);
        foreach (var line in input)
        {
            var levels = line.Split(' ');
            _reports.Add(levels.Select(int.Parse).ToList());
        }
    }
    
    public override ValueTask<string> Solve_1()
    {
        var safe = 0;
        foreach(var report in _reports)
        {
            if (IsValidReport(report))
            {
                safe++;
            }
        }
        return new ValueTask<string>($"{safe}");
    }

    public override ValueTask<string> Solve_2()
    {
        var safe = 0;

        foreach (var report in _reports)
        {
            for (var skipIndex = 0; skipIndex < report.Count; skipIndex++)
            {
                var filteredReport = new List<int>(report.Count - 1);

                for (var i = 0; i < report.Count; i++)
                {
                    if (i != skipIndex)
                    {
                        filteredReport.Add(report[i]);
                    }
                }

                if (IsValidReport(filteredReport))
                {
                    safe++;
                    break;
                }
            }
        }
        
        return new ValueTask<string>($"{safe}");
    }

    private static bool IsValidReport(List<int> report)
    {
        if(report[0] == report[1])
        {
            return false;
        }
        
        var inc = report[0] - report[1] < 0;
        var dec = report[0] - report[1] > 0;

        for (var i = 0; i < report.Count - 1; i++)
        {
            var diff = report[i] - report[i + 1];
            if (dec && (diff < 1 || diff > 3))
                return false;
            if (inc && (diff > -1 || diff < -3))
                return false;
        }

        return true;
    }
}