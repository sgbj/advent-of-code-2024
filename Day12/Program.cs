using P = (int X, int Y);

var input = await File.ReadAllLinesAsync("input.txt");

var price = 0;
var discountedPrice = 0;

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        price += Count(x, y, input[y][x], [], false);
        discountedPrice += Count(x, y, input[y][x], [], true);
    }
}

Console.WriteLine(price);
Console.WriteLine(discountedPrice);

int Count(int x, int y, char plant, HashSet<P> plots, bool discount)
{
    if (!Check(x, y, plant) || !plots.Add((x, y)))
    {
        return 0;
    }

    P[] directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    int count;

    if (discount)
    {
        P[][] corners = [[(0, -1), (1, 0)], [(1, 0), (0, 1)], [(0, 1), (-1, 0)], [(-1, 0), (0, -1)]];

        count = corners.Count(corner =>
            (!Check(x + corner[0].X, y + corner[0].Y, input[y][x]) &&
             !Check(x + corner[1].X, y + corner[1].Y, input[y][x])) ||
            (Check(x + corner[0].X, y + corner[0].Y, input[y][x]) &&
             Check(x + corner[1].X, y + corner[1].Y, input[y][x]) &&
             !Check(x + corner[0].X + corner[1].X, y + corner[0].Y + corner[1].Y, input[y][x])));
    }
    else
    {
        count = directions.Count(d => !Check(x + d.X, y + d.Y, input[y][x]));
    }

    return count + directions.Sum(d => Count(x + d.X, y + d.Y, plant, plots, discount));
}

bool Check(int x, int y, char plant) =>
    y >= 0 && y < input.Length && x >= 0 && x < input[y].Length && input[y][x] == plant;