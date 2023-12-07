
internal class Program
{
    private static Dictionary<int, List<int>> RowWinners = [];
    private static Dictionary<int, int> RowWinnersSum = [];
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day4b\input.txt");
        //Card   1:  8 86 59 90 68 52 55 24 37 69 | 10 55  8 86  6 62 69 68 59 37 91 90 24 22 78 61 58 89 52 96 95 94 13 36 81

        CheckCards(input, 0, input.Length - 1);

        foreach (var row in RowWinners.OrderByDescending(x => x.Key).ToList())
        {
            var currentRowSum = row.Value.Count;

            foreach (var winner in row.Value)
            {
                if (RowWinnersSum.ContainsKey(winner))
                {
                    currentRowSum += RowWinnersSum[winner];
                }
            }
            RowWinnersSum.Add(row.Key, currentRowSum);
        }
        Console.WriteLine(RowWinnersSum.Sum(x => x.Value) + input.Length);
    }
    private static void CheckCards(string[] input, int startCheck, int endCheck)
    {
        for (var i = startCheck; i < endCheck; i++)
        {
            if (!RowWinners.ContainsKey(i))
            {
                RowWinners.Add(i, []);
            }

            var split = input[i].Split(":");
            var winnersplit = split[1].Split("|");

            var winners = winnersplit[0].Chunk(3).Where(x => x.Count() == 3).Select(x => int.Parse(string.Join("", x).Trim())).ToList();
            var numbers = winnersplit[1].Chunk(3).Select(x => int.Parse(string.Join("", x).Trim())).ToList();

            var correct = numbers.Where(x => winners.Contains(x)).Count();
            Console.WriteLine($"row {i} wins {correct} copies");

            for (var j = 0; j < correct; j++)
            {
                RowWinners[i].Add(j + i + 1);
                Console.WriteLine($"adding {j + i + 1} to dictionary");
            }
        }
    }
}
