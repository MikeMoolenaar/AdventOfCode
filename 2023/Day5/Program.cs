using System.Text.RegularExpressions;

var lines = File.ReadLines("Input.txt").ToArray();

var seedString = Regex.Match(lines[0], "seeds: (.*)");
List<long> seeds = seedString.Groups[1]
    .Value
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(long.Parse)
    .ToList();

var regex = Regex.Matches(string.Join('\n', lines[2..]), @"(?!map:\n)(?:\d ?\n?)+");

List<List<(long destRange, long sourceRangeStart, long length)>> maps = new();
foreach (Match o in regex)
{
    var map = new List<(long destRange, long sourceRangeStart, long length)>();

    foreach (var c in o.Value.Split('\n', StringSplitOptions.RemoveEmptyEntries))
    {
        var parts = c.Split(' ');
        map.Add((long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
    }
    
    maps.Add(map);
}

long GetSeedPosition(long seed)
{
    long curPos = seed;
    foreach (var map in maps)
    {
        foreach (var (destRange, sourceRangeStart, length) in map)
        {
            if (sourceRangeStart <= curPos && curPos < sourceRangeStart + length)
            {
                curPos = destRange + (curPos - sourceRangeStart);
                break;
            }
               
        }
    }

    return curPos;
}

// ============== Part 1
var min = seeds.Select(GetSeedPosition).Min();
Console.WriteLine("Part 1: " +  min);
Console.WriteLine("Correct: " + (min == 324724204 ? "yes" : "no"));
Console.WriteLine();

// ============== Part 2
List <(long seeds, long length)> seedRanges = new();
for (int i = 0; i < seeds.Count; i+=2)
{
    seedRanges.Add((seeds[i], seeds[i+1]));
}

long lowestValue = long.MaxValue;
foreach (var (seed, length) in seedRanges)
{
    for (long i = seed; i < seed+length; i++)
    {
        var res = GetSeedPosition(i);
        if (res < lowestValue)
            lowestValue = res;
    }
}

Console.WriteLine("Part 2: " +  lowestValue);
Console.WriteLine("Correct: " + (lowestValue == 104070862 ? "yes" : "no"));
Console.WriteLine();