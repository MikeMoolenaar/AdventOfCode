string content = File.ReadAllText("../../../Test.txt");
var lines = content.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

var baseList = Enumerable.Repeat('.', 100).ToList();
List<List<char>> positions = Enumerable.Repeat(baseList, 100).ToList();

(int x, int y) posTail = (0,0);
(int x, int y) posHead = (0,0);
var tailVisitedPositions = new HashSet<(int x, int y)>();

void PrintPos()
{
    Console.WriteLine();
    for (var y = 0; y < positions.Count; y++)
    {
        for (var x = 0; x < positions[y].Count; x++)
        {
            if (posHead == (x,y))
                Console.Write('H');
            else if (posTail == (x,y))
                Console.Write('T');
            else
                Console.Write(positions[y][x]);
        }
        Console.WriteLine();
    }
}

bool TailShouldMove()
{
    bool shouldMoveInSameDirection =  Math.Abs(posHead.x - posTail.x) > 1 || Math.Abs(posHead.y - posTail.y) > 1;
    if (Math.Abs(posHead.y - posTail.y) == 2)
        posTail.x = posHead.x;
    else if (Math.Abs(posHead.x - posTail.x) == 2)
        posTail.y = posHead.y;
    return shouldMoveInSameDirection;
}

PrintPos();
foreach (var line in lines)
{
    char dir = line[0];
    int steps = Convert.ToInt32(line[2..]);
    
    Console.WriteLine($"============> {dir} {steps}");

    for (int i = 0; i < steps; i++)
    {
        switch (dir)
        {
            case 'U':
                posHead.y--;
                if (TailShouldMove()) posTail.y--;
                break;
            case 'R':
                posHead.x++;
                if (TailShouldMove()) posTail.x++;
                break;
            case 'D':
                posHead.y++;
                if (TailShouldMove()) posTail.y++;
                break;
            case 'L':
                posHead.x--;
                if (TailShouldMove()) posTail.x--;
                break;
            default:
                throw new InvalidDataException($"{dir} is not a valid direction");
        }

        tailVisitedPositions.Add(posTail);
        PrintPos();
    }
}

Console.WriteLine($"Positions visited by tail: {tailVisitedPositions.Count}");