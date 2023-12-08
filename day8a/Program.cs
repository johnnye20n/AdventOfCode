var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day8a\input.txt");

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

var found = false;
var current = map.First(x => x.Key == "AAA");

do
{
    foreach (var instruction in instructions)
    {

        steps++;

        var next = instruction == 'L' ? current.Value.Select(y => y.Key).First() : current.Value.Select(y => y.Value).First();
        if (next== "ZZZ")
        {
            found = true;
            break;
        }

        current = map.First(x => x.Key == next);
    }
} while (found == false);

Console.WriteLine(steps);