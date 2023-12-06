internal class Program
{
    private static int total = 0;
    private static List<string> output = new();
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day4b\input2.txt");
        total = 0;//input.Length;
        //Card   1:  8 86 59 90 68 52 55 24 37 69 | 10 55  8 86  6 62 69 68 59 37 91 90 24 22 78 61 58 89 52 96 95 94 13 36 81
        
        CheckCards(input, 0, input.Length - 1);
        output = output.OrderBy(x => x).ToList();
        foreach(var o in output) {
            Console.WriteLine(o);
        }
        Console.WriteLine(total);
    }
    private static void CheckCards(string[] input, int startCheck, int endCheck, int level = 0)
    {
        for (var i = startCheck; i < endCheck; i++)
        {            
            if (i > input.Length - 1)
            {
                break;
            }
            Console.WriteLine($"line {i+1} checked");
            var split = input[i].Split(":");
            var winnersplit = split[1].Split("|");

            var winners = winnersplit[0].Chunk(3).Where(x => x.Count() == 3).Select(x => int.Parse(string.Join("", x).Trim())).ToList();
            var numbers = winnersplit[1].Chunk(3).Select(x => int.Parse(string.Join("", x).Trim())).ToList();

            var correct = numbers.Where(x => winners.Contains(x)).Count();
            total += Math.Max(correct, 1);
            output.Add($"level {level} card {i+1} winners {i+2}-{i + 1 + correct}");
            //line 1 3 correct => check 2,3,4
            if (correct > 0)
            {
                
                CheckCards(input, i + 1, i + correct, level+1);
            }
        }
    }

}