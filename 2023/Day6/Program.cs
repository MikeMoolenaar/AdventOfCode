using System.Text.RegularExpressions;

var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

// ============== Part 1
var times = Regex.Matches(lines[0], @"\d+")
    .Select(x => int.Parse(x.Value))
    .ToList();
var distances = Regex.Matches(lines[1], @"\d+")
    .Select(x => int.Parse(x.Value))
    .ToList();
List<(int time, int distance)> races = times.Select((time, x) => (time, distances[x])).ToList();

var multiplied = 1;
foreach (var (raceTime, raceDistance) in races)
{
    var raceWins = 0;
    for (var i = 0; i < raceTime; i++)
    {
        var timeLeft = raceTime - i;
        if (timeLeft * i > raceDistance)
            raceWins++;
    }

    multiplied *= raceWins;
}

Console.WriteLine("Part 1: " + multiplied);
Console.WriteLine("Correct: " + (multiplied == 32076 ? "yes" : "no"));
Console.WriteLine();

// ============== Part 2
long time = long.Parse(Regex.Replace(lines[0], @"[a-zA-Z]|:| ", string.Empty));
long distanceRecord = long.Parse(Regex.Replace(lines[1], @"[a-zA-Z]|:| ", string.Empty));

var wins = 0;
for (var i = 0; i < time; i++)
{
    var timeLeft = time - i;
    if (timeLeft * i > distanceRecord)
        wins++;
}

Console.WriteLine("Part 2: " + wins);
Console.WriteLine("Correct: " + (wins == 34278221 ? "yes" : "no"));


watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");