string input = File.ReadAllText("../../../File.txt");

// Part1 is 4, part2 is 14
const int numberOfDistinctChars = 14;
const int startPos = numberOfDistinctChars-1;

for (var i = startPos; i < input.Length; i++)
{
    string group = input[(i)..(i + numberOfDistinctChars)];
    if (group.ToCharArray().GroupBy(x => x).All(g => g.Count() == 1))
    {
        Console.WriteLine(i+startPos+1);
        break;
    }
        
}