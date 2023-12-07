using Day7;

var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

int Calculate(bool isPart2 = false)
{
    List<Game> games = new();
    foreach (var line in lines)
    {
        var parts = line.Split(" ");
        games.Add(new Game(parts[0].ToCharArray().ToList(), int.Parse(parts[1]), isPart2));
    }

    // Game implements CompareTo
    games.Sort();

    var score = 0;
    for (var i = games.Count-1; i >= 0; i--)
    {
        var game = games[i];
        score += (i + 1) * game.Bid;
    }

    return score;
}

var part1 = Calculate();
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 255048101 ? "yes" : "no"));

var part2 = Calculate(true);
Console.WriteLine("Part 1: " + part2);
Console.WriteLine("Correct: " + (part2 == 253718286 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");