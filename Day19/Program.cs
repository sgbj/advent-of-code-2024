using System.Text.RegularExpressions;

var input = Regex.Split(await File.ReadAllTextAsync("input.txt"), "\r?\n\r?\n");
var patterns = input[0].Split(", ");
var designs = Regex.Split(input[1], "\r?\n");

Console.WriteLine(designs.Count(design => CountPossibleArrangements(design, "", []) > 0));
Console.WriteLine(designs.Sum(design => CountPossibleArrangements(design, "", [])));

long CountPossibleArrangements(string design, string current, Dictionary<string, long> cache)
{
    if (cache.TryGetValue(current, out var count))
    {
        return count;
    }

    if (current.Length >= design.Length || !design.StartsWith(current))
    {
        cache[current] = current == design ? 1 : 0;
    }
    else
    {
        cache[current] = patterns.Sum(pattern => CountPossibleArrangements(design, current + pattern, cache));
    }

    return cache[current];
}