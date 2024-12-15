using System.Text.RegularExpressions;

var machines = Regex.Split(await File.ReadAllTextAsync("input.txt"), "\r?\n\r?\n")
    .Select(s => Regex.Matches(s, @"\d+", RegexOptions.Multiline).Select(match => long.Parse(match.Value)).ToArray())
    .ToArray();

Console.WriteLine(machines.Sum(machine => GetTokens(machine)));
Console.WriteLine(machines.Sum(machine => GetTokens(machine, 10_000_000_000_000)));

static long GetTokens(long[] machine, long error = 0)
{
    // https://en.wikipedia.org/wiki/Cramer%27s_rule
    var (a1, a2, b1, b2, c1, c2) =
        (machine[0], machine[1], machine[2], machine[3], machine[4] + error, machine[5] + error);

    var d = a1 * b2 - b1 * a2;

    if (d == 0)
    {
        return 0;
    }

    var x = (c1 * b2 - b1 * c2) / d;
    var y = (a1 * c2 - c1 * a2) / d;

    return x >= 0 && y >= 0 && a1 * x + b1 * y == c1 && a2 * x + b2 * y == c2 ? 3 * x + y : 0;
}