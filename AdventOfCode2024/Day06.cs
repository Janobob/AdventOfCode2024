using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day06 : BaseDay
{
    private readonly string _direction = "^";
    private readonly (int y, int x) _guardPosition = (0, 0);
    private readonly List<List<char>> _map = [];
    private readonly int _xLength;
    private readonly int _yLength;

    public Day06()
    {
        // Convert input to map
        var input = File.ReadAllLines(InputFilePath);
        foreach (var line in input)
        {
            _map.Add(line.ToCharArray().ToList());
        }

        // Set map dimensions
        _yLength = _map.Count;
        _xLength = _map[0].Count;

        // find guard position
        for (var y = 0; y < _yLength; y++)
        for (var x = 0; x < _xLength; x++)
        {
            if (_map[y][x] == '^')
            {
                _guardPosition = (y, x);
                break;
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        // Copy map 
        var map = _map.Select(row => row.ToList()).ToList();
        var guardPosition = _guardPosition;
        var direction = _direction;

        while (true)
        {
            // Set X to guard position
            map[guardPosition.y][guardPosition.x] = 'X';

            // Define movement deltas and direction transitions
            var directions = new Dictionary<string, (int dx, int dy, string nextDirection)>
            {
                { "^", (0, -1, ">") },
                { ">", (1, 0, "v") },
                { "v", (0, 1, "<") },
                { "<", (-1, 0, "^") }
            };

            // Get current direction details
            var (dx, dy, nextDirection) = directions[direction];

            // Calculate next position
            var nextX = guardPosition.x + dx;
            var nextY = guardPosition.y + dy;

            // Check if the next position is out of bound
            if (!IsInBounds(nextX, nextY, _xLength, _yLength))
            {
                break;
            }

            // Check if the next position is a wall
            if (map[nextY][nextX] == '#')
            {
                direction = nextDirection;
            }
            else
            {
                guardPosition.x = nextX;
                guardPosition.y = nextY;
            }

            // Break if the guard is out of bounds
            if (!IsInBounds(guardPosition.x, guardPosition.y, _xLength, _yLength))
            {
                break;
            }
        }

        return new ValueTask<string>($"{map.Sum(row => row.Count(c => c == 'X'))}");
    }

    public override ValueTask<string> Solve_2()
    {
        var count = 0;

        for (var y = 0; y < _yLength; y++)
        {
            for (var x = 0; x < _xLength; x++)
            {
                if (_map[y][x] != '.' || (y, x) == _guardPosition)
                {
                    continue;
                }

                // Place an obstruction
                _map[y][x] = '#';

                // Check for loops
                if (CausesLoop())
                {
                    count++;
                }

                // Remove the obstruction
                _map[y][x] = '.';
            }
        }

        return new ValueTask<string>($"{count}");
    }

    private bool CausesLoop()
    {
        var visited = new HashSet<(int y, int x, string direction)>();
        var guardPosition = _guardPosition;
        var direction = _direction;

        var directions = new Dictionary<string, (int dx, int dy, string nextDirection)>
        {
            { "^", (0, -1, ">") },
            { ">", (1, 0, "v") },
            { "v", (0, 1, "<") },
            { "<", (-1, 0, "^") }
        };

        while (true)
        {
            var state = (guardPosition.y, guardPosition.x, direction);

            if (!visited.Add(state))
            {
                return true;
            }

            var (dx, dy, nextDirection) = directions[direction];
            var nextX = guardPosition.x + dx;
            var nextY = guardPosition.y + dy;

            if (!IsInBounds(nextX, nextY, _xLength, _yLength))
            {
                return false;
            }

            if (_map[nextY][nextX] == '#')
            {
                direction = nextDirection;
            }
            else
            {
                guardPosition = (nextY, nextX);
            }
        }
    }

    private static bool IsInBounds(int x, int y, int xLength, int yLength)
    {
        return x >= 0 && x < xLength && y >= 0 && y < yLength;
    }
}