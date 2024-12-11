var input = (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.Select(c => c - '0').ToArray())
    .ToArray();

var totalScore = 0;
var totalRating = 0;

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        totalScore += Measure(x, y, 0, []);
        totalRating += Measure(x, y, 0);
    }
}

Console.WriteLine(totalScore);
Console.WriteLine(totalRating);

int Measure(int x, int y, int height, HashSet<(int, int)>? visited = null)
{
    if (y < 0 || y >= input.Length || x < 0 || x >= input[y].Length || input[y][x] != height ||
        visited?.Add((x, y)) == false)
    {
        return 0;
    }

    if (height == 9)
    {
        return 1;
    }

    return Measure(x + 1, y, height + 1, visited) + Measure(x - 1, y, height + 1, visited) +
           Measure(x, y + 1, height + 1, visited) + Measure(x, y - 1, height + 1, visited);
}