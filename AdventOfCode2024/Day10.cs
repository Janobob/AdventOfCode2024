using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day10 : BaseDay
{
    private readonly int[][] _map;

    public Day10()
    {
        var lines = File.ReadAllLines(InputFilePath);

        _map = new int[lines.Length][];
        for (var i = 0; i < lines.Length; i++)
        {
            _map[i] = new int[lines[i].Length];
            for (var j = 0; j < lines[i].Length; j++)
            {
                _map[i][j] = lines[i][j] - '0';
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = 0;

        for (var i = 0; i < _map.Length; i++)
        {
            for (var j = 0; j < _map[i].Length; j++)
            {
                if (_map[i][j] != 0)
                {
                    continue;
                }

                var ends = new HashSet<(int i, int j)>();
                Hike(i, j, 0, ends);
                sum += ends.Count;
            }
        }

        return new ValueTask<string>($"{sum}");
    }

    private void Hike(int i, int j, int n, ICollection<(int i, int j)> ends)
    {
        if (i < 0 || i >= _map.Length || j < 0 || j >= _map[i].Length)
        {
            return;
        }

        var cell = _map[i][j];

        if (n < cell || n > cell)
        {
            return;
        }

        if (cell == 9)
        {
            ends.Add((i, j));
            return;
        }

        Hike(i - 1, j, n + 1, ends);
        Hike(i + 1, j, n + 1, ends);
        Hike(i, j - 1, n + 1, ends);
        Hike(i, j + 1, n + 1, ends);
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;

        for (var i = 0; i < _map.Length; i++)
        {
            for (var j = 0; j < _map[i].Length; j++)
            {
                if (_map[i][j] != 0)
                {
                    continue;
                }

                var ends = new List<(int i, int j)>();
                Hike(i, j, 0, ends);
                sum += ends.Count;
            }
        }

        return new ValueTask<string>($"{sum}");
    }
}