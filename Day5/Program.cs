using System.Text.RegularExpressions;

// False for Part1, True for Part2
const bool craneCanMoveMultipleAtTheSameTime = true; 

string input = File.ReadAllText("../../../File.txt");


// Stack number, crates
var dict = new Dictionary<int, List<char>>();

string[] lines = input.Split('\n');
int crateSectionLength = lines.ToList().IndexOf("");
var crateSection = lines[..(crateSectionLength)].Reverse().ToList();

// Create crate sections in the dict
foreach (var c in crateSection[0].ToCharArray())
{
    if (char.IsNumber(c))
        dict.Add(Convert.ToInt32(c.ToString()), new List<char>());
}

// Put all crates into the right section
foreach (var line in crateSection.GetRange(1, crateSection.Count - 1))
{
    for (var i = 0; i < Math.Ceiling(line.ToCharArray().Length / 4d); i++)
    {
        char crate = line[i * 4 + 1];
        if (char.IsWhiteSpace(crate)) continue;
        int keyByIndex = dict.Keys.ToArray()[i];
        dict[keyByIndex].Add(crate);
    }
}

// Execute actions
foreach (var line in lines[(crateSectionLength+1)..])
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    
    var match = new Regex(@"move (\d+) from (\d+) to (\d+)").Match(line);

    int numberOfCrates = Convert.ToInt32(match.Groups[1].Value);
    int crateSource = Convert.ToInt32(match.Groups[2].Value);
    int crateDest = Convert.ToInt32(match.Groups[3].Value);

    var ls = dict[crateSource].GetRange(dict[crateSource].Count - numberOfCrates, numberOfCrates);
    if (!craneCanMoveMultipleAtTheSameTime) 
        ls.Reverse();
    dict[crateSource].RemoveRange(dict[crateSource].Count - numberOfCrates, numberOfCrates);
    dict[crateDest].AddRange(ls);
}

string result = string.Empty;
foreach (var dictValue in dict.Values)
{
    result += dictValue[^1];
}

Console.WriteLine(result);