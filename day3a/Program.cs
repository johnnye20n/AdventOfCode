var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day3a\input.txt");

var sum = 0;


var abcdefg = "abcdefg";
var r = abcdefg[1..4];

for (var line = 0; line < input.Length; line++)
{
    var numbersinline = new List<int>();
    var number = "";
    var numberstartpos = -1;
    var numberendpos = -1;
    for (var i = 0; i < input[line].Length; i++)
    {
        var c = input[line][i];
        if (Char.IsDigit(c))
        {
            number += c;
            if (numberstartpos == -1)
            {
                numberstartpos = i;
            }
        }
        else if (!string.IsNullOrWhiteSpace(number))
        {
            numberendpos = i - 1;

            //check left
            if (numberstartpos > 0)
            {
                var leftchar = input[line][numberstartpos - 1];
                if (leftchar != '.')
                {
                    Console.WriteLine($"line {line} number {number} matched in left {leftchar}");
                    sum += int.Parse(number);
                    number = "";
                    numberstartpos = -1;
                    continue;
                }
            }

            //check right
            if (numberendpos + 1 < input[line].Length)
            {
                var rightchar = input[line][numberendpos + 1];
                if (rightchar != '.')
                {
                    Console.WriteLine($"line {line} number {number} matched in right {rightchar}");
                    sum += int.Parse(number);
                    number = "";
                    numberstartpos = -1;
                    continue;
                }
            }

            //check top
            if (line > 0)
            {
                var leftsiderange = (numberstartpos > 0 ? numberstartpos - 1 : numberstartpos);
                var rightsiderange = numberendpos == input[line].Length - 1 ? numberendpos : numberendpos + 1;
                var range = input[line - 1][leftsiderange..(rightsiderange + 1)];

                if (range.Any(x => x != '.' && !char.IsAsciiDigit(x)))
                {
                    Console.WriteLine($"line {line} number {number} matched in upperrange {range}");
                    sum += int.Parse(number);
                    number = "";
                    numberstartpos = -1;
                    continue;
                }
            }

            //check bottom
            if (line < input.Length - 1)
            {
                var leftsiderange = (numberstartpos > 0 ? numberstartpos - 1 : numberstartpos);
                var rightsiderange = numberendpos == input[line].Length - 1 ? numberendpos : numberendpos + 1;
                var range = input[line + 1][leftsiderange..(rightsiderange + 1)];

                if (range.Any(x => x != '.' && !char.IsAsciiDigit(x)))
                {
                    Console.WriteLine($"line {line} number {number} matched in bottomrange {range}");
                    sum += int.Parse(number);
                    number = "";
                    numberstartpos = -1;
                    continue;
                }
            }
            number = "";
            numberstartpos = -1;

        }

        if (i == input[line].Length - 1)
        {
            if (Char.IsDigit(c))
            {
                numberendpos = i;

                //check left
                if (numberstartpos > 0)
                {
                    var leftchar = input[line][numberstartpos - 1];
                    if (leftchar != '.')
                    {
                        Console.WriteLine($"line {line} number {number} matched in left {leftchar}");
                        sum += int.Parse(number);
                        number = "";
                        numberstartpos = -1;
                        continue;
                    }
                }

                //check right
                if (numberendpos + 1 < input[line].Length)
                {
                    var rightchar = input[line][numberendpos + 1];
                    if (rightchar != '.')
                    {
                        Console.WriteLine($"line {line} number {number} matched in right {rightchar}");
                        sum += int.Parse(number);
                        number = "";
                        numberstartpos = -1;
                        continue;
                    }
                }

                //check top
                if (line > 0)
                {
                    var leftsiderange = (numberstartpos > 0 ? numberstartpos - 1 : numberstartpos);
                    var rightsiderange = numberendpos == input[line].Length - 1 ? numberendpos : numberendpos + 1;
                    var range = input[line - 1][leftsiderange..(rightsiderange + 1)];

                    if (range.Any(x => x != '.' && !char.IsAsciiDigit(x)))
                    {
                        Console.WriteLine($"line {line} number {number} matched in upperrange {range}");
                        sum += int.Parse(number);
                        number = "";
                        numberstartpos = -1;
                        continue;
                    }
                }

                //check bottom
                if (line < input.Length - 1)
                {
                    var leftsiderange = (numberstartpos > 0 ? numberstartpos - 1 : numberstartpos);
                    var rightsiderange = numberendpos == input[line].Length - 1 ? numberendpos : numberendpos + 1;
                    var range = input[line + 1][leftsiderange..(rightsiderange + 1)];

                    if (range.Any(x => x != '.' && !char.IsAsciiDigit(x)))
                    {
                        Console.WriteLine($"line {line} number {number} matched in bottomrange {range}");
                        sum += int.Parse(number);
                        number = "";
                        numberstartpos = -1;
                        continue;
                    }
                }
                number = "";
                numberstartpos = -1;
            }
        }
    }

    Console.WriteLine(string.Join(", ", numbersinline));
}

Console.WriteLine(sum);
