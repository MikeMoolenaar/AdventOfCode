string content = File.ReadAllText("../../../File.txt");

var lines = content.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

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

Console.WriteLine("Found positions (x y val)");
foreach (var s in internalVisibleList)
{
    var coords = (Convert.ToInt32(s.Split(' ')[0]), Convert.ToInt32(s.Split(' ')[1]));
    Console.WriteLine($"{s} {lines[coords.Item1][coords.Item2]}");
}

Console.WriteLine();
Console.WriteLine($"{nameof(alwaysVisible)} + {nameof(internalVisibleList.Count)}");
Console.WriteLine($"{alwaysVisible} + {internalVisibleList.Count} = {alwaysVisible + internalVisibleList.Count}");