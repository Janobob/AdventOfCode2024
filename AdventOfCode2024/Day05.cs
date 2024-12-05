using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day05 : BaseDay
{
    private readonly List<Rule> _rules = [];
    private readonly List<List<int>> _updates = [];

    public Day05()
    {
        var input = File.ReadAllLines(InputFilePath);

        var i = 0;
        // parse rules
        while (true)
        {
            var line = input[i++];
            if (string.IsNullOrWhiteSpace(line)) break;

            var split = line.Split("|");
            var before = int.Parse(split[0]);
            var after = int.Parse(split[1]);

            // add rule for before and after
            var ruleBefore = _rules.FirstOrDefault(x => x.PageNumber == before) ?? new Rule { PageNumber = before };
            var ruleAfter = _rules.FirstOrDefault(x => x.PageNumber == after) ?? new Rule { PageNumber = after };

            ruleBefore.Before.Add(after);
            ruleAfter.After.Add(before);

            // update or add rules
            if (!_rules.Contains(ruleBefore)) _rules.Add(ruleBefore);
            if (!_rules.Contains(ruleAfter)) _rules.Add(ruleAfter);
        }

        // parse updates
        foreach (var line in input[i..]) _updates.Add(line.Split(",").Select(int.Parse).ToList());
    }


    public override ValueTask<string> Solve_1()
    {
        var count = 0;

        foreach (var update in _updates)
        {
            int i;
            for (i = 0; i < update.Count; i++)
            {
                var rule = _rules.FirstOrDefault(x => x.PageNumber == update[i]);
                var beforePages = update[..i];
                var afterPages = update[(i + 1)..];

                if (!(rule.AreAllBefore(beforePages) && rule.AreAllAfter(afterPages))) break;
            }

            // Skip if update is incorrect
            if (i != update.Count) continue;

            // Get middle page number
            var middle = update[update.Count / 2];
            count += middle;
        }

        return new ValueTask<string>($"{count}");
    }

    public override ValueTask<string> Solve_2()
    {
        var count = 0;

        foreach (var update in _updates)
        {
            int i;
            for (i = 0; i < update.Count; i++)
            {
                var rule = _rules.FirstOrDefault(x => x.PageNumber == update[i]);
                var beforePages = update[..i];
                var afterPages = update[(i + 1)..];

                if (!(rule.AreAllBefore(beforePages) && rule.AreAllAfter(afterPages))) break;
            }

            // Skip if update is correct
            if (i == update.Count) continue;

            // Correct update with comparing rules
            var sorted = update.OrderBy(x => x, Comparer<int>.Create((p1, p2) =>
            {
                var ruleP1 = _rules.FirstOrDefault(r => r.PageNumber == p1);
                var ruleP2 = _rules.FirstOrDefault(r => r.PageNumber == p2);

                // Check if rules are null
                if (ruleP1 == null || ruleP2 == null)
                    return 0;

                // Check if x must come before y
                if (ruleP1.Before.Contains(p2))
                    return -1;

                // Check if x must come after y
                if (ruleP1.After.Contains(p2))
                    return 1;

                // Check if y must come before x
                if (ruleP2.Before.Contains(p1))
                    return 1;

                // Check if y must come after x
                if (ruleP2.After.Contains(p1))
                    return -1;

                return 0;
            })).ToList();

            // Get middle page number
            var middle = sorted[sorted.Count / 2];
            count += middle;
        }

        return new ValueTask<string>($"{count}");
    }

    private class Rule
    {
        public int PageNumber { get; set; }
        public List<int> Before { get; } = [];
        public List<int> After { get; } = [];

        private bool IsBefore(int pageNumber)
        {
            return Before.Contains(pageNumber);
        }

        private bool IsAfter(int pageNumber)
        {
            return After.Contains(pageNumber);
        }

        public bool AreAllBefore(List<int> pageNumbers)
        {
            return !pageNumbers.Any(IsBefore);
        }

        public bool AreAllAfter(List<int> pageNumbers)
        {
            return !pageNumbers.Any(IsAfter);
        }
    }
}