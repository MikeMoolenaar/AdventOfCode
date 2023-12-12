using System.Diagnostics;

var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

List<List<char>> mapChar = lines.Select(x => x.ToCharArray().ToList()).ToList();

List<List<Pipe>> map = new();
int size = lines[0].Length;
for (var y = 0; y < size; y++)
{
    var list = new List<Pipe>(size);
    for (var x = 0; x < size; x++)
    {
        list.Add(new Pipe(0, 0, 0, 0, (y, x), '.'));
    }
    map.Add(list);
    
    
}

(int y, int x) start = (-1, -1);
for (var y = 0; y < mapChar.Count; y++)
{
    var x = mapChar[y].FindIndex(x => x == 'S');
    if (x != -1)
        start = (y, x);
}
if (start is (-1, -1))
    throw new ArgumentException("S cannot be found");

for (var y = 0; y < mapChar.Count; y++)
{
    for (var x = 0; x < mapChar[y].Count; x++)
    {
        map[y][x] = mapChar[y][x] switch
        {
            '|' => new Pipe(1, 0, 1, 0, (y, x), mapChar[y][x]),
            '-' => new Pipe(0, 1, 0, 1, (y, x), mapChar[y][x]),
            'L' => new Pipe(1, 1, 0, 0, (y, x), mapChar[y][x]),
            'J' => new Pipe(1, 0, 0, 1, (y, x), mapChar[y][x]),
            '7' => new Pipe(0, 0, 1, 1, (y, x), mapChar[y][x]),
            'F' => new Pipe(0, 1, 1, 0, (y, x), mapChar[y][x]),
            '.' => new Pipe(0, 0, 0, 0, (y, x), mapChar[y][x]),
            'S' => new Pipe(1, 1, 1, 1, (y, x), mapChar[y][x]),
            _ => new Pipe(0, 0, 0, 0, (y, x), mapChar[y][x])
        };
    }
}


List<Pipe> open = [map[start.y][start.x]];

for (var i = 0; i < open.Count; i++)
{
    var pipe = open[i];
    List<(int y, int x)> neighbours =
    [
        (pipe.Coords.y - 1, pipe.Coords.x), // North
        (pipe.Coords.y, pipe.Coords.x + 1), // East
        (pipe.Coords.y + 1, pipe.Coords.x), // South
        (pipe.Coords.y, pipe.Coords.x - 1) // West
    ];
    
    foreach (var neighbourCoords in neighbours)
    {
        if (neighbourCoords.y < 0 || neighbourCoords.y >= map.Count || neighbourCoords.x < 0 || neighbourCoords.x >= map[0].Count)
            continue; // Out of bounds
        
        var neighbour = map[neighbourCoords.y][neighbourCoords.x];
        if (neighbour.Visited || neighbour.Sign == 'S')
            continue; // Should not go backwards to avoid infinite loop

        if (
            (pipe.North == 1 && neighbour.South == 1 && pipe.Coords.y == neighbour.Coords.y+1 && pipe.Coords.x == neighbour.Coords.x) ||
            (pipe.East == 1 && neighbour.West == 1 && pipe.Coords.y == neighbour.Coords.y && pipe.Coords.x == neighbour.Coords.x-1) ||
            (pipe.South == 1 && neighbour.North == 1 && pipe.Coords.y == neighbour.Coords.y-1 && pipe.Coords.x == neighbour.Coords.x) ||
            (pipe.West == 1 && neighbour.East == 1 && pipe.Coords.y == neighbour.Coords.y && pipe.Coords.x == neighbour.Coords.x+1)
        )
        {
            map[neighbourCoords.y][neighbourCoords.x].Step = pipe.Step + 1;
            open.Add(neighbour);
        }
    }

    map[pipe.Coords.y][pipe.Coords.x].Visited = true;
}

Pipe? highestPipe = null;
for (var y = 0; y < map.Count; y++)
{
    for (var x = 0; x < map[y].Count; x++)
    {
        var pipe = map[y][x];
        if (highestPipe is null || pipe.Step > highestPipe.Step)
            highestPipe = pipe;
    }
}

// ============== Part 1
Console.WriteLine("Part 1: " + highestPipe?.Step ?? "Not found");
Console.WriteLine("Correct: " + (highestPipe?.Step == 7012 ? "yes" : "n5o"));
Console.WriteLine();

// ============== Part 2
int part2Count = 0;
for (var y = 0; y < map.Count; y++)
{
    for (var x = 0; x < map[y].Count; x++)
    {
        var result = map[y][x];
        char transformedSign = result.Sign switch
        {
            '|' => '|',
            '-' => '-',
            'L' => '\u2514',
            'J' => '\u2518',
            '7' => '\u2510',
            'F' => '\u250c',
            _ => result.Sign
        };
            
        if (result.Visited)
            Console.Write(transformedSign);
        else
        {
            // See if open area is inside the square with a radius of 1/4th of the entire input
            // (yes this is a dirty hack and I'm not proud of it....)
            var inSquare = y > size/4 && y < size/4 * 3 && x > size/4 && x < size/4 * 3;
            if (inSquare)
            {
                Console.Write('Y');
                part2Count++;
            }
            else
                Console.Write('O');
        }
    }
    Console.WriteLine();
}

Console.WriteLine("Part 2: " + part2Count);
Console.WriteLine("Correct: " + (part2Count == 395 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");

public record Pipe(short North, short East, short South, short West, (int y, int x) Coords, char Sign)
{
    public char Sign { get; set; } = Sign;
    public int Step { get; set; } = 0;
    public bool Visited { get; set; } = false;
    public int? GroupId { get; set; } = null;

}