var stones = (await File.ReadAllTextAsync("input.txt")).Split(' ').Select(long.Parse).ToArray();
var cache = new Dictionary<string, long>();

Console.WriteLine(stones.Sum(stone => Count(stone, 25, cache)));
Console.WriteLine(stones.Sum(stone => Count(stone, 75, cache)));

static long Count(long stone, int blinks, Dictionary<string, long> cache)
{
    if (blinks == 0)
    {
        return 1;
    }

    if (cache.TryGetValue($"{stone}:{blinks}", out var count))
    {
        return count;
    }

    count = stone switch
    {
        0 => Count(1, blinks - 1, cache),
        _ when $"{stone}" is var s && s.Length % 2 == 0 =>
            Count(long.Parse(s[..(s.Length / 2)]), blinks - 1, cache) +
            Count(long.Parse(s[(s.Length / 2)..]), blinks - 1, cache),
        _ => Count(stone * 2024, blinks - 1, cache)
    };

    cache[$"{stone}:{blinks}"] = count;

    return count;
}