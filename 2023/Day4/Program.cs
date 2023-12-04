using System.Text.RegularExpressions;

var lines = File.ReadLines("Input.txt").ToList();
Regex gameRegex = new(@"Card\W+(\d+): ([\d ]+)\|([\d ]+)");

// ============== Part 1
var points = 0;
foreach (var line in lines)
{
    var regex = gameRegex.Match(line);
    
    var winningNumbers = regex.Groups[2].Value.Split(" ").Where(x => x != "").ToList();
    var myNumbers = regex.Groups[3].Value.Split(" ").Where(x => x != "").ToList();

    var gamePoints = 0;
    foreach (var myNumber in myNumbers)
    {
        if (winningNumbers.Contains(myNumber)) 
            gamePoints += gamePoints == 0 ? 1 : gamePoints;
    }

    points += gamePoints;
}

Console.WriteLine("Part 1: " + points);
Console.WriteLine("Correct: " + (points == 23028 ? "Yes": "No"));
Console.WriteLine();

// ============== Part 2
var dict = new Dictionary<int, int>();
for (var i = 1; i <= lines.Count; i++)
{
    dict.Add(i, 1);
}
foreach (var line in lines)
{
    var regex = gameRegex.Match(line);
    
    var gameId = int.Parse(regex.Groups[1].Value);
    var winningNumbers = regex.Groups[2].Value.Split(" ").Where(x => x != "").ToList();
    var myNumbers = regex.Groups[3].Value.Split(" ").Where(x => x != "").ToList();

    var wins = myNumbers.Count(myNumber => winningNumbers.Contains(myNumber));
    
    for (var x = 0; x < wins; x++)
        dict[gameId + x + 1] += dict[gameId];
}

var cards = dict.Values.Sum();
Console.WriteLine("Part 2: " + cards);
Console.WriteLine("Correct: " + (cards == 9236992 ? "Yes": "No"));