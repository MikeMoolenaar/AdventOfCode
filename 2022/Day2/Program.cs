// See https://aka.ms/new-console-template for more information

string text = File.ReadAllText("../../../File.txt");

Weapons Parse(char c) => c switch
{
    'A' or 'X' => Weapons.Rock,
    'B' or 'Y' => Weapons.Paper,
    'C' or 'Z' => Weapons.Scisors,
    _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
};

// PART1 =========================================
int totalScorePart1 = 0;
foreach (string line in text.Split('\n'))
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    
    Weapons opponent = Parse(line[0]);
    Weapons me = Parse(line[2]);

    int scoreShape = me switch
    {
        Weapons.Rock => 1,
        Weapons.Paper => 2,
        Weapons.Scisors => 3,
        _ => throw new ArgumentOutOfRangeException()
    };

    int scoreOutcome = (me, opponent) switch
    {
        (Weapons.Rock, Weapons.Paper) => 0,
        (Weapons.Rock, Weapons.Scisors) => 6,
        (Weapons.Paper, Weapons.Rock) => 6,
        (Weapons.Paper, Weapons.Scisors) => 0,
        (Weapons.Scisors, Weapons.Rock) => 0,
        (Weapons.Scisors, Weapons.Paper) => 6,
        _ => 3
    };

    totalScorePart1 += scoreShape + scoreOutcome;
}

// PART2 =========================================
int totalScorePart2 = 0;
foreach (string line in text.Split('\n'))
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    Weapons opponent = Parse(line[0]);
    char result = line[2];

    Weapons me = (opponent, result) switch
    {
        (Weapons.Rock, 'X') => Weapons.Scisors,
        (Weapons.Rock, 'Z') => Weapons.Paper,
        (Weapons.Paper, 'X') => Weapons.Rock,
        (Weapons.Paper, 'Z') => Weapons.Scisors,
        (Weapons.Scisors, 'X') => Weapons.Paper,
        (Weapons.Scisors, 'Z') => Weapons.Rock,
        _ => opponent // So it results in a draw
    };
    
    int scoreShape = me switch
    {
        Weapons.Rock => 1,
        Weapons.Paper => 2,
        Weapons.Scisors => 3,
        _ => throw new ArgumentOutOfRangeException()
    };
    
    int scoreOutcome = result switch
    {
        'X' => 0,
        'Y' => 3,
        'Z' => 6,
        _ => throw new ArgumentOutOfRangeException()
    };

    totalScorePart2 += scoreShape + scoreOutcome;
}

// PRINT RESULTS =========================================
Console.WriteLine(totalScorePart1);
Console.WriteLine(totalScorePart2);

enum Weapons { Rock,Paper,Scisors }