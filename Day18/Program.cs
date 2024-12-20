using P = (int X, int Y);

var input = (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.Split(',').Select(int.Parse).ToArray())
    .Select(values => (values[0], values[1]))
    .ToArray();

const int size = 70, take = 1024;

P start = (0, 0), end = (size, size);

Console.WriteLine(Simulate([..input.Take(take)]));

for (var i = take; i < input.Length; i++)
{
    if (Simulate([..input.Take(i)]) < 0)
    {
        Console.WriteLine(input[i - 1]);
        break;
    }
}

int Simulate(HashSet<P> corrupted)
{
    var queue = new PriorityQueue<P, int>();
    var steps = new Dictionary<P, int>();

    queue.Enqueue(start, 0);
    steps[start] = 0;

    while (queue.TryDequeue(out var current, out var step))
    {
        if (current == end)
        {
            break;
        }

        P[] directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];

        foreach (var (dx, dy) in directions)
        {
            P nextPosition = (current.X + dx, current.Y + dy);
            var nextStep = step + 1;

            if (nextPosition is { X: >= 0 and <= size, Y: >= 0 and <= size } && !corrupted.Contains(nextPosition) &&
                (!steps.TryGetValue(nextPosition, out var minStep) || nextStep < minStep))
            {
                queue.Enqueue(nextPosition, nextStep);
                steps[nextPosition] = nextStep;
            }
        }
    }

    return steps.GetValueOrDefault(end, -1);
}