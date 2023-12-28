// This only worked for part 1 , but couldn't get part 2 to work.

// The most illegal thing I own
var stackSize = 100000000;
Thread thread = new Thread(new ThreadStart(Main), stackSize);
thread.Start();
thread.Join();

void Main()
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").Select(x => 
    x.Select(c => int.Parse(c.ToString())).ToList())
    .ToList();

(int y, int x) dest = (lines.Count-1, lines[0].Count-1);
(int y, int x) start = (0, 0);
List<Node> nodes = new();

// Calculate distances
for (var y = 0; y < lines.Count; y++)
{
    for (var x = 0; x < lines[0].Count; x++)
    {
        var weight = lines[y][x];
        var distance = dest.y - y + dest.x - x;
        nodes.Add(new Node((y, x), weight, distance));
    }
}

var startIndex = nodes.FindIndex(n => n.Pos == dest);
nodes[startIndex].Score = 0;
Calc(nodes[startIndex]);

void Calc(Node node)
{
    nodes.Find(n => n.Pos == node.Pos)!.Visited = true;
    
    if (node.Pos == start) return;
    
    List<(int y, int x)> checkNodes =
    [
        (node.Pos.y - 1, node.Pos.x),
        (node.Pos.y, node.Pos.x + 1),
        (node.Pos.y + 1, node.Pos.x),
        (node.Pos.y, node.Pos.x - 1)
    ];

    foreach (var checkPos in checkNodes)
    {
        var index = nodes.FindIndex(n => n.Pos == checkPos);
        if (index == -1 || nodes[index].Visited) continue;
        
        var newCost = node.Score + nodes[index].Weight;
        if (newCost < nodes[index].Score)
            nodes[index].Score = newCost;
    }

    var newNode = nodes.Where(n => !n.Visited).MinBy(n => n.Score);
    if (newNode is not null)
        Calc(newNode);
}

List<Node> path = new();
void ForwardTrace(Node node, char dir, int distance, bool isPart2)
{
    path.Add(node);
    if (node.Pos == dest) return;

    List<(int y, int x)> checkNodes = [];

    if (isPart2)
    {
        if (dir != 'n' && ((dir != 'e' && dir != 'w') || distance >= 3) && ( dir != 's' || distance <= 10))
            checkNodes.Add((node.Pos.y + 1, node.Pos.x));
        if (dir != 'w' && ((dir != 'n' && dir != 's') || distance >= 3) && (dir != 'e' || distance <= 10))
            checkNodes.Add((node.Pos.y, node.Pos.x + 1));
        if (dir != 's' && ((dir != 'w' && dir != 'e') || distance >= 3) && (dir != 'n' || distance <= 10))
            checkNodes.Add((node.Pos.y - 1, node.Pos.x));
        if (dir != 'e' && ((dir != 'n' && dir != 's') || distance >= 3) && (dir != 'w' || distance <= 10))
            checkNodes.Add((node.Pos.y, node.Pos.x - 1));
    }
    else
    {
        if (dir != 'n' && (dir != 's' || distance <= 3))
            checkNodes.Add((node.Pos.y + 1, node.Pos.x));
        if (dir != 'w' && (dir != 'e' || distance <= 3))
            checkNodes.Add((node.Pos.y, node.Pos.x + 1));
        if (dir != 's' && (dir != 'n' || distance <= 3))
            checkNodes.Add((node.Pos.y - 1, node.Pos.x));
        if (dir != 'e' && (dir != 'w' || distance <= 3))
            checkNodes.Add((node.Pos.y, node.Pos.x - 1));
    }
  

    var result = checkNodes.Select(p => nodes.Find(n => n.Pos == p))
        .Where(n => n != null && !path.Contains(n))
        .MinBy(n => n!.Score);

    if (result is null)
    {
        Console.WriteLine($"!! Cart crashed at position {node.Pos} !!");
        return;
    }

    var resultDir = (node.Pos, result.Pos) switch
    {
        _ when node.Pos.y == result.Pos.y && node.Pos.x > result.Pos.x => 'w',
        _ when node.Pos.y == result.Pos.y && node.Pos.x < result.Pos.x => 'e',
        _ when node.Pos.y < result.Pos.y && node.Pos.x == result.Pos.x => 's',
        _ when node.Pos.y > result.Pos.y && node.Pos.x == result.Pos.x => 'n',
        _ => throw new ArgumentException()
    };

    if (dir == resultDir)
        distance++;
    else
        distance = 0;

    ForwardTrace(result, resultDir, distance, isPart2);
}

var startNote = nodes.Find(n => n.Pos == start);
Console.WriteLine("\nOrder:");
ForwardTrace(startNote, 's', 0, true);

var part1 = path.Sum(x => x.Weight);
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 797 ? "yes" : "no"));

// Answer for part 2 914


// Visuals
for (var y = 0; y < lines.Count; y++)
{
    for (var x = 0; x < lines[0].Count; x++)
    {
        var node = nodes.FirstOrDefault(n => n.Pos == (y, x))!;
        if (path.Contains(node))
            Console.Write('#');
        else
            Console.Write(node.Weight);
    }
    Console.WriteLine();
}


watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");

}


record Node((int y, int x) Pos, int Weight, int Distance)
{
    public int Score { get; set; } = int.MaxValue;
    public bool Visited { get; set; } = false;
}