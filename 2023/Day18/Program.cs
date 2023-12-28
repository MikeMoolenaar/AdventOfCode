using System.Text.RegularExpressions;

var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

var regex = new Regex(@"(\w) (\d+) \(([#a-z0-9]+)\)");
var stepsInput = new Step[lines.Length];
for (var i = 0; i < lines.Length; i++)
{
    var result = regex.Match(lines[i]);
    
    var dir = result.Groups[1].Value[0];
    var count = long.Parse(result.Groups[2].Value);
    var color = result.Groups[3].Value;

    stepsInput[i] = new Step(dir, count, color);
}

((long y, long x)[] coords, long trenchLength) GetCoords((char Dir, long Count)[] steps)
{
    var coords = new (long y, long x)[steps.Length];

    (long y, long x) pos = (0, 0);
    long trenchLength = 0;
    for (var i = 0; i < steps.Length; i++)
    {
        var (dir, count) = steps[i];
        switch (dir)
        {
            case 'R':
                pos.x += count;
                break;
            case 'L':
                pos.x -= count;
                break;
            case 'U':
                pos.y -= count;
                break;
            case 'D':
                pos.y += count;
                break;
        }

        coords[i] = pos;
        trenchLength += count;
    }

    return (coords, trenchLength);
}

long CalcArea((char Dir, long Count)[] steps)
{
    var (coords, trenchLength) = GetCoords(steps);

    // Shoelace formula
    long s1 = 0;
    long s2 = 0;
    for (var i = 0; i < coords.Length-1; i++)
    {
        s1 += coords[i].x * coords[i + 1].y;
        s2 += coords[i].y * coords[i + 1].x;
    }
    var area = Math.Abs(s1 - s2) / 2;

    // Sum up area + outer border to get total area
    return area + trenchLength / 2 + 1;
}

// ============== Part 1
var part1Input = stepsInput.Select(x => (x.Dir, x.Count)).ToArray();
var part1 = CalcArea(part1Input);
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 52035 ? "yes" : "no"));

// ============== Part 2
var part2Input = stepsInput.Select(x =>
{
    var count = Convert.ToInt64(x.Color[1..6], 16);
    var dir = x.Color[^1] switch
    {
        '0' => 'R',
        '1' => 'D',
        '2' => 'L',
        '3' => 'U',
        _ => throw new ArgumentException($"Direction {x.Color[^1]} is not valid")
    };

    return (dir, count);
}).ToArray();
var part2 = CalcArea(part2Input);
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 60612092439765 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");

record Step(char Dir, long Count, string Color);