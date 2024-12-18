using System.Text.RegularExpressions;

var input = (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => Regex.Matches(line, @"-?\d+").Select(match => int.Parse(match.Value)).ToArray())
    .ToArray();

Console.WriteLine(GetSafetyFactor(101, 103, 100));
GetSafetyFactor(101, 103, 10_000);

int GetSafetyFactor(int w, int h, int seconds)
{
    var robots = input.Select(robot => robot.ToArray()).ToArray();

    for (var second = 1; second <= seconds; second++)
    {
        foreach (var robot in robots)
        {
            robot[0] = (robot[0] + robot[2] + w) % w;
            robot[1] = (robot[1] + robot[3] + h) % h;
        }

        if (HasChristmasTree(robots, w, h))
        {
            Console.WriteLine(second);
            Console.WriteLine(string.Join("\n",
                Enumerable.Range(0, h).Select(y => string.Join("",
                    Enumerable.Range(0, w).Select(x => robots.Any(r => r[0] == x && r[1] == y) ? "#" : ".")))));
            break;
        }
    }

    return robots.Count(robot => robot[0] >= 0 && robot[0] < w / 2 && robot[1] >= 0 && robot[1] < h / 2) *
           robots.Count(robot => robot[0] > w / 2 && robot[0] < w && robot[1] >= 0 && robot[1] < h / 2) *
           robots.Count(robot => robot[0] >= 0 && robot[0] < w / 2 && robot[1] > h / 2 && robot[1] < h) *
           robots.Count(robot => robot[0] > w / 2 && robot[0] < w && robot[1] > h / 2 && robot[1] < h);
}

bool HasChristmasTree(int[][] robots, int w, int h)
{
    foreach (var robot in robots)
    {
        var hasChristmasTree = true;

        for (var dy = 0; dy < 5; dy++)
        {
            for (var dx = -dy; dx <= dy; dx++)
            {
                var x = (robot[0] + dx + w) % w;
                var y = (robot[1] + dy + h) % h;

                if (!robots.Any(r => r[0] == x && r[1] == y))
                {
                    hasChristmasTree = false;
                    break;
                }
            }
        }

        if (hasChristmasTree)
        {
            return true;
        }
    }

    return false;
}