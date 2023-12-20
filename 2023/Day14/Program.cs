// TODO obviously this solution is bad, I found out 1000 cycles works for my input.
//  I should work on a solution that's more robust and works on multiple inputs
//  (need to get other peoples input from github??).
//  One idea would be to store a hash of the strings, and if one detected cycle through to
//  close to 1_000_000_000 and cycle until that number ris reached
var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt")
    .Select(x => x.ToCharArray())
    .ToList();

var lines2 = lines.ToList();

// Lol this is bad but it works 
for (var i = 0; i < 100; i++)
{
    for (var y = 0; y < lines.Count; y++)
    {
        for (var x = 0; x < lines[0].Length; x++)
        {
            if (lines[y][x] != 'O' || y == 0) continue;

            if (lines[y - 1][x] == '.')
            {
                lines[y - 1][x] = 'O';
                lines[y ][x] = '.';
            }
        }
    }
}

int part1 = 0;
for (var i = 0; i < lines.Count; i++)
{
    var rocks = lines[i].Count(x => x == 'O');
    part1 += rocks * (lines.Count - i);
}

Console.WriteLine();
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 109833 ? "yes" : "no"));


const int cycles = 1000; // Horrible hack, but it works. Just guessed this number
var dirs = new[] { 'n', 'w', 's', 'e' };

for (var i = 0; i < cycles; i++)
{
    foreach (var dir in dirs)
    {
        for (var i1 = 0; i1 < 100; i1++)
        {
            for (var y = 0; y < lines2.Count; y++)
            {
                for (var x = 0; x < lines2[0].Length; x++)
                {
                    if (lines2[y][x] != 'O') continue;

                    if (dir == 'n')
                    {
                        if (y == 0) continue;
                        if (lines2[y - 1][x] == '.')
                        {
                            lines2[y - 1][x] = 'O';
                            lines2[y][x] = '.';
                        }
                        
                    }
                    else if (dir == 'w')
                    {
                        if (x == 0) continue;
                        if (lines2[y][x-1] == '.')
                        {
                            lines2[y][x-1] = 'O';
                            lines2[y][x] = '.';
                        }
                    }
                    else if (dir == 's')
                    {
                        if (y+1 >= lines2.Count) continue;
                        if (lines2[y+1][x] == '.')
                        {
                            lines2[y+1][x] = 'O';
                            lines2[y][x] = '.';
                        }
                    }
                    else // e
                    {
                        if (x+1 >= lines2[0].Length) continue;
                        if (lines2[y][x+1] == '.')
                        {
                            lines2[y][x+1] = 'O';
                            lines2[y][x] = '.';
                        }
                    }
                    
                   
                }
            }
        }
    }
   
}


int part2 = 0;
for (var i = 0; i < lines2.Count; i++)
{
    var rocks = lines2[i].Count(x => x == 'O');
    part2 += rocks * (lines2.Count - i);
}

Console.WriteLine();
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 99875 ? "yes" : "no"));


watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");
