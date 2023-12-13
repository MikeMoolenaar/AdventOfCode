var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

var cache = new Dictionary<string, long>();
long Calculate(string record, List<int> groups)
{
    var cacheKey = record + String.Join(',', groups);
    if (cache.TryGetValue(cacheKey, out long res))
        return res;
    
    if (groups.Count == 0)
        return record.Contains('#') ? 0 : 1;

    if (record.Length == 0)
        return 0;

    var nextChar = record[0];
    var nextGroup = groups[0];
    
    // Logic that Logic that treats the first character as pound-sign "#"
    long Pound()
    {
        if (record.Length < nextGroup)
            return 0;
        var thisGroup = record[..nextGroup].Replace("?", "#");

        if (thisGroup != new String('#', nextGroup))
            return 0;

        if (record.Length == nextGroup)
            return groups.Count == 1 ? 1 : 0;

        if (record[nextGroup] == '?' || record[nextGroup] == '.')
        {
            // Can be a separator, reduce
            return Calculate(record[(nextGroup + 1)..], groups[1..]);
        }

        // No possibilities
        return 0;
    }
    
    // Logic that Logic that treats the first character as dot "."
    long Dot()
    {
        return Calculate(record[1..], groups);
    }

    long output = nextChar switch
    {
        '#' => Pound(),
        '.' => Dot(),
        '?' => Dot() + Pound(),
        _ => throw new ArgumentException($"Unknown char {nameof(nextChar)}={nextChar}")
    };

    cache.Add(cacheKey, output);
    return output;
}

long GetPossibleCombinations(int repeat)
{
    long sum = 0;
    foreach (var line in lines)
    {
        var recordOriginal = line.Split(' ')[0];
        var groupsOriginal = line.Split(' ')[1].Split(',').Select(int.Parse).ToList();

        var record = "";
        var groups = new List<int>();
    
        for (var i = 0; i < repeat; i++)
        {
            record += (i != 0 ? "?" : "") + recordOriginal;
            groups.AddRange(groupsOriginal);
        }
    
        sum += Calculate(record, groups);
    }

    return sum;
}

// ============== Part 1
var part1 = GetPossibleCombinations(1);
Console.WriteLine();
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 7221 ? "yes" : "no"));

// ============== Part 2
var part2 = GetPossibleCombinations(5);
Console.WriteLine();
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 7139671893722 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");