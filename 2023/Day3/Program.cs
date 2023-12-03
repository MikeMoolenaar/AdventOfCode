using System.Text.RegularExpressions;

var lines = File.ReadAllLines("Input.txt").ToList();
var lineLength = lines[0].Length;

string GetLinePart(int lineNr, int pos, int length)
{
    if (lineNr < 0 || lineNr >= lines.Count)
        return string.Empty;

    var pos1 = Math.Max(pos - 1, 0);
    var pos2 = Math.Min(pos + length + 1, lineLength);

    return lines[lineNr][pos1..pos2];
}

// ============== Part 1
var total = 0;
for (var i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    var numberString = Regex.Matches(line, @"\d+");
    foreach (Capture capture in numberString)
    {
        var lineAbove = GetLinePart(i - 1, capture.Index, capture.Length);
        var lineBelow = GetLinePart(i + 1, capture.Index, capture.Length);

        string left = string.Empty;
        if (capture.Index != 0)
            left = line[capture.Index - 1].ToString();

        string right = string.Empty;
        if (lineLength > capture.Index + capture.Length)
            right = line[capture.Index + capture.Length].ToString();

        var concat = Regex.Replace(lineAbove + lineBelow + left + right, 
            @"[\d.]", 
            string.Empty);

        if (concat.Length != 0) 
            total += Convert.ToInt32(capture.Value);

    }
}

Console.WriteLine();
Console.WriteLine("Part 1: " + total);
Console.WriteLine("Correct: " + (total == 560670 ? "Yes": "No"));

// ============== Part 2
var dict = new Dictionary<string, List<int>>();
for (var i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    var numberString = Regex.Matches(line, @"\d+");
    
    foreach (Capture capture in numberString)
    {
        var lineAbove = GetLinePart(i - 1, capture.Index, capture.Length);
        var lineBelow = GetLinePart(i + 1, capture.Index, capture.Length);

        string left = string.Empty;
        if (capture.Index != 0)
            left = line[capture.Index - 1].ToString();

        string right = string.Empty;
        if (lineLength > capture.Index + capture.Length)
            right = line[capture.Index + capture.Length].ToString();

        var correction = -1 - (capture.Index == 0 ? -1 : 0);
        string? key = null;
        if (lineAbove.Contains('*'))
            key = $"{i - 1}:{capture.Index + lineAbove.IndexOf('*') + correction}";
        else if (lineBelow.Contains('*'))
            key = $"{i + 1}:{capture.Index + lineBelow.IndexOf('*') + correction}";
        else if (left.Contains('*'))
            key = $"{i}:{capture.Index - 1}";
        else if (right.Contains('*'))
            key = $"{i}:{capture.Index + capture.Length}";
        
        if (key is not null)
        {
            var number = Convert.ToInt32(capture.Value);
            if (dict.ContainsKey(key)) 
                dict[key].Add(number);
            else 
                dict.Add(key, new List<int> { number });
        }
    }
}

var totalGearRatio = dict.Values
    .Where(x => x.Count == 2)
    .Select(x => x[0] * x[1])
    .Sum();

Console.WriteLine();
Console.WriteLine("Part 2: " + totalGearRatio);
Console.WriteLine("Correct: " + (totalGearRatio == 91622824 ? "Yes": "No"));
