var watch = System.Diagnostics.Stopwatch.StartNew();
var patterns = File.ReadAllText("Input.txt")
    .Split("\n\n")
    .Select(x => x.Split('\n'))
    .ToArray();


bool InRange(int i, int i1, int length) => i - i1 >= 0 && i + i1 <= length;

int Calculate(string[] pattern, bool part1)
{
    // Vertical
    int patternVertical = 0;
    for (var i = 1; i < pattern[0].Length; i++)
    {
        for (var i1 = 1; InRange(i, i1, pattern[0].Length); i1++)
        {
            if (i - i1 != 0 && i + i1 != pattern[0].Length) continue;
            var line1 = string.Join(string.Empty, pattern.Select(x => x[(i - i1)..i]));
            var line2 = string.Join(string.Empty, pattern.Select(x => string.Join(string.Empty, x[i..(i + i1)].Reverse())));

            var diff = 0;
            for (var linex = 0; linex < line1.Length; linex++)
            {
                if (line1[linex] != line2[linex])
                {
                    diff++;
                    if (part1) break;
                }
            }
            
            if ((part1 && diff == 0) || (!part1 && diff == 1))
            {
                patternVertical = i;
                break;
            }
        }
    }

    if (patternVertical != 0)
    {
        // Console.WriteLine("Vertical="+patternVertical);
        return patternVertical;
    }
    
    // Horizontal
    int patternHorizontal = 0;
    for (var i = 1; i < pattern.Length; i++)
    {
        for (var i1 = 1; InRange(i, i1, pattern.Length); i1++)
        {
            if (i - i1 != 0 && i + i1 != pattern.Length) continue;
            
            var line1 = string.Join(string.Empty, pattern[(i - i1)..i]);
            var line2 = string.Join(string.Empty, pattern[i..(i + i1)].Reverse().ToArray());
            
            var diff = 0;
            for (var linex = 0; linex < line1.Length; linex++)
            {
                if (line1[linex] != line2[linex])
                {
                    diff++;
                    if (part1) break;
                }
            }
            
            if ((part1 && diff == 0) || (!part1 && diff == 1))
            {
                patternHorizontal = i;
                break;
            }
        }
    }

    if (patternHorizontal != 0)
    {
        return patternHorizontal * 100;
    }
    
    return 0;
}

var part1 = patterns.Sum(p => Calculate(p, true));
Console.WriteLine();
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 43614 ? "yes" : "no"));

var part2 = patterns.Sum(p => Calculate(p, false));

Console.WriteLine();
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 36771 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");