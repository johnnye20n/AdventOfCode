internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day3b\input.txt");

        var gears = new List<string>();
        var lineLength = 0;

        for (var line = 0; line < input.Length; line++)
        {
            lineLength = input[line].Length;
            for (var i = 0; i < input[line].Length; i++)
            {
                if (input[line][i] == '*')
                {
                    gears.Add($"{line}:{i}");
                }
            }
        }

        //Console.WriteLine(string.Join(", ", gears));

        var sum = 0;

        foreach (var gear in gears)
        {
            var splitted = gear.Split(":");
            var line = int.Parse(splitted[0]);
            var col = int.Parse(splitted[1]);

            var colCheckLeft = col > 0 ? col - 1 : col;
            var colCheckRight = col + 1 < lineLength ? col + 1 : lineLength;

            var foundNumbers = new List<int>();

            //check upper
            if (line > 0)
            {
                var upper = CheckLine(input, line - 1, colCheckLeft, colCheckRight);
                if (upper.Any())
                {
                    foundNumbers.AddRange(upper);
                }
            }

            //check under
            if (line < input.Length)
            {
                var upper = CheckLine(input, line + 1, colCheckLeft, colCheckRight);
                if (upper.Any())
                {
                    foundNumbers.AddRange(upper);
                }
            }

            //check current
            var current = CheckLine(input, line, colCheckLeft, colCheckRight);
            if (current.Any())
            {
                foundNumbers.AddRange(current);
            }

            if (foundNumbers.Count > 1)
            {
                sum += foundNumbers[0] * foundNumbers[1];
            }
        }

        Console.WriteLine(sum);

        static List<int> CheckLine(string[] input, int line, int colCheckLeft, int colCheckRight)
        {
            var foundNumbers = new List<int>();

            var lineCheck = input[line][colCheckLeft..(colCheckRight + 1)];

            if (lineCheck.All(char.IsDigit))
            {
                foundNumbers.Add(int.Parse(lineCheck));
            }
            else if (lineCheck.Any(char.IsDigit) && !char.IsDigit(lineCheck.Last()) && !char.IsDigit(lineCheck.First()))
            {
                foundNumbers.Add(int.Parse(lineCheck[1].ToString()));
            }
            else if (lineCheck.Any(char.IsDigit) && !char.IsDigit(lineCheck.Last()))
            {
                var left = CheckLeft(input, line, colCheckLeft, string.Join("", lineCheck.ToCharArray().Where(char.IsDigit)));
                foundNumbers.Add(left);
            }
            else if (lineCheck.Any(char.IsDigit) && !char.IsDigit(lineCheck.First()))
            {
                var right = CheckRight(input, line, colCheckRight, string.Join("", lineCheck.ToCharArray().Where(char.IsDigit)));
                foundNumbers.Add(right);
            }
            else if (lineCheck.Any(char.IsDigit))
            {
                var first = lineCheck.First();
                var left = CheckLeft(input, line, colCheckLeft, first.ToString());
                foundNumbers.Add(left);
                var second = lineCheck.Last();
                var right = CheckRight(input, line, colCheckRight, second.ToString());
                foundNumbers.Add(right);
            }

            return foundNumbers;
        }

        static int CheckLeft(string[] input, int line, int colCheckLeft, string stringToCheck)
        {
            var res = stringToCheck;
            var nextColCheck = colCheckLeft - 1;
            do
            {
                var next = input[line][nextColCheck];
                if (!Char.IsDigit(next))
                {
                    break;
                }
                res = next + res;
                nextColCheck--;
            } while (nextColCheck >= 0);

            return int.Parse(res);
        }

        static int CheckRight(string[] input, int line, int colCheckLeft, string stringToCheck)
        {
            var res = stringToCheck;
            var nextColCheck = colCheckLeft + 1;
            do
            {
                var next = input[line][nextColCheck];
                if (!Char.IsDigit(next))
                {
                    break;
                }
                res += next;
                nextColCheck++;
            } while (nextColCheck <= input[line].Length - 1);

            return int.Parse(res);
        }
    }
}