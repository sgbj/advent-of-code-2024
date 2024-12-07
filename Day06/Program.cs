var input = await File.ReadAllLinesAsync("input.txt");
var (sx, sy, _) = input.SelectMany((line, y) => line.Select((c, x) => (x, y, c))).First(p => p.c == '^');

var positions = Predict().Positions;

Console.WriteLine(positions);

var loops = Enumerable.Range(0, input.Length)
    .Sum(y => Enumerable.Range(0, input[y].Length).Count(x => (x, y) != (sx, sy) && Predict((x, y)).IsLoop));

Console.WriteLine(loops);

(int Positions, bool IsLoop) Predict((int X, int Y)? block = null)
{
    var (x, y, dir) = (sx, sy, 0);
    var visited = new HashSet<(int x, int y, int dir)>();

    while (visited.Add((x, y, dir)))
    {
        var (dx, dy) = dir switch
        {
            0 => (0, -1),
            1 => (1, 0),
            2 => (0, 1),
            3 => (-1, 0),
            _ => throw new InvalidOperationException()
        };

        if (y + dy < 0 || y + dy >= input.Length || x + dx < 0 || x + dx >= input[y + dy].Length)
        {
            return (visited.Select(v => (v.x, v.y)).Distinct().Count(), false);
        }

        if (input[y + dy][x + dx] == '#' || (x + dx, y + dy) == block)
        {
            dir = (dir + 1) % 4;
        }
        else
        {
            x += dx;
            y += dy;
        }
    }

    return (0, true);
}