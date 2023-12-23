var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();
var steps = lines[0].Split(',');

int Hash(string input)
{
    var x = 0;
    foreach (var c in input)
    {
        x += c;
        x *= 17;
        x %= 256;
    }

    return x;
}

var part1 = 0;
foreach (var step in steps) 
    part1 += Hash(step);

Console.WriteLine("Part 1: " + part1);
Console.WriteLine();

List<List<(string Label, int focalLength)>> boxes = new(256);
for (var i = 0; i < boxes.Capacity; i++)
{
    boxes.Add(new(9));
}

foreach (var step in steps)
{
    
    if (step.Contains('='))
    {
        var parts = step.Split('=');
        var label = parts[0];
        var focalLength = int.Parse(parts[1]);
        
        var boxNumber = Hash(label);

        var index = boxes[boxNumber].FindIndex(x => x.Label == label);
        if (index != -1)
        {
            boxes[boxNumber][index] = (label, focalLength);
        }
        else
        {
            boxes[boxNumber].Add((label,focalLength));
        }
    }
    else if (step.Contains('-'))
    {
        var label = step.Split('-')[0];
        var boxNumber = Hash(label);
        
        var index = boxes[boxNumber].FindIndex(x => x.Label == label);
        if (index != -1)
        {
            boxes[boxNumber].RemoveAt(index);
        }
    }
    else
    {
        throw new ArgumentException("step must contain either '=' or '-'");
    }
}

#if DEBUG
Console.WriteLine("Boxes part 2");
for (var i = 0; i < boxes.Count; i++)
{
    Console.Write($"Box {i}: ");
    foreach (var (label, focalLength) in boxes[i])
    {
        Console.Write($"[{label} {focalLength}] ");
    }
    Console.WriteLine();
}
Console.WriteLine();
#endif

int part2 = 0;
for (var i = 0; i < boxes.Count; i++)
{
    for (var i1 = 0; i1 < boxes[i].Count; i1++)
    {
        var focalLength = boxes[i][i1].focalLength;
        part2 += (i + 1) * (i1 + 1) * focalLength;
    }
}

Console.WriteLine("Part 2: " + part2);




watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");