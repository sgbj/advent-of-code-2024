using System.Text.RegularExpressions;

var input = Regex.Split(await File.ReadAllTextAsync("input.txt"), "\r?\n\r?\n");

Console.WriteLine(Simulate());
Console.WriteLine(Simulate(2));

int Simulate(int scale = 1)
{
    var map = Regex.Split(input[0], "\r?\n").SelectMany((line, y) =>
            line.Select((c, x) => new Item(c, x * scale, y, c == '@' ? 1 : scale)).Where(item => item.C != '.')
                .ToArray())
        .ToArray();
    var moves = input[1];
    var robot = map.First(item => item.C == '@');

    foreach (var move in moves)
    {
        var (dx, dy) = move switch
        {
            '>' => (1, 0),
            '<' => (-1, 0),
            'v' => (0, 1),
            '^' => (0, -1),
            _ => (0, 0)
        };

        if (CanMove(robot, dx, dy))
        {
            Move(robot, dx, dy);
        }
    }

    return map.Where(item => item.C == 'O').Sum(item => 100 * item.Y + item.X);

    bool CanMove(Item item, int dx, int dy)
    {
        var items = GetCollisions(item, dx, dy);

        return items.All(i => i.C != '#' && CanMove(i, dx, dy));
    }

    void Move(Item item, int dx, int dy)
    {
        var items = GetCollisions(item, dx, dy);

        foreach (var i in items)
        {
            Move(i, dx, dy);
        }

        item.X += dx;
        item.Y += dy;
    }

    IEnumerable<Item> GetCollisions(Item item, int dx, int dy) =>
        map.Where(i => i != item && i.X < item.X + item.W + dx && item.X + dx < i.X + i.W && i.Y == item.Y + dy);
}

file class Item(char c, int x, int y, int w)
{
    public char C { get; } = c;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int W { get; } = w;
}