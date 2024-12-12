using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day11 : BaseDay
{
    private readonly Dictionary<long, List<long>> _cache;
    private readonly Dictionary<long, long> _numbers;

    public Day11()
    {
        var input = File.ReadAllText(InputFilePath);
        _numbers = input.Split(' ').Select(long.Parse).ToDictionary(n => n, _ => 1L);
        _cache = new Dictionary<long, List<long>>();
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>($"{Simulate(25)}");
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>($"{Simulate(75)}");
    }

    private long Simulate(int iterations)
    {
        var nums = new Dictionary<long, long>(_numbers);

        for (var i = 0; i < iterations; i++)
        {
            var next = new Dictionary<long, long>();

            foreach (var (num, count) in nums)
            {
                foreach (var n in ProcessNumber(num))
                {
                    if (!next.TryAdd(n, count))
                    {
                        next[n] += count;
                    }
                }
            }

            nums = next;
        }

        return nums.Values.Sum();
    }

    private IEnumerable<long> ProcessNumber(long num)
    {
        if (_cache.TryGetValue(num, out var cachedResult))
        {
            return cachedResult;
        }

        List<long> result;
        if (num == 0)
        {
            result = [1];
        }
        else
        {
            var digitCount = (int)Math.Floor(Math.Log10(num) + 1);

            if (digitCount % 2 == 0)
            {
                var power = (long)Math.Pow(10, digitCount / 2);
                var firstHalf = num / power;
                var secondHalf = num % power;
                result = [firstHalf, secondHalf];
            }
            else
            {
                result = [num * 2024];
            }
        }

        _cache[num] = result;
        return result;
    }
}