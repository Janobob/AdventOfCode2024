using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day01 : BaseDay
{
    private readonly List<int> _left = [];
    private readonly List<int> _right = [];
    
    public Day01()
    {
        var input = File.ReadAllLines(InputFilePath);
        // fill the left and right lists
        foreach (var line in input)
        {
            var split = line.Split("   ");
            _left.Add(int.Parse(split[0]));
            _right.Add(int.Parse(split[1]));
        }
        
        // order the lists
        _left.Sort();
        _right.Sort();
    }
    
    public override ValueTask<string> Solve_1()
    {
        var totalDistance = 0;
        
        // calculate the total distance
        // Don't use LINQ because it's slower
        for(var i = 0; i < _left.Count; i++)
        {
            totalDistance += Math.Abs(_left[i] - _right[i]);
        }
        
        return new ValueTask<string>($"{totalDistance}");
    }

    public override ValueTask<string> Solve_2()
    {
        var lookUp = new Dictionary<int, int>();
        var total = 0;
        
        // fill the lookup
        for(var i = 0; i < _left.Count; i++)
        {
            var value = _left[i];
            if(!lookUp.ContainsKey(value))
                lookUp.Add(value, CountOccurrences(_right, value) * value);
            total += lookUp[value];
        }
        return new ValueTask<string>($"{total}");
    }
    
    private int CountOccurrences(List<int> sortedList, int value)
    {
        var firstIndex = FindFirstIndex(sortedList, value);
        if (firstIndex == -1) 
            return 0;

        var lastIndex = FindLastIndex(sortedList, value);
        return lastIndex - firstIndex + 1;
    }

    private int FindFirstIndex(List<int> sortedList, int value)
    {
        int low = 0, high = sortedList.Count - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            if (sortedList[mid] < value)
            {
                low = mid + 1;
            }
            else if (sortedList[mid] > value || (mid > 0 && sortedList[mid - 1] == value))
            {
                high = mid - 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }

    private int FindLastIndex(List<int> sortedList, int value)
    {
        int low = 0, high = sortedList.Count - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            if (sortedList[mid] > value)
            {
                high = mid - 1;
            }
            else if (sortedList[mid] < value || (mid < sortedList.Count - 1 && sortedList[mid + 1] == value))
            {
                low = mid + 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }
}