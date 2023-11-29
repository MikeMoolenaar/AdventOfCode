// See https://aka.ms/new-console-template for more information

string text = File.ReadAllText("../../../file.txt");

var elvesCalories = text
    .Split("\n\n")
    .Select(x => x
        .Split("\n")
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(x => Convert.ToInt32(x))
        .Sum())
    .OrderDescending().ToList();

Console.WriteLine("Highest: " + elvesCalories.First());
Console.WriteLine("Top 3: " + elvesCalories.GetRange(0, 3).Sum());