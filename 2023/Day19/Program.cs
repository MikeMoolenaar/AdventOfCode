using System.Text.RegularExpressions;
var watch = System.Diagnostics.Stopwatch.StartNew();

var lines = File.ReadLines("Input.txt").ToList();
var split = lines.FindIndex(x => x == string.Empty);

var functionsRaw = lines[..split];
var partsRaw = lines[(split + 1)..];

var functions = new Dictionary<string, string>(functionsRaw.Count);
foreach (var s in functionsRaw)
{
    var lineParts = s.Split('{');
    functions.Add(lineParts[0], lineParts[1][..^1]);
}

// ============== Part 1
var parts = new List<Part>(partsRaw.Count);
foreach (var s in partsRaw)
{
    var result = Regex.Matches(s, @"\d+");
    
    parts.Add(new Part(int.Parse(result[0].Value), 
        int.Parse(result[1].Value),
        int.Parse(result[2].Value),
        int.Parse(result[3].Value)));
}

bool Function(string functionName, Part part)
{
    if (functionName == "R")
        return false;
    if (functionName == "A")
        return true;
    
    foreach (var expression in functions[functionName].Split(','))
    {
        if (!expression.Contains(':'))
        {
            // Else statement
            return Function(expression, part);
        }

        // [ variable, rightValue, functionIfTrue ]
        // [ m, 20, ps ]
        var expParts = expression.Split('<', '>', ':');
        
        var left = expParts[0] switch
        {
            "x" => part.X,
            "m" => part.M,
            "a" => part.A,
            "s" => part.S,
            _ => throw new ArgumentException($"Could not parse variable {expParts[0]}")
        };
        var right = int.Parse(expParts[1]);

        if (expression.Contains('<') ? left < right : left > right)
            return Function(expParts[2], part);
    }
    
    throw new ArgumentException($"Could not process function {functionName}|{functions[functionName]}");
}

var part1 = 0;
foreach (var part in parts)
{
    var accepted = Function("in", part);
    if (accepted)
        part1 += part.Sum();
}
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 319295 ? "yes" : "no"));

// ============== Part 2
const int max = 4000;
var partRanges = new PartRange((1, max), (1, max), (1, max), (1, max));

List<PartRange> ranges = new();
void FunctionPart2(string functionName, PartRange range)
{
    if (functionName == "R") 
        return;
    if (functionName == "A")
    {
        ranges.Add(range);
        return;
    }

    foreach (var expression in functions[functionName].Split(','))
    {
        if (!expression.Contains(':'))
        {
            // Else statement
            FunctionPart2(expression, range);
            return;
        }

        // [ variable, rightValue, functionIfTrue ]
        // [ m, 20, ps ]
        var expParts = expression.Split('<', '>', ':');
        
        var right = int.Parse(expParts[1]);
        switch (expParts[0])
        {
            case "x":
                if (expression.Contains('<'))
                {
                    FunctionPart2(expParts[2], range with { X = (range.X.min, right - 1) });
                    range.X = (right, range.X.max);
                }
                else
                {
                    FunctionPart2(expParts[2], range with { X = (right+1, range.X.max) });
                    range.X = (range.X.min, right);
                }
                break;
            case "m":
                if (expression.Contains('<'))
                {
                    FunctionPart2(expParts[2], range with { M = (range.M.min, right - 1) });
                    range.M = (right, range.M.max);
                }
                else
                {
                    FunctionPart2(expParts[2], range with { M = (right+1, range.M.max) });
                    range.M = (range.M.min, right);
                }
                break;
            case "a":
                if (expression.Contains('<'))
                {
                    FunctionPart2(expParts[2], range with { A = (range.A.min, right - 1) });
                    range.A = (right, range.A.max);
                }
                else
                {
                    FunctionPart2(expParts[2], range with { A = (right+1, range.A.max) });
                    range.A = (range.A.min, right);
                }
                break;
            case "s":
                if (expression.Contains('<'))
                {
                    FunctionPart2(expParts[2], range with { S = (range.S.min, right - 1) });
                    range.S = (right, range.S.max);
                }
                else
                {
                    FunctionPart2(expParts[2], range with { S = (right+1, range.S.max) });
                    range.S = (range.S.min, right);
                }
                break;
        }
    }
    
    throw new ArgumentException($"Could not process function {functionName}|{functions[functionName]}");
}
FunctionPart2("in", partRanges);

var part2 = ranges.Sum(r => r.Possibilities());
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 110807725108076 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");

record Part(int X, int M, int A, int S)
{
    public int Sum() => X + M + A + S;
}

record PartRange((int min, int max) X, (int min, int max) M, (int min, int max) A, (int min, int max) S)
{
    public (int min, int max) X { get; set; } = X;
    public (int min, int max) M { get; set; } = M;
    public (int min, int max) A { get; set; } = A;
    public (int min, int max) S { get; set; } = S;
    public long Possibilities() => ((long)X.max - X.min + 1) * ((long)M.max - M.min+1) * ((long)A.max - A.min+1) * ((long)S.max - S.min+1);
}