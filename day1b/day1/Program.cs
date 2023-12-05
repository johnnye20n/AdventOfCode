var lines = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day1\input1.txt");
var list = new List<string>();
foreach (var line in lines)
{
    var lineChars = "";
    for (var i = 0; i < line.Length; i++)
    {
        if (int.TryParse(line[i].ToString(), out var linechar))
        {
            lineChars += linechar.ToString();
            break;
        }        
    }
    for (var i = line.Length -1; i >= 0; i--)
    {
        if (int.TryParse(line[i].ToString(), out var linechar))
        {
            lineChars += linechar.ToString();
            break;
        }        
    }
    list.Add(lineChars);
}


var res = list.Sum(int.Parse);
Console.WriteLine(res);