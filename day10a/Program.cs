using System.Linq.Expressions;

var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day10a\input.txt");

var spointRow = 0;
var spointCol = 0;
var chars = new[] {
    'F',
    '|',
    '-',
    'L',
    'J',
    '7'
};


var charDirectionMap = new Dictionary<char, List<Direction>> {
    { '|', new [] { Direction.Up, Direction.Down}.ToList()},
    { '-', new [] { Direction.Left, Direction.Right}.ToList()},
    { 'L', new [] { Direction.Down, Direction.Left}.ToList()},
    { 'J', new [] { Direction.Down, Direction.Right}.ToList()},
    { '7', new [] { Direction.Up, Direction.Right}.ToList()},
    { 'F', new [] { Direction.Up, Direction.Right}.ToList()},
};

for (var i = 0; i < input.Length; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {
        if (input[i][j] == 'S')
        {
            spointRow = i;
            spointCol = j;
            break;
        }
    }
}

var startfoundcount = 0;

foreach (var startchar in chars)
{
    foreach (var map in charDirectionMap[startchar])
    {
        var directionCount = 0;
        var foundStart = false;
        var currentChar = startchar;

        var start = (spointRow, spointCol, map);
        var foundGround = false;
        Console.WriteLine(currentChar);
        do
        {
            directionCount++;
            var next = GetNext(start, currentChar);
            if (next.Col == spointCol && next.Row == spointRow)
            {
                foundStart = true;
            }
            else if (input[next.Row][next.Col] == '.')
            {
                foundGround = true;
            }
            start = next;
            currentChar = input[next.Row][next.Col];
            Console.WriteLine(currentChar);
        } while (foundStart == false && foundGround == false);

        if (foundGround)
        {
            Console.WriteLine($"{startchar} {map} found ground " + directionCount);
            break;
        }
        else
        {
            Console.WriteLine($"{startchar} {map} found start " + directionCount);
            startfoundcount = directionCount;
            break;
        }
    }
    if(startfoundcount > 0) {
        break;
    }
}


Console.WriteLine($"start found {startfoundcount}");
Console.WriteLine(startfoundcount/2);
static (int Row, int Col, Direction Direction) GetNext((int Row, int Col, Direction Direction) current, char currentChar)
{
    switch (currentChar)
    {
        case '|': return (current.Row + (current.Direction == Direction.Down ? 1 : -1), current.Col, current.Direction);
        case '-': return (current.Row, current.Col + (current.Direction == Direction.Left ? -1 : 1), current.Direction);
        case 'L':
            return current.Direction == Direction.Down
            ? (current.Row, current.Col + 1, Direction.Right)
            : current.Direction == Direction.Left ? (current.Row - 1, current.Col, Direction.Up) : throw new Exception("fel");
        case 'J':
            return current.Direction == Direction.Down
            ? (current.Row, current.Col - 1, Direction.Left)
            : current.Direction == Direction.Right ? (current.Row - 1, current.Col, Direction.Up) : throw new Exception("fel");
        case '7':
            return current.Direction == Direction.Up
            ? (current.Row, current.Col - 1, Direction.Left)
            : current.Direction == Direction.Right ? (current.Row + 1, current.Col, Direction.Down) : throw new Exception("fel");
        case 'F':
            return current.Direction == Direction.Up
            ? (current.Row, current.Col + 1, Direction.Right)
            : current.Direction == Direction.Left ? (current.Row + 1, current.Col, Direction.Down) : throw new Exception("fel");

        case '.': return (-10, -10, Direction.Ground);
    }
    throw new Exception("fel");
}



enum Direction
{
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4,
    Ground = 99
}