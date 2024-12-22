using P = (int X, int Y);

var input = await File.ReadAllLinesAsync("input.txt");
var map = input.SelectMany((line, y) => line.Select((c, x) => (x, y, c)))
    .ToDictionary(p => (p.x, p.y), p => p.c);
var start = map.First(e => e.Value == 'S').Key;
var end = map.First(e => e.Value == 'E').Key;

var normal = Simulate([]);

Console.WriteLine(normal);

Console.WriteLine(Enumerable.Range(0, input.Length)
    .SelectMany(y => Enumerable.Range(0, input[y].Length).Select(x => new P[] { (x, y) }))
    .Count(cheats => normal - Simulate(cheats) >= 100));

int Simulate(P[] cheats)
{
    var queue = new PriorityQueue<P, int>();
    var times = new Dictionary<P, int>();

    queue.Enqueue(start, 0);
    times[start] = 0;

    while (queue.TryDequeue(out var current, out var time))
    {
        if (current == end)
        {
            break;
        }

        var directions = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach (var (dx, dy) in directions)
        {
            var nextPosition = (current.X + dx, current.Y + dy);
            var nextTime = time + 1;

            if (map.TryGetValue(nextPosition, out var c) &&
                (c != '#' || cheats.Contains(nextPosition)) &&
                (!times.TryGetValue(nextPosition, out var minTime) || nextTime < minTime))
            {
                queue.Enqueue(nextPosition, nextTime);
                times[nextPosition] = nextTime;
            }
        }
    }

    return times[end];
}