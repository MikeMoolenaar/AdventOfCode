var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToList();

long SumOfGalaxyDistances(List<string> lines, long expansion)
{
    // Expand rows
    List<int> rowExpansions = new();
    for (var i = 0; i < lines.Count; i++)
    {
        if (lines[i].All(x => x == '.')) 
            rowExpansions.Add(i);
    }
    
    // Expand columns
    List<int> columnExpansions = new();
    for (var i = 0; i < lines[0].Length; i++)
    {
        var foundGalaxy = false;
        foreach (var line in lines)
        {
            if (line[i] == '#')
                foundGalaxy = true;
        }
    
        if (!foundGalaxy) 
            columnExpansions.Add(i);
    }
    
    // Map galaxies
    List<(long y, long x)> galaxies = new();
    for (var y = 0; y < lines.Count; y++)
    {
        for (var x = 0; x < lines[0].Length; x++)
        {
            if (lines[y][x] == '#')
            {
                int yExpansion;
                for (yExpansion = 0; yExpansion < rowExpansions.Count; yExpansion++)
                {
                    if (rowExpansions[yExpansion] > y) break;
                }
                
                int xExpansion;
                for (xExpansion = 0; xExpansion < columnExpansions.Count; xExpansion++)
                {
                    if (columnExpansions[xExpansion] > x) break;
                }

                galaxies.Add((y + yExpansion * expansion, x + xExpansion * expansion));
            }
        }
    }
    
    // Get distance between all galaxy pairs using Manhattan Distance
    long sum = 0;
    foreach (var (y1, x1) in galaxies)
    {
        foreach (var (y2, x2) in galaxies)
        {
            sum += Math.Abs(y1 - y2) + Math.Abs(x1 - x2);
        }
    }

    // I don't know why I should divide by 2, but the answer is correct
    return sum / 2;
}

// ============== Part 1
var result1 = SumOfGalaxyDistances(lines, 1);
Console.WriteLine("Part 1: " + result1);
Console.WriteLine("Correct: " + (result1 == 10033566 ? "yes" : "no"));
Console.WriteLine();

// ============== Part 2
// I don't know why -1 but it works
var result2 = SumOfGalaxyDistances(lines, 1_000_000-1);
Console.WriteLine("Part 2: " + result2);
Console.WriteLine("Correct: " + (result2 == 560822911938 ? "yes" : "no"));
    
watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");