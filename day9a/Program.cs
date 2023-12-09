var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day9a\input.txt");



var sum = 0;
foreach (var line in input)
{
    
    var found = false;
    var sublines = new List<List<int>>();
    var current = line.Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
    sublines.Add(current);
    do
    {
        var subline = new List<int>();
        for (var i = 0; i < current.Count - 1; i++)
        {
            subline.Add(current[i + 1] - current[i]);
        }
        sublines.Add(subline);
        if (subline.All(x => x == 0))
        {
            found = true;
        }
        current = subline;
    } while (found == false);

    for (var i = sublines.Count - 1; i > 0; i--)
    {
        var toadd = sublines[i - 1].Last() + sublines[i].Last();
        sublines[i - 1].Add(toadd);
    }

    foreach (var subline in sublines)
    {
        Console.WriteLine(string.Join(", ", subline));
    }

    sum += sublines.First().Last();
}

Console.WriteLine(sum);