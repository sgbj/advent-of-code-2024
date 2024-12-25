using System.Text.RegularExpressions;

var input = Regex.Split(await File.ReadAllTextAsync("input.txt"), "\r?\n\r?\n");
var locks = input.Where(s => s.StartsWith("#####")).Select(s => Regex.Split(s, "\r?\n")).ToArray();
var keys = input.Where(s => s.EndsWith("#####")).Select(s => Regex.Split(s, "\r?\n")).ToArray();

Console.WriteLine(locks.Sum(@lock => keys.Count(key => IsFit(@lock, key))));

bool IsFit(string[] @lock, string[] key)
{
    var pinHeights = GetHeights(@lock, true).ToArray();
    var keyHeights = GetHeights(key, false).ToArray();

    return !pinHeights.Where((pin, i) => @lock.Length - 1 - pin <= keyHeights[i]).Any();
}

IEnumerable<int> GetHeights(string[] spec, bool isLock)
{
    for (var x = 0; x < spec[0].Length; x++)
    {
        var y = 0;

        while (y < spec.Length && spec[isLock ? y : spec.Length - 1 - y][x] == '#')
        {
            y++;
        }

        yield return y - 1;
    }
}