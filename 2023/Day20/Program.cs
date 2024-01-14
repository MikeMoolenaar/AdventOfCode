var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();

Dictionary<string, Module> dict;
long Execute(bool part2, string? breakWhenOutputHigh = null)
{
    dict = new();

    foreach (var line in lines)
    {
        var split = line.Split(" -> ");
        string key;
        char type;
        var dests = split[1].Split(", ");

        if (split[0] == "broadcaster")
        {
            key = "broadcaster";
            type = 'b';
        }
        else
        {
            key = split[0][1..];
            type = split[0][0];
        }
        
        dict.Add(key, new Module
        {
            Name = key,
            Type = type, 
            Status = 'l', 
            Dests = dests,
            Highs = 0,
            Lows = 0
        });
    }

    foreach (var dest in dict.Values.SelectMany(x => x.Dests).ToList())
    {
        dict.TryAdd(dest, new Module
        {
            Name = dest,
            Type = ' ',
            Status = 'l',
            Dests = [],
            Highs = 0,
            Lows = 0
        });
    }

    foreach (var (_, module) in dict)
    {
        if (module.Type != '&') continue;
        foreach (var (k, _) in dict.Where(x => x.Value.Dests.Contains(module.Name))) 
            module.Inputs.TryAdd(k, 'l');
       
    }

    long buttonPushes = part2 ? long.MaxValue : 1_000;
    for (long press = 0; press < buttonPushes; press++)
    {
        // introduce some sort of priority queue
        Queue<string> queue = new();
        queue.Enqueue("broadcaster");
        
        while (queue.TryDequeue(out var key))
        {
            var module = dict[key];

            var statusNew = module.Type == 'b' ? 'l' : module.Status;
            foreach (var s in module.Dests) 
                dict[s].SetStatus(statusNew, (key, module));

            if (breakWhenOutputHigh is not null && dict[breakWhenOutputHigh].Status == 'h')
                return press+1; // press starts at 0 so we must do +1

            foreach (var dest in module.Dests)
            {
                if (dict[dest].SignalSent)
                    queue.Enqueue(dest);
            }
        }
    }

    if (!part2)
    {
        long totalLows = dict.Values.Sum(x => x.Lows) + buttonPushes;
        long totalHighs = dict.Values.Sum(x => x.Highs);

        return totalLows * totalHighs;
    }
    throw new ArgumentException("Woopsie this should NOT happen!");

}

// ============== Part 1
var part1 = Execute(false);
Console.WriteLine("Part 1: " + part1);
Console.WriteLine("Correct: " + (part1 == 873301506 ? "yes" : "no"));

// ============== Part 2
Module moduleRxActivator = dict.Values.First(x => x.Dests.Contains("rx"));
List<Module> moduleInputs = dict.Values.Where(x => x.Dests.Contains(moduleRxActivator.Name)).ToList();

List<long> presses = [];
foreach (var moduleInput in moduleInputs) 
    presses.Add(Execute(true, moduleInput.Name));

var part2 = CalculateLcmFromList(presses);
Console.WriteLine("Part 2: " + part2);
Console.WriteLine("Correct: " + (part2 == 241823802412393 ? "yes" : "no"));

// ============== Other stuff
watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");

// Stolen from https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
static long CalculateLcmFromList(List<long> numbers) => 
    numbers.Aggregate(Lcm);
static long Lcm(long a, long b) => 
    Math.Abs(a * b) / Gcd(a, b);
static long Gcd(long a, long b) => 
    b == 0 ? a : Gcd(b, a % b);

record Module
{
    public required string Name { get; init; }
    public required char Type { get; init; }

    public required char Status { get; set; }
    public required string[] Dests { get; init; }
    
    public Dictionary<string, char> Inputs = new();
    public int Highs;
    public int Lows;
    public bool SignalSent = true;

    public void SetStatus(char value, (string key, Module module) source)
    {
        if (value == 'h')
            Highs++;
        else
            Lows++;

        switch (Type)
        {
            case '&':
                Inputs[source.key] = value;
                Status = Inputs.Values.All(x => x == 'h') ? 'l' : 'h';
                break;
            case '%' when value == 'l':
                Status = Status is 'l' ? 'h' : 'l';
                SignalSent = true;
                break;
            case '%':
                SignalSent = false;
                break;
            default:
                Status = value;
                break;
        }
    }
}