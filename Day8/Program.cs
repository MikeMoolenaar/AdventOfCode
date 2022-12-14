string content = File.ReadAllText("../../../File.txt");

var lines = content.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

int alwaysVisible = lines[0].Length*2 + (lines.Count - 2)*2;

string Reverse(string s) => new(s.Reverse().ToArray());

List <(int, int)> seenCoords = new();
void checkIfCanSee(string trees, (int,int) coords)
{
    trees = trees.Replace("0", "");
    int currentTree = Convert.ToInt32(lines[coords.Item1][coords.Item2].ToString());
    if (currentTree == 0) return;
    var lsInt = trees.Select(x => Convert.ToInt32(x.ToString())).ToList();
    var lsIntSorted = lsInt.OrderDescending();
    if (lsInt.Count == 0 ||
        (lsInt.SequenceEqual(lsIntSorted) && lsInt.First() < currentTree))
        seenCoords.Add(coords);
}


for (int rowi = 1; rowi < lines.Count - 1; rowi++)
{
    for (int coli = 1; coli < lines[0].Length - 1; coli++)
    {
        // Left - right & right - left
        checkIfCanSee(lines[rowi][..(coli)], (rowi,coli));
        checkIfCanSee(lines[rowi][(coli+1)..], (rowi,coli));
        // Top - bottom & bottom - top
        string completeCOl = string.Empty;
        foreach (var t in lines)
            completeCOl += t[coli];

        checkIfCanSee(completeCOl[..(rowi)], (rowi,coli));
        checkIfCanSee(completeCOl[(rowi+1)..], (rowi,coli));
    }
}

int internalVisible = seenCoords.Select(x => $"{x.Item1}{x.Item2}")
    .GroupBy(x => x)
    .Count();

Console.WriteLine();
Console.WriteLine($"{alwaysVisible} + {internalVisible}");
Console.WriteLine(alwaysVisible + internalVisible);