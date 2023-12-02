using System.Text.RegularExpressions;

var input = File.ReadAllLines("Input.txt").ToArray();

const int cubesRed = 12;
const int cubesGreen = 13;
const int cubesBlue = 14;

// ============== Part 1
int totalId = 0;
foreach (var line in input)
{
    var reg = Regex.Match(line, @"Game (\d+): (.*)");
    var gameId = Convert.ToInt32(reg.Groups[1].Value);
    var gameValue = reg.Groups[2].Value;

    var dict = new Dictionary<string, List<int>>
    {
        { "red", new() },
        { "green", new() },
        { "blue", new() }
    };

    foreach (var gameStep in gameValue
                 .Split(';', StringSplitOptions.TrimEntries)
                 .SelectMany(x => x.Split(',', StringSplitOptions.TrimEntries)))
    {
        var regGame = Regex.Match(gameStep, @"(\d+) (.*)");
        var count = Convert.ToInt32(regGame.Groups[1].Value);
        var color = regGame.Groups[2].Value;
        dict[color].Add(count);
    }

    var redPossible = cubesRed >= dict["red"].Max();
    var greenPossible = cubesGreen >= dict["green"].Max();
    var bluePossible = cubesBlue >= dict["blue"].Max();

    if (redPossible && greenPossible && bluePossible)
        totalId += gameId;
}

Console.WriteLine();
Console.WriteLine("Part1: "+ totalId);

// ============== Part 2
var totalPower = 0;
foreach (var line in input)
{
    var reg = Regex.Match(line, @"Game (\d+): (.*)");
    var gameValue = reg.Groups[2].Value;

    var dict = new Dictionary<string, List<int>>
    {
        { "red", new() },
        { "green", new() },
        { "blue", new() }
    };

    foreach (var gameStep in gameValue
                 .Split(';', StringSplitOptions.TrimEntries)
                 .SelectMany(x => x.Split(',', StringSplitOptions.TrimEntries)))
    {
        var regGame = Regex.Match(gameStep, @"(\d+) (.*)");
        var count = Convert.ToInt32(regGame.Groups[1].Value);
        var color = regGame.Groups[2].Value;
        dict[color].Add(count);
    }

    totalPower += dict["red"].Max() * dict["green"].Max() * dict["blue"].Max();
}

Console.WriteLine();
Console.WriteLine("Part2: " + totalPower);