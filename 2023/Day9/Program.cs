var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

int Calculate(bool reverseHistory)
{
    List<List<int>> histories = lines.Select(
            x =>
            {
                var temp = x
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList();
                if (reverseHistory)
                    temp.Reverse();
                return temp;
            })
        .ToList();

    var sum = 0;
    foreach (var history in histories)
    {
        List<int> historyNew = [];
        List<int> historyTemp = history;
        List<int> lastValues = [history.Last()];

        while (historyTemp.Any(x => x != 0))
        {
            historyNew.Clear();
            for (var i = 0; i < historyTemp.Count-1; i++)
            {
                historyNew.Add( historyTemp[i+1] - historyTemp[i]);
            }

            lastValues.Add(historyNew.Last());
            historyTemp = historyNew[..];
        }

        lastValues.Reverse();
        sum += lastValues.Aggregate((a,b) => a + b);
    }

    return sum;
}

// ============== Part 1
var part1 = Calculate(false);
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 1992273652 ? "yes" : "no"));

// ============== Part 2
var part2 = Calculate(true);
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 1012 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");