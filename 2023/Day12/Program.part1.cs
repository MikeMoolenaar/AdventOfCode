/*
using System.Text;
using System.Text.RegularExpressions;

var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

int sum = 0;
var repeat = 1;
Parallel.ForEach(lines, line =>
{
    var rowOriginal = line.Split(' ')[0];
    var recordOriginal = line.Split(' ')[1].Split(',').Select(int.Parse).ToList();

    var row = "";
    var record = new List<int>();
    
    for (var i = 0; i < repeat; i++)
    {
        row += (i != 0 ? "?" : "") + rowOriginal;
        record.AddRange(recordOriginal);
    }
    
    Console.WriteLine(row + " " + string.Join(',', record));

    var matches = Regex.Matches(row, @"\?");

    List<string> binaryValues = new();
    int val = 1 << matches.Count;
    for (var i = 1; i < val + 1; i++)
    {
        int bin = (1 << matches.Count) - i;

        string value = Convert.ToString(bin, 2);
        if (value.Length != matches.Count)
        {
            value = new String('0', matches.Count - value.Length) + value;
        }

        binaryValues.Add(value);
    }
    
    foreach (string value in binaryValues)
    {
        var rowTest = new StringBuilder(row);
        for (var i = 0; i < matches.Count; i++)
        {
            rowTest[matches[i].Index] = value[i] == '1' ? '#' : '.';
        }

        var springMatches = Regex.Matches(rowTest.ToString(), @"#+");

        List<int> result = springMatches.Select(x => x.Length).ToList();

        if (result.SequenceEqual(record))
            sum++;
    }

});

Console.WriteLine("Part 1: " + sum);
Console.WriteLine("Correct: " + (sum == 21 ? "yes" : "no"));
// Console.WriteLine("Correct: " + (sum == 7221 ? "yes" : "no"));

watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");
*/