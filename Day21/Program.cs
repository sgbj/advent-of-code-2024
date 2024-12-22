using P = (int X, int Y);

var codes = await File.ReadAllLinesAsync("input.txt");

string[] numericKeypad = ["789", "456", "123", " 0A"];
string[] directionalKeypad = [" ^A", "<v>"];

var cache = new Dictionary<(char, char, int), long>();

Console.WriteLine(GetTotalComplexity(2));
Console.WriteLine(GetTotalComplexity(25));

long GetTotalComplexity(int robots) =>
    codes.Sum(code => GetShortestSequenceLength(code, numericKeypad, robots) * int.Parse(code.Replace("A", "")));

long GetShortestSequenceLength(string code, string[] keypad, int robots) =>
    $"A{code}".Zip(code, (key1, key2) => FindShortestSequenceLength(key1, key2, keypad, robots)).Sum();

P GetPosition(char key, string[] keypad) =>
    keypad.SelectMany((line, y) => line.Select((c, x) => (x, y, c)))
        .Where(p => p.c == key).Select(p => (p.x, p.y)).First();

long FindShortestSequenceLength(char key1, char key2, string[] keypad, int robots)
{
    if (cache.TryGetValue((key1, key2, robots), out var shortestLength))
    {
        return shortestLength;
    }

    shortestLength = long.MaxValue;

    var queue = new Queue<(P Position, string Sequence, HashSet<P> Visited)>();

    var start = GetPosition(key1, keypad);
    var end = GetPosition(key2, keypad);

    queue.Enqueue((start, "", [start]));

    while (queue.TryDequeue(out var current))
    {
        if (current.Position == end)
        {
            var length = robots == 0
                ? $"{current.Sequence}A".Length
                : GetShortestSequenceLength($"{current.Sequence}A", directionalKeypad, robots - 1);

            shortestLength = Math.Min(shortestLength, length);

            continue;
        }

        var directions = new[] { (-1, 0, '<'), (1, 0, '>'), (0, -1, '^'), (0, 1, 'v') };

        foreach (var (dx, dy, c) in directions)
        {
            P next = (current.Position.X + dx, current.Position.Y + dy);

            if (next.Y >= 0 && next.Y < keypad.Length && next.X >= 0 && next.X < keypad[next.Y].Length &&
                keypad[next.Y][next.X] != ' ' && !current.Visited.Contains(next))
            {
                queue.Enqueue((next, current.Sequence + c, [.. current.Visited, next]));
            }
        }
    }

    cache[(key1, key2, robots)] = shortestLength;

    return shortestLength;
}