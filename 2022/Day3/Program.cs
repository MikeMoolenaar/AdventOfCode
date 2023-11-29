var input = File.ReadAllText("../../../File.txt");

int GetPrio(char c) =>
    c > 96 ? 
        c - 96 : // a-z
        c - 38; // A-Z

// Part 1 =============================
int prioSum = 0;
foreach (var line in input.Split('\n'))
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    
    var comp1 = line[..(line.Length / 2)];
    var comp2 = line[(line.Length / 2)..];

    char? duplicateChar = null;
    foreach (var c in comp1.ToCharArray())
    {
        if (comp2.Contains(c))
        {
            duplicateChar = c;
            break;
        }
    }

    if (duplicateChar is null) throw new ArgumentNullException(nameof(duplicateChar));
    prioSum += GetPrio((char) duplicateChar);
}

// Part 2 =============================
int groupPrioSum = 0;
string[] lines = input.Split('\n');
for (var i = 0; i < lines.Length-1; i+=3)
{
    string[] groupLines = lines[(i)..(i + 3)];

    char? groupChar = null;
    foreach (var c in groupLines[0].ToCharArray())
    {
        if (groupLines[1].Contains(c) && groupLines[2].Contains(c))
        {
            groupChar = c;
            break;
        }
    }

    if (groupChar is null) throw new ArgumentNullException(nameof(groupChar));
    groupPrioSum += GetPrio((char) groupChar);
}

Console.WriteLine(prioSum);
Console.WriteLine(groupPrioSum);

