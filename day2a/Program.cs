var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day2a\input.txt");


var sum = 0;

var maxred = 12;
var maxgreen = 13;
var maxblue = 14;

//Game 1: 2 red, 2 green; 1 red, 1 green, 2 blue; 3 blue, 3 red, 3 green; 1 blue, 3 green, 7 red; 5 red, 3 green, 1 blue

foreach (var line in input)
{
    var split = line.Split(":");
    var id = int.Parse(split[0].Split(" ")[1]);

    var cubes = split[1].Split(";");
    var shouldbreak = false;
    foreach (var cube in cubes)
    {
        var cubesplit = cube.Split(",");
        var cubegreen = cubesplit.Where(x => x.Contains("green")).Select(x => x.Trim()).Sum(x => int.Parse(x.Split(" ")[0]));
        var cubered = cubesplit.Where(x => x.Contains("red")).Select(x => x.Trim()).Sum(x => int.Parse(x.Split(" ")[0]));
        var cubeblue = cubesplit.Where(x => x.Contains("blue")).Select(x => x.Trim()).Sum(x => int.Parse(x.Split(" ")[0]));
        if (cubegreen > maxgreen || cubeblue > maxblue || cubered > maxred)
        {
            shouldbreak = true;
            break;
        }
    }
    if (shouldbreak)
    {
        continue;
    }
    sum += id;
}

Console.WriteLine(sum);
