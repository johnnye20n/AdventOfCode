var input = File.ReadAllLines(@"C:\Code\advent\2023\AdventOfCode\day5b\input.txt");

var seedToSoilMaps = new SeedMap("seed-to-soil");
var soilToFertilizerMaps = new SeedMap("soil-to-fertilize");
var fertilizerToWaterMaps = new SeedMap("fertilize-to-water");
var waterToLightMaps = new SeedMap("water-to-light");
var lightToTemperatureMaps = new SeedMap("light-to-temp");
var temperatureToHumidityMaps = new SeedMap("temp-to-hum");
var humidityToLocationMaps = new SeedMap("hum-to-loc");

SeedCache.KnownEnds.Add("seed-to-soil", []);
SeedCache.KnownEnds.Add("soil-to-fertilize", []);
SeedCache.KnownEnds.Add("fertilize-to-water", []);
SeedCache.KnownEnds.Add("water-to-light", []);
SeedCache.KnownEnds.Add("light-to-temp", []);
SeedCache.KnownEnds.Add("temp-to-hum", []);

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

var seeds = input[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

var lowest = (long)-1;

for (var seed = 0; seed < seeds.Count; seed += 2)
{
    //79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93
    var startrange = seeds[seed];
    var endrange = startrange + seeds[seed + 1];
    for (var i = startrange; i < endrange; i++)
    {
        var res = seedToSoilMaps.Find(seed, i);
        if (res < 0)
        {
            continue;
        }
        lowest = lowest > -1 ? Math.Min(res, lowest) : res;
        Console.WriteLine($"{i} / {endrange}");
    }
    Console.WriteLine($"seed {seed}/{seeds.Count} completed");
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

    public long Find(int seed, long val)
    {        
        var item = Items.FirstOrDefault(x => val >= x.SourceRangeStart && val < x.SourceRangeStart + x.RangeLength);
        if (item == null)
        {
            //Console.WriteLine($"{Name} => next {val}");
            var nullres = IsFinal ? val : Next!.Find(seed, val);
            return nullres;
        }

        //Console.WriteLine($"{Name} => next {item.DestRangeStart + (val - item.SourceRangeStart)}");
        var res = IsFinal ? item.DestRangeStart + (val - item.SourceRangeStart) : Next!.Find(seed, item.DestRangeStart + (val - item.SourceRangeStart));
        // if (item.IsChecked.ContainsKey(seed))
        // {
        //     return item.IsChecked[seed];
        // }
        // item.IsChecked.Add(seed, res);
        return res;
    }

}

static class SeedCache
{
    public static Dictionary<string, Dictionary<long, long>> KnownEnds { get; set; } = [];
}
class SeedMapItem
{
    public long DestRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }
    public Dictionary<int, long> IsChecked { get; set; } = [];
    public long SourceRangeEnd => SourceRangeStart + RangeLength;
    public bool IsInRange(long val)
    {
        return val >= SourceRangeStart && val < SourceRangeEnd;
    }
}