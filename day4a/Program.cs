var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day4a\input.txt");
//Card   1:  8 86 59 90 68 52 55 24 37 69 | 10 55  8 86  6 62 69 68 59 37 91 90 24 22 78 61 58 89 52 96 95 94 13 36 81

var sum = 0;
foreach (var line in input)
{
    var split = line.Split(":");
    var winnersplit = split[1].Split("|");
    var aa = winnersplit[0].Chunk(3).ToList();
    var winners = winnersplit[0].Chunk(3).Where(x => x.Count() == 3).Select(x => int.Parse(string.Join("", x).Trim())).ToList();
    var numbers = winnersplit[1].Chunk(3).Select(x => int.Parse(string.Join("", x).Trim())).ToList();
    
    var correct = numbers.Where(x => winners.Contains(x)).Count();

    //correct = 3 => Math.Pow(2, 2) => 4

    var res = correct > 0 ? Math.Pow(2, correct - 1) : 0;
    sum += (int)res;
}

Console.WriteLine(sum);