var input = await File.ReadAllLinesAsync("input.txt");
var maze = input.SelectMany((line, y) => line.Select((c, x) => (x, y, c))).ToList();
var start = maze.First(item => item.c == 'S');
var end = maze.First(item => item.c == 'E');

var queue = new PriorityQueue<State, int>();
var scores = new Dictionary<(int X, int Y, int Direction), int>();
var bestScore = int.MaxValue;
var bestSpots = new HashSet<(int X, int Y)>();

queue.Enqueue(new(start.x, start.y, 0, 0, []), 0);
scores[(start.x, start.y, 0)] = 0;

while (queue.TryDequeue(out var current, out var score))
{
    HashSet<(int X, int Y)> path = [..current.Path, (current.X, current.Y)];

    if (current.X == end.x && current.Y == end.y && score <= bestScore)
    {
        bestScore = score;
        foreach (var p in path)
        {
            bestSpots.Add(p);
        }

        continue;
    }

    var (dx, dy) = current.Direction switch
    {
        0 => (1, 0),
        1 => (0, 1),
        2 => (-1, 0),
        3 => (0, -1),
        _ => throw new InvalidOperationException()
    };

    State[] nextStates =
    [
        new(current.X + dx, current.Y + dy, current.Direction, score + 1, path),
        new(current.X, current.Y, (current.Direction + 1) % 4, score + 1_000, path),
        new(current.X, current.Y, (current.Direction + 3) % 4, score + 1_000, path)
    ];

    foreach (var next in nextStates)
    {
        if (next.Y >= 0 && next.Y < input.Length && next.X >= 0 && next.X < input[next.Y].Length &&
            input[next.Y][next.X] != '#' &&
            (!scores.TryGetValue((next.X, next.Y, next.Direction), out var minScore) || next.Score <= minScore))
        {
            queue.Enqueue(next, next.Score);
            scores[(next.X, next.Y, next.Direction)] = next.Score;
        }
    }
}

Console.WriteLine(bestScore);
Console.WriteLine(bestSpots.Count);

file record struct State(int X, int Y, int Direction, int Score, HashSet<(int X, int Y)> Path);