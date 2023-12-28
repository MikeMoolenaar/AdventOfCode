using System.Collections;
using System.Diagnostics;

var watch = Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").Select(x => 
        x.Select(c => int.Parse(c.ToString())).ToList())
    .ToList();
var height = lines.Count;
var width = lines[0].Count;

var heightMax = height - 1;
var widthMax = width - 1;

var states = new List<Node>(height*width);
var statesSeen = new Hashtable(height*width);
int? finalCost;

void MoveAndAddState(int cost, int x, int y, int dy, int dx, int distance)
{
    x += dx;
    y += dy;

    if (x < 0 || y < 0 || x >= width || y >= height)
        return;

    var newCost = cost + lines[y][x];

    if (x == widthMax && y == heightMax)
    {
        finalCost = newCost;
        return;
    }

    var stateSeen = new NodeSeen((y, x), (dy, dx), distance);
    if (!statesSeen.ContainsKey(stateSeen))
    {
        statesSeen.Add(stateSeen, null);
        states.Add(new Node((y, x), (dy, dx), newCost, distance));
    }
}

int CalculateCoverage(bool isPart2)
{
    states.Clear();
    statesSeen.Clear();
    finalCost = null;
    
    MoveAndAddState(0, 0, 0, 0, 1, 1); // To east
    MoveAndAddState(0, 0, 0, 1, 0, 1); // To south
    
    while (finalCost is null)
    {
        var currentCost = states.MinBy(n => n.Cost)!.Cost;
        var statesToProcess = states.Where(n => n.Cost == currentCost).ToArray();
        states.RemoveAll(n => n.Cost == currentCost);
        
        foreach (var (pos, dir, cost, distance) in statesToProcess)
        {
            if (isPart2)
            {
                if (distance > 3)
                {
                    MoveAndAddState(cost, pos.x, pos.y, dir.dx*-1, dir.dy, 1);
                    MoveAndAddState(cost, pos.x, pos.y, dir.dx, dir.dy*-1, 1);
                }
            
                if (distance < 10)
                    MoveAndAddState(cost, pos.x, pos.y, dir.dy, dir.dx, distance + 1);
            }
            else
            {
                MoveAndAddState(cost, pos.x, pos.y, dir.dx*-1, dir.dy, 1);
                MoveAndAddState(cost, pos.x, pos.y, dir.dx, dir.dy*-1, 1);
            
                if (distance < 3)
                    MoveAndAddState(cost, pos.x, pos.y, dir.dy, dir.dx, distance + 1);
            }
        }
    }

    return finalCost.Value;
}

// ============== Part 1
var part1 = CalculateCoverage(false);
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 797 ? "yes" : "no"));

// ============== Part 2
var part2 = CalculateCoverage(true);
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 914 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");

record Node((int y, int x) Pos, (int dy, int dx) Dir, int Cost, int Distance);
record NodeSeen((int y, int x) Pos, (int dy, int dx) Dir, int Distance);