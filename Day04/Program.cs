var input = await File.ReadAllLinesAsync("input.txt");

var answer1 = 0;
var answer2 = 0;

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        answer1 += Search(x, y, "XMAS", [(1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (-1, -1), (1, -1), (-1, 1)]);
        answer2 += (Search(x - 1, y - 1, "MAS", [(1, 1)]) > 0 || Search(x - 1, y - 1, "SAM", [(1, 1)]) > 0) &&
            (Search(x - 1, y + 1, "MAS", [(1, -1)]) > 0 || Search(x - 1, y + 1, "SAM", [(1, -1)]) > 0) ? 1 : 0;
    }
}

Console.WriteLine(answer1);
Console.WriteLine(answer2);

int Search(int x, int y, string s, (int dx, int dy)[] dirs) =>
    dirs.Sum(d => s.Select((c, i) => Check(x + d.dx * i, y + d.dy * i, c)).All(b => b) ? 1 : 0);

bool Check(int x, int y, char c) =>
    y >= 0 && y < input.Length && x >= 0 && x < input[y].Length && input[y][x] == c;
