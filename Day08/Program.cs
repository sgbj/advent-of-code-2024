var input = await File.ReadAllLinesAsync("input.txt");
var antennas = input.SelectMany((line, y) => line.Select((c, x) => (x, y, c)))
    .Where(a => a.c != '.')
    .GroupBy(a => a.c)
    .ToList();

var antinodes1 = new HashSet<(int X, int Y)>();
var antinodes2 = new HashSet<(int X, int Y)>();

foreach (var frequency in antennas)
{
    foreach (var a1 in frequency)
    {
        foreach (var a2 in frequency)
        {
            if (a1 == a2)
            {
                continue;
            }

            if (InBounds(a1.x + (a2.x - a1.x) * 2, a1.y + (a2.y - a1.y) * 2))
            {
                antinodes1.Add((a1.x + (a2.x - a1.x) * 2, a1.y + (a2.y - a1.y) * 2));
            }

            var (dx, dy) = Normalize(a2.x - a1.x, a2.y - a1.y);

            for (var step = 0; InBounds(a1.x + dx * step, a1.y + dy * step); step--)
            {
                antinodes2.Add((a1.x + dx * step, a1.y + dy * step));
            }

            for (var step = 0; InBounds(a1.x + dx * step, a1.y + dy * step); step++)
            {
                antinodes2.Add((a1.x + dx * step, a1.y + dy * step));
            }
        }
    }
}

Console.WriteLine(antinodes1.Count);
Console.WriteLine(antinodes2.Count);

bool InBounds(int x, int y) => y >= 0 && y < input.Length && x >= 0 && x < input[y].Length;

(int X, int Y) Normalize(int x, int y)
{
    var gcd = Gcd(Math.Abs(x), Math.Abs(y));
    return (x / gcd, y / gcd);
    int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);
}