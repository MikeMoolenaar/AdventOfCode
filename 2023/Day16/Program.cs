using System.Diagnostics;

var watch = Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();
List<char> dirs = ['u', 'r', 'd', 'l'];

char NextDir(char dir, int turnBy)
{
    var index = dirs.IndexOf(dir);
    return turnBy switch
    {
        1 when index == 3 => dirs[0],
        -1 when index == 0 => dirs[3],
        _ => dirs[index + turnBy]
    };
}

(int y, int x) NextPos((int y, int x) pos, char dir)
{
    switch (dir)
    {
        case 'u':
            pos.y--;
            break;
        case 'r':
            pos.x++;
            break;
        case 'd':
            pos.y++;
            break;
        case 'l':
            pos.x--;
            break;
    }

    return pos;
}

HashSet<(int y, int x)> visited = [];
HashSet<(int y, int x)> visitedSplits = [];
void ProcessStep((int y, int x) pos, char dir)
{
    // Range check
    if (pos.y >= lines.Length || pos.x >= lines[0].Length || pos.x < 0 || pos.y < 0)
        return;
    
    var thisChar = lines[pos.y][pos.x];
    visited.Add(pos);

#if DEBUG
    Console.WriteLine($"y={pos.y},x={pos.x},dir={dir},curChar={thisChar}");
    for (var y = 0; y < lines.Length; y++)
    {
        for (var x = 0; x < lines[0].Length; x++)
        {
            char curChar = lines[y][x];
            Console.Write(curChar == '.' && visited.Contains((y, x)) ? '#' : curChar);
        }

        Console.WriteLine();
    }

    Console.WriteLine();
#endif
    switch (thisChar)
    {
        case '\\':
        {
            var nextDir = NextDir(dir, dir is 'r' or 'l' ? 1 : -1);
            ProcessStep(NextPos(pos, nextDir), nextDir);
            break;
        }
        case '/':
        {
            var nextDir = NextDir(dir, dir is 'u' or 'd' ? 1 : -1);
            ProcessStep(NextPos(pos, nextDir), nextDir);
            break;
        }
        case '|' when dir is 'l' or 'r':
        case '-' when dir is 'u' or 'd':
        {
            if (!visitedSplits.Add(pos)) return; // Split already visited, return
            
            var nextDir = NextDir(dir, 1);
            ProcessStep(NextPos(pos, nextDir), nextDir);
            
            nextDir = NextDir(dir, -1);
            ProcessStep(NextPos(pos, nextDir), nextDir);
            break;
        }
        case '.' or '|' or '-':
            ProcessStep(NextPos(pos, dir), dir);
            break;
        default:
            throw new ArgumentException("Unknown char " + thisChar);
    }
}

int CalculateCoverage(int initialY, int initialX, char initialDir) 
{
    visited.Clear();
    visitedSplits.Clear();
    ProcessStep((initialY, initialX), initialDir);

    return visited.Count;
}

// ============== Part 1
var part1 = CalculateCoverage(0, 0, 'r');
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 6605 ? "yes" : "no"));

// ============== Part 2
List<(int y, int x, char dir)> positions = [];
int maxX = lines[0].Length;
int maxY = lines.Length;
for (var x = 0; x < maxX; x++)
{
    positions.Add((0, x, 'd'));
    positions.Add((maxY-1, x, 'u'));
}

for (var y = 0; y < maxY; y++)
{
    positions.Add((y, 0, 'r'));
    positions.Add((y, maxX-1, 'l'));
}

int part2 = 0;
foreach (var (y, x, dir) in positions)
{
    var result = CalculateCoverage(y, x, dir);
    if (part2 < result)
        part2 = result;
}

Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 6766 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");