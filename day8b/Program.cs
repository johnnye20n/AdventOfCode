using System.Numerics;

var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day8b\input.txt");

var instructions = input[0].ToCharArray();

var map = new Dictionary<string, Dictionary<string, string>>();

for (var i = 2; i < input.Length; i++)
{
    var splitted = input[i].Split("=", StringSplitOptions.TrimEntries);
    var directions = splitted[1].Split(",", StringSplitOptions.TrimEntries);
    map.Add(splitted[0], new Dictionary<string, string> { { directions[0].Replace("(", ""), directions[1].Replace(")", "") } });
}


Console.WriteLine(instructions);
foreach (var kvp in map)
{
    Console.WriteLine($"{kvp.Key} {kvp.Value.Select(y => y.Key).First()} {kvp.Value.Select(y => y.Value).First()}");
}


var steps = 0;

var nodes = map
    .Where(x => x.Key.EndsWith("A"))
    .Select(x => new GhostNode
    {
        MapValue = x.Key,
        Current = x
    }).ToList();


var found = false;

do
{
    foreach (var instruction in instructions)
    {
        steps++;
        Console.WriteLine($"step {steps}");
        foreach (var ghost in nodes.Where(x => !x.IsFound))
        {
            ghost.Steps++;
            ghost.Next = instruction == 'L' ? ghost.Current.Value.Select(y => y.Key).First() : ghost.Current.Value.Select(y => y.Value).First();
            ghost.Current = map.First(x => x.Key == ghost.Next);

            ghost.DirectionPattern += instruction;
            //Console.WriteLine($"{instruction} {ghost.Current.Key}");
            //Console.WriteLine($"{ghost.Steps}");
        }

        found = nodes.All(x => x.IsFound);
    }
    //Console.WriteLine(steps);
} while (found == false);


foreach (var ghost in nodes)
{
    Console.WriteLine(ghost.Steps);
    Console.WriteLine(ghost.DirectionPattern.Length);
}

var mgn = LCM(nodes.Select(x => (long)x.Steps).ToArray());
Console.WriteLine(mgn);


//math formulas stolen from stackverflow
static long LCM(long[] numbers)
{
    return numbers.Aggregate(lcm);
}
static long lcm(long a, long b)
{
    return Math.Abs(a * b) / GCD(a, b);
}
static long GCD(long a, long b)
{
    return b == 0 ? a : GCD(b, a % b);
}
class GhostNode
{
    public string MapValue { get; set; } = "";
    public KeyValuePair<string, Dictionary<string, string>> Current { get; set; }
    public string Next { get; set; } = "";
    public bool IsFound => Next.EndsWith("Z");
    public string DirectionPattern = "";
    public int Steps { get; set; }
}