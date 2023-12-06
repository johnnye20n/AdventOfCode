var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day6b\input.txt");

var time = long.Parse(input[0].Split(":")[1].Replace(" ", ""));

var distance = long.Parse(input[1].Split(":")[1].Replace(" ", ""));
var wins = 0;

for (var milli = 0; milli < time - 1; milli++)
{
    var currdistance = milli * (time - milli);

    if (currdistance > distance)
    {
        wins++;
    }
}


Console.WriteLine(wins);