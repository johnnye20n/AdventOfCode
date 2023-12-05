var lines = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day1b\input1.txt");
var list = new List<string>();

foreach (var line in lines)
{
    var currentLine = line;
    currentLine = currentLine.Replace("one", "one1one");
    currentLine = currentLine.Replace("two", "two2two");
    currentLine = currentLine.Replace("three", "three3three");
    currentLine = currentLine.Replace("four", "four4four");
    currentLine = currentLine.Replace("five", "five5five");
    currentLine = currentLine.Replace("six", "six6six");
    currentLine = currentLine.Replace("seven", "seven7seven");
    currentLine = currentLine.Replace("eight", "eight8eight");
    currentLine = currentLine.Replace("nine", "nine9nine");
    
    var lineChars = "";

    for (var i = 0; i < currentLine.Length; i++)
    {
        if (int.TryParse(currentLine[i].ToString(), out var linechar))
        {
            lineChars += linechar.ToString();
            break;
        }
    }

    for (var i = currentLine.Length - 1; i >= 0; i--)
    {
        if (int.TryParse(currentLine[i].ToString(), out var linechar))
        {
            lineChars += linechar.ToString();
            break;
        }
    }
    list.Add(lineChars);
}

var res = list.Sum(int.Parse);
Console.WriteLine(res);