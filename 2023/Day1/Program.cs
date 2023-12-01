var lines = File.ReadLines("Input.txt").ToArray();

var globalCount = 0;
foreach (var line in lines)
{
    var ints = new List<int>();
    
    for (var i = 0; i < line.Length; i++)
    {
        var curr = line[i..];
        if (curr.StartsWith("one") || curr.StartsWith("1"))
            ints.Add(1);
        else if (curr.StartsWith("two") || curr.StartsWith("2"))
            ints.Add(2);
        else if (curr.StartsWith("three") || curr.StartsWith("3"))
            ints.Add(3);
        else if (curr.StartsWith("four") || curr.StartsWith("4"))
            ints.Add(4);
        else if (curr.StartsWith("five") || curr.StartsWith("5"))
            ints.Add(5);
        else if (curr.StartsWith("six") || curr.StartsWith("6"))
            ints.Add(6);
        else if (curr.StartsWith("seven") || curr.StartsWith("7"))
            ints.Add(7);
        else if (curr.StartsWith("eight") || curr.StartsWith("8"))
            ints.Add(8);
        else if (curr.StartsWith("nine") || curr.StartsWith("9"))
            ints.Add(9);
        
    }

    if (ints.Count == 1)
    {
        Console.WriteLine(ints[0].ToString() + ints[0]);
        globalCount += ints[0] * 10 + ints[0];
    }
    else
    {
        Console.WriteLine(ints[0].ToString() + ints.Last());
        globalCount +=  ints[0] * 10 + ints.Last();
    }
}

Console.WriteLine();
Console.WriteLine(globalCount);
