string content = File.ReadAllText("../../../File.txt");
var lines = content.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

// ================ Part 1
Console.WriteLine("============ Part 1");
int alwaysVisible = lines[0].Length*2 + (lines.Count - 2)*2;

List <(int, int)> seenCoords = new();
void CheckIfCanSee(string trees, (int,int) coords)
{
    int currentTree = Convert.ToInt32(lines[coords.Item1][coords.Item2].ToString());
    var lsInt = trees.Select(x => Convert.ToInt32(x.ToString())).ToList();
    if (lsInt.All(x => x < currentTree))
        seenCoords.Add(coords);
}

for (int rowi = 1; rowi < lines.Count - 1; rowi++)
{
    for (int coli = 1; coli < lines[0].Length - 1; coli++)
    {
        // Left - right & right - left
        CheckIfCanSee(lines[rowi][..coli], (rowi,coli));
        CheckIfCanSee(lines[rowi][(coli + 1)..], (rowi,coli));
        // Top - bottom & bottom - top
        string completeCOl = string.Empty;
        foreach (var t in lines)
            completeCOl += t[coli];

        CheckIfCanSee(completeCOl[..rowi], (rowi,coli));
        CheckIfCanSee(completeCOl[(rowi + 1)..], (rowi,coli));
    }
}

var internalVisibleList = seenCoords.Select(x => $"{x.Item1} {x.Item2}")
    .Distinct()
    .ToList();

// Console.WriteLine("Found positions (x y val)");
// foreach (var s in internalVisibleList)
// {
//     var coords = (Convert.ToInt32(s.Split(' ')[0]), Convert.ToInt32(s.Split(' ')[1]));
//     Console.WriteLine($"{s} {lines[coords.Item1][coords.Item2]}");
// }

Console.WriteLine($"{nameof(alwaysVisible)} + {nameof(internalVisibleList.Count)}");
Console.WriteLine($"{alwaysVisible} + {internalVisibleList.Count} = {alwaysVisible + internalVisibleList.Count}");

// ================ Part 2
Console.WriteLine("============ Part 2");
string Reverse(string s) => new(s.Reverse().ToArray());
int CalculateViewingScore(string trees, (int,int) coords)
{
    int currentTree = Convert.ToInt32(lines[coords.Item1][coords.Item2].ToString());
    var lsInt = trees.Select(x => Convert.ToInt32(x.ToString())).ToList();

    int i = 0;
    foreach (var i1 in lsInt)
    {
        i++;
        if (i1 >= currentTree) break;
    }
    return i;
}
List<((int,int),int)> treeScores = new();
for (int rowi = 1; rowi < lines.Count - 1; rowi++)
{
    for (int coli = 1; coli < lines[0].Length - 1; coli++)
    {
        // Left - right & right - left
        var x1 = CalculateViewingScore(Reverse(lines[rowi][..coli]), (rowi,coli));
        var x2 = CalculateViewingScore(lines[rowi][(coli + 1)..], (rowi,coli));
        // Top - bottom & bottom - top
        string completeCOl = string.Empty;
        foreach (var t in lines)
            completeCOl += t[coli];

        var x3 = CalculateViewingScore(Reverse(completeCOl[..rowi]), (rowi,coli));
        var x4 = CalculateViewingScore(completeCOl[(rowi + 1)..], (rowi,coli));
        
        treeScores.Add(((rowi,coli), x1*x2*x3*x4));
    }
}

void LogTreeScore(((int,int),int) treeScore) => 
    Console.WriteLine($"{treeScore.Item1.Item1} {treeScore.Item1.Item2}: {treeScore.Item2}");

// Console.WriteLine("All:");
// foreach (var valueTuple in treeScores)
//     LogTreeScore(valueTuple);

Console.WriteLine("Highest one (x y score)");
LogTreeScore(treeScores.MaxBy(x => x.Item2));