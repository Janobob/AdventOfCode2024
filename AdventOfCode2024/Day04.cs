using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day04 : BaseDay
{
    private readonly char[][] _input;
    
    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToCharArray()).ToArray();
    }
    
    public override ValueTask<string> Solve_1()
    {
        var count = 0;
        var xLength = _input.Length;
        var yLength = _input[0].Length;
        for(var x = 0; x < xLength; x++)
        {
            for(var y = 0; y < yLength; y++)
            {
                // skip if not X or S
                if (_input[x][y] != 'X' && _input[x][y] != 'S') continue;
                
                // horizontal forward
                if (y + 3 < yLength && _input[x][y] == 'X' && _input[x][y + 1] == 'M' && _input[x][y + 2] == 'A' &&
                    _input[x][y + 3] == 'S')
                {
                    count++;
                }
                
                // horizontal backward
                if (y + 3 < yLength && _input[x][y] == 'S' && _input[x][y + 1] == 'A' && _input[x][y + 2] == 'M' &&
                    _input[x][y + 3] == 'X')
                {
                    count++;
                }
                
                // vertical forward
                if(x + 3 < xLength && _input[x][y] == 'X' && _input[x + 1][y] == 'M' && _input[x + 2][y] == 'A' &&
                   _input[x + 3][y] == 'S')
                {
                    count++;
                }
                
                // vertical backward
                if(x + 3 < xLength && _input[x][y] == 'S' && _input[x + 1][y] == 'A' && _input[x + 2][y] == 'M' &&
                   _input[x + 3][y] == 'X')
                {
                    count++;
                }
                
                // diagonal forward down
                if(x + 3 < xLength && y + 3 < yLength && _input[x][y] == 'X' && _input[x + 1][y + 1] == 'M' &&
                   _input[x + 2][y + 2] == 'A' && _input[x + 3][y + 3] == 'S')
                {
                    count++;
                }
                
                // diagonal forward up
                if(x + 3 < xLength && y - 3 >= 0 && _input[x][y] == 'X' && _input[x + 1][y - 1] == 'M' &&
                   _input[x + 2][y - 2] == 'A' && _input[x + 3][y - 3] == 'S')
                {
                    count++;
                }
                
                // backward down
                if(x + 3 < xLength && y + 3 < yLength && _input[x][y] == 'S' && _input[x + 1][y + 1] == 'A' &&
                   _input[x + 2][y + 2] == 'M' && _input[x + 3][y + 3] == 'X')
                {
                    count++;
                }
                
                // backward up
                if(x + 3 < xLength && y - 3 >= 0 && _input[x][y] == 'S' && _input[x + 1][y - 1] == 'A' &&
                   _input[x + 2][y - 2] == 'M' && _input[x + 3][y - 3] == 'X')
                {
                    count++;
                }
            }
        }
        return new ValueTask<string>($"{count}");
    }

    public override ValueTask<string> Solve_2()
    {
        var count = 0;
        var xLength = _input.Length;
        var yLength = _input[0].Length;
        for(var x = 0; x < xLength; x++)
        {
            for(var y = 0; y < yLength; y++)
            {
                // skip if not A
                if (_input[x][y] != 'A') continue;
                
                // two S top and two M bottom
                if (x - 1 >= 0 && x + 1 < xLength &&
                    y - 1 >= 0 && y + 1 < yLength &&
                    _input[x - 1][y - 1] == 'S' && _input[x - 1][y + 1] == 'S' &&
                    _input[x + 1][y - 1] == 'M' && _input[x + 1][y + 1] == 'M')
                {
                    count++;
                }
                    
                // two M top and two S bottom
                if (x - 1 >= 0 && x + 1 < xLength &&
                    y - 1 >= 0 && y + 1 < yLength &&
                    _input[x - 1][y - 1] == 'M' && _input[x - 1][y + 1] == 'M' &&
                    _input[x + 1][y - 1] == 'S' && _input[x + 1][y + 1] == 'S')
                {
                    count++;
                }
                    
                // two S left and two M right
                if (y - 1 >= 0 && y + 1 < yLength &&
                    x - 1 >= 0 && x + 1 < xLength &&
                    _input[x - 1][y - 1] == 'S' && _input[x + 1][y - 1] == 'S' &&
                    _input[x - 1][y + 1] == 'M' && _input[x + 1][y + 1] == 'M')
                {
                    count++;
                }
                    
                // two M left and two S right
                if (y - 1 >= 0 && y + 1 < yLength &&
                    x - 1 >= 0 && x + 1 < xLength &&
                    _input[x - 1][y - 1] == 'M' && _input[x + 1][y - 1] == 'M' &&
                    _input[x - 1][y + 1] == 'S' && _input[x + 1][y + 1] == 'S')
                {
                    count++;
                }
            }
        }
        return new ValueTask<string>($"{count}");
    }
}