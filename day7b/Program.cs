var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day7b\input.txt");
var draws = new List<Draw>();

var i = 0;
foreach (var line in input)
{
    var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    var draw = new Draw(i, split[0], int.Parse(split[1]));
    draws.Add(draw);
    i++;
}

var groupedDraws = draws.GroupBy(x => (int)x.Hand).Select(x => x).ToList();

foreach (var group in groupedDraws)
{
    foreach (var item in group)
    {
        var others = group.Where(x => x.Id != item.Id).ToList();
        item.Rank = others.Count(item.IsWinner) + 1;
    }
}

var currentSubRank = 0;
var winnings = 0;
foreach (var group in groupedDraws.OrderBy(x => x.Key))
{
    foreach (var item in group)
    {
        winnings += item.Winnings(currentSubRank);
    }
    currentSubRank += group.Max(x => x.Rank);
}

Console.WriteLine(winnings);


class Draw
{
    public int Id { get; }
    public int Rank { get; set; }

    public int Winnings(int subrank) => (subrank + Rank) * Bid;
    public Draw(int id, string cards, int bid)
    {
        Id = id;
        Cards = cards;
        Bid = bid;
        var cardArray = Cards.ToCharArray().Where(x => x != 'J').ToArray();

        if (IsXOfKind(cardArray, 5))
        {
            Hand = Hand.FiveOfAKind;
            return;
        }
        else if (IsXOfKind(cardArray, 4))
        {
            Hand = (Hand)((int)Hand.FourOfAKind + JokerCount);
        }
        else if (IsXOfKind(cardArray, 3) && IsXOfKind(cardArray, 2))
        {
            Hand = Hand.FullHouse;
        }
        else if (IsXOfKind(cardArray, 3))
        {
            Hand = (Hand)((int)Hand.ThreeOfAKind + (JokerCount == 1 ? 2 : JokerCount == 2 ? 3 : 0));
        }
        else if (cardArray.GroupBy(x => x).Select(x => x).Where(y => y.Count() == 2).Count() == 2)
        {
            Hand = (Hand)((int)Hand.TwoPair + (JokerCount == 1 ? 2 : JokerCount));
        }
        else if (IsXOfKind(cardArray, 2))
        {
            Hand = (Hand)((int)Hand.OnePair + (JokerCount == 1 ? 2 : JokerCount == 2 ? 4 : JokerCount == 3 ? 5 : 0));
        }
        else
        { 
            Hand = JokerCount == 1 ? Hand.OnePair : JokerCount == 2 ? Hand.ThreeOfAKind : JokerCount == 3 ? Hand.FourOfAKind : JokerCount == 4 ? Hand.FiveOfAKind : JokerCount == 5 ? Hand.FiveOfAKind : Hand.HighCard;
        }

        if (Cards.Contains('J'))
        {
            File.AppendAllLines(@"test.txt", new[] { cards + " " + Hand });

        }
    }

    public string Cards { get; }
    public int Bid { get; }
    public Hand Hand { get; set; }
    static bool IsXOfKind(char[] cardArray, int num)
    {
        return cardArray.GroupBy(x => x).Select(x => x).Any(y => y.Count() == num);
    }

    public int JokerCount => Cards.ToCharArray().Count(x => x == 'J');

    public bool IsWinner(Draw draw)
    {
        if ((int)Hand > (int)draw.Hand)
        {
            return true;
        }
        if ((int)Hand < (int)draw.Hand)
        {
            return false;
        }

        for (var i = 0; i < Cards.Length; i++)
        {
            if (CardValueMap[Cards[i]] > CardValueMap[draw.Cards[i]])
            {
                return true;
            }

            if (CardValueMap[Cards[i]] < CardValueMap[draw.Cards[i]])
            {
                return false;
            }
        }
        throw new Exception("fel");
    }

    private Dictionary<char, int> CardValueMap { get; set; } = new Dictionary<char, int> {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
        { '1', 1 },
        { 'J', 0}
    };
}


enum Hand
{
    FiveOfAKind = 10,
    FourOfAKind = 9,
    FullHouse = 8,
    ThreeOfAKind = 7,
    TwoPair = 6,
    OnePair = 5,
    HighCard = 4
}

