using System.Text.RegularExpressions;

var input = File.ReadAllText("../../../File.txt");

(int, int) ParseSection(string sec)
{
    var regex = new Regex(@"(\d+)\-(\d+)");

    var match = regex.Match(sec);
    return (
        Convert.ToInt16(match.Groups[1].Value),
        Convert.ToInt16(match.Groups[2].Value)
    );
}

int contains = 0;
int overlap = 0;
foreach (var line in input.Split('\n'))
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    var section1 = ParseSection(line.Split(',')[0]);
    var section2 = ParseSection(line.Split(',')[1]);

    if (section1.Item1 >= section2.Item1 && section1.Item2 <= section2.Item2 ||
        section2.Item1 >= section1.Item1 && section2.Item2 <= section1.Item2)
        contains++;

    var ls = new List<int>();
    for(var x = section1.Item1; x <= section1.Item2; x++)
        ls.Add(x);
    for(var x = section2.Item1; x <= section2.Item2; x++)
        ls.Add(x);
    ls.Sort();

    if (ls.GroupBy(x => x).Any(g => g.Count() > 1))
        overlap++;
}

Console.WriteLine(contains); // Part 1
Console.WriteLine(overlap); // Part 2