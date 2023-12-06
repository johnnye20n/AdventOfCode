var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day6a\input.txt");

var time = input[0].Split(":")[1];
var times = time.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

var distance = input[1].Split(":")[1];
var distances = distance.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(); ;

var totalwins = new List<int>();
for (var i = 0; i < times.Count; i++)
{
    var wins = 0;
    for (var milli = 0; milli < times[i] - 1; milli++)
    {
        var currdistance = milli * (times[i] - milli);

        if (currdistance > distances[i])
        {
            wins++;
        }
    }
    totalwins.Add(wins);
}


Console.WriteLine(totalwins.Aggregate((x, y) => x * y));