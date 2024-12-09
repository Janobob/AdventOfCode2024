using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day08 : BaseDay
{
    private char[][] _map;

    public Day08()
    {
        // Parse input
        var input = File.ReadAllLines(InputFilePath);
        
        _map = new char[input.Length][];
        for (var i = 0; i < input.Length; i++)
        {
            _map[i] = new char[input[i].Length];
            for (var j = 0; j < input[i].Length; j++)
            {
                _map[i][j] = input[i][j];
            }
        }
    }
    
    public override ValueTask<string> Solve_1()
    {
        var antennas = new Dictionary<char, List<(int i, int j)>>();
        
        // Fill antennas
        for (var i = 0; i < _map.Length; i++)
        {
            for (var j = 0; j < _map[i].Length; j++)
            {
                if (_map[i][j] == '.') continue;

                if (!antennas.TryGetValue(_map[i][j], out var value))
                {
                    value = new List<(int i, int j)>();
                    antennas[_map[i][j]] = value;
                }

                value.Add((i, j));
            }
        }

        var antinodes = new HashSet<(int i, int j)>();

        foreach (var kvp in antennas)
        {
            var antennaList = kvp.Value;

            // Compare each antenna against every other in the same list
            for (var a = 0; a < antennaList.Count; a++)
            {
                for (var b = a + 1; b < antennaList.Count; b++)
                {
                    var (i1, j1) = antennaList[a];
                    var (i2, j2) = antennaList[b];

                    var dx = i2 - i1;
                    var dy = j2 - j1;

                    // Check opposite directions
                    if (IsValidAntinode(i1 - dx, j1 - dy))
                    {
                        antinodes.Add((i1 - dx, j1 - dy));
                    }

                    if (IsValidAntinode(i2 + dx, j2 + dy))
                    {
                        antinodes.Add((i2 + dx, j2 + dy));
                    }
                }
            }
        }

        return new ValueTask<string>($"{antinodes.Count}");
    }

    private bool IsValidAntinode(int i, int j) =>
        i >= 0 && i < _map.Length && j >= 0 && j < _map[i].Length;

    public override ValueTask<string> Solve_2()
    {
        var antennas = new Dictionary<char, List<(int i, int j)>>();
        
        // Fill antennas
        for (var i = 0; i < _map.Length; i++)
        {
            for (var j = 0; j < _map[i].Length; j++)
            {
                if (_map[i][j] == '.') continue;

                if (!antennas.TryGetValue(_map[i][j], out var value))
                {
                    value = new List<(int i, int j)>();
                    antennas[_map[i][j]] = value;
                }

                value.Add((i, j));
            }
        }

        var antinodes = new HashSet<(int i, int j)>();

        foreach (var kvp in antennas)
        {
            var antennaList = kvp.Value;

            // Compare each antenna against every other in the same list
            for (var a = 0; a < antennaList.Count; a++)
            {
                for (var b = a + 1; b < antennaList.Count; b++)
                {
                    var (i1, j1) = antennaList[a];
                    var (i2, j2) = antennaList[b];

                    var dx = i2 - i1;
                    var dy = j2 - j1;
                    
                    var amplitude = 0;
                    while (IsValidAntinode(i1 - dx * amplitude, j1 - dy * amplitude))
                    {
                        antinodes.Add((i1 - dx * amplitude, j1 - dy * amplitude));
                        amplitude++;                        
                    }
                    
                    amplitude = 0;
                    while (IsValidAntinode(i2 + dx * amplitude, j2 + dy * amplitude))
                    {
                        antinodes.Add((i2 + dx * amplitude, j2 + dy * amplitude));
                        amplitude++;
                    }
                }
            }
        }
        
        return new ValueTask<string>($"{antinodes.Count}");
    }
}