var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day5a\input.txt");

var seedToSoilMaps = new SeedMap("seed-to-soil");
var soilToFertilizerMaps = new SeedMap("soil-to-fertilize");
var fertilizerToWaterMaps = new SeedMap("fertilize-to-water");
var waterToLightMaps = new SeedMap("water-to-light");
var lightToTemperatureMaps = new SeedMap("light-to-temp");
var temperatureToHumidityMaps = new SeedMap("temp-to-hum");
var humidityToLocationMaps = new SeedMap("hum-to-loc");

seedToSoilMaps.Next = soilToFertilizerMaps;
soilToFertilizerMaps.Next = fertilizerToWaterMaps;
fertilizerToWaterMaps.Next = waterToLightMaps;
waterToLightMaps.Next = lightToTemperatureMaps;
lightToTemperatureMaps.Next = temperatureToHumidityMaps;
temperatureToHumidityMaps.Next = humidityToLocationMaps;

var currentMap = "";

for (var i = 1; i < input.Length - 1; i++)
{
    if (string.IsNullOrWhiteSpace(input[i]))
    {
        continue;
    }

    if (input[i].Contains("map"))
    {
        currentMap = input[i].Trim();
        continue;
    }

    var split = input[i].Split(" ").Select(long.Parse).ToList();
    var seedMap = new SeedMapItem
    {
        DestRangeStart = split[0],
        SourceRangeStart = split[1],
        RangeLength = split[2]
    };

    switch (currentMap)
    {
        case "seed-to-soil map:":
            {
                seedToSoilMaps.Add(seedMap);
                break;
            }
        case "soil-to-fertilizer map:":
            {
                soilToFertilizerMaps.Add(seedMap);
                break;
            }
        case "fertilizer-to-water map:":
            {
                fertilizerToWaterMaps.Add(seedMap);
                break;
            }
        case "water-to-light map:":
            {
                waterToLightMaps.Add(seedMap);
                break;
            }
        case "light-to-temperature map:":
            {
                lightToTemperatureMaps.Add(seedMap);
                break;
            }
        case "temperature-to-humidity map:":
            {
                temperatureToHumidityMaps.Add(seedMap);
                break;
            }
        case "humidity-to-location map:":
            {
                humidityToLocationMaps.Add(seedMap);
                break;
            }
        default: throw new Exception();
    }
}

var seeds = input[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);

var lowest = (long)-1;
foreach (var seed in seeds)
{
    var res = seedToSoilMaps.Find(seed);
    lowest = lowest > -1 ? Math.Min(res, lowest) : res;    
}

Console.WriteLine(lowest);

class SeedMap
{
    public string Name { get; }
    public SeedMap(string name)
    {
        Name = name;
    }
    public List<SeedMapItem> Items { get; set; } = [];

    public void Add(SeedMapItem item)
    {
        Items.Add(item);
    }

    public SeedMap? Next { get; set; }
    public bool IsFinal => Next == null;

    public long Find(long val)
    {
        var item = Items.FirstOrDefault(x => val >= x.SourceRangeStart && val < x.SourceRangeStart + x.RangeLength);
        if (item == null)
        {
            Console.WriteLine($"{Name} => next {val}");
            return IsFinal ? val : Next!.Find(val);
        }
        Console.WriteLine($"{Name} => next {item.DestRangeStart + (val - item.SourceRangeStart)}");
        return IsFinal ? item.DestRangeStart + (val - item.SourceRangeStart) : Next!.Find(item.DestRangeStart + (val - item.SourceRangeStart));
    }
}
class SeedMapItem
{
    public long DestRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }
}