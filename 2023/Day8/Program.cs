using System.Text.RegularExpressions;

var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

var instructions = lines[0].ToCharArray();

var dict = new Dictionary<string, (string left, string right)>();

foreach (var node in lines[2..])
{
    var matches = Regex.Matches(node, @"\w{3}");
    dict.Add(matches[0].Value, (matches[1].Value, matches[2].Value));
}

// ============== Part 1
string key = "AAA";
var stepsPart1 = 0;
for (var i = 0; i < instructions.Length; i++)
{
    if (key == "ZZZ")
        break;
    
    var node = dict[key];
    
    key = instructions[i] == 'L' ? node.left : node.right;
    stepsPart1++;
    
    if (instructions.Length == i+1)
        i = -1; // Definitely didn't take me 5 mins to figure out why i=0 was not working
    
}

Console.WriteLine("Part 1: " + stepsPart1);
Console.WriteLine("Correct: " + (stepsPart1 == 17287 ? "yes" : "no"));

// ============== Part 2
var startPositions = dict.Keys.Where(x => x.EndsWith('A')).ToList();


List<long> steps = [];
foreach (var start in startPositions)
{
    string keyPart2 = start;
    var stepsPart2 = 0;
    for (var i = 0; i < instructions.Length; i++)
    {
        if (keyPart2.EndsWith("Z"))
            break;
        
        var node = dict[keyPart2];
    
        keyPart2 = instructions[i] == 'L' ? node.left : node.right;
        stepsPart2++;
    
        if (instructions.Length == i+1)
            i = -1;
    
    }
    steps.Add(stepsPart2);
}

var part2Result = CalculateLcmFromList(steps);

Console.WriteLine("Part 2: " + part2Result);
Console.WriteLine("Correct: " + (part2Result == 18625484023687 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");

// Stolen from https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
static long CalculateLcmFromList(List<long> numbers)
{
    return numbers.Aggregate(lcm);
}
static long lcm(long a, long b)
{
    return Math.Abs(a * b) / GCD(a, b);
}
static long GCD(long a, long b)
{
    return b == 0 ? a : GCD(b, a % b);
}
