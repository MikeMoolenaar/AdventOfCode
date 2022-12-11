using Path = Day7.Path;
using Directory = Day7.Directory;
using File = Day7.File;

string input = System.IO.File.ReadAllText("../../../File.txt");
string[] lines = input.Split('\n');

var root = new Directory(string.Empty);

List<string> pathPointer = new();
void Add(Path path, Directory? dir = null, List<string> tempPath = null)
{
    if (dir is null) dir = root;
    if (tempPath is null)
    {
        tempPath = pathPointer.ToArray().ToList(); // Clone list
    }

    if (!tempPath.Any())
    {
        dir.Paths.Add(path);
        return; // Stop recusrion
    }
    
    Directory pathTemp = (dir.Paths.First(x => x is Directory && x.name == tempPath[0]) as Directory)!;
    tempPath.RemoveAt(0);
    Add(path, pathTemp, tempPath);
}

const int startPos = 1; // Ignore first command: cd /;
int x = startPos; 
while (x < lines.Length-startPos)
{
    // Command
    if (lines[x].StartsWith("$ cd"))
    {
        string folderName = lines[x].Split(' ')[2];
        if (folderName == "..")
        {
            pathPointer.RemoveAt(pathPointer.Count-1);
        }
        else
        { 
            pathPointer.Add(folderName);
        }
        
        x++;
    }
    else if (lines[x].StartsWith("$ ls"))
    {
        List<string> outputLines = new();
        while (x++ < lines.Length)
        {
            if (lines[x].StartsWith("$") || string.IsNullOrWhiteSpace(lines[x])) break;
            outputLines.Add(lines[x]);
        }
        
        foreach (var outputLine in outputLines)
        {
            if (outputLine.StartsWith("dir"))
            {
                Add(new Directory(outputLine.Replace("dir ", "")));
            }
            else // Contains file
            {
                int size = Convert.ToInt32(outputLine.Split(' ')[0]);
                string name = outputLine.Split(' ')[1];
                
                Add(new File(name, size));
            }
        }
    }
    else
        throw new ArgumentOutOfRangeException($"{lines[x]} cannot be parsed");
}

int totalDirSizesUnder100k = 0;

void CalculateDirSize(Directory dir)
{
    foreach (Directory dirr in dir.Paths.OfType<Directory>())
    {
        int size = dirr.CalcSize();
        if (size <= 100_000)
            totalDirSizesUnder100k += size;
        CalculateDirSize(dirr);
    }
}
CalculateDirSize(root);
Console.WriteLine(totalDirSizesUnder100k); // Part 1

// Part 2 ===================================================
List<(int, string)> dirSizes = new();
void IndexDirSizes(Directory dir)
{
    foreach (Directory dirr in dir.Paths.OfType<Directory>())
    {
        dirSizes.Add(new(dirr.CalcSize(), dirr.name));
        IndexDirSizes(dirr);
    }
}
IndexDirSizes(root);

int freeSpace = 70000000 - root.CalcSize();
int requiredMinSpaceLeft = 30000000 - freeSpace;

var dir = dirSizes.Where(x => x.Item1 >= requiredMinSpaceLeft).OrderBy(x => x.Item1).First();
Console.WriteLine($"{dir.Item1} {dir.Item2}"); // Part 2
