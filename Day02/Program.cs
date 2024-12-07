var reports = (await File.ReadAllLinesAsync("input.txt"))
    .Select(report => report.Split(" ").Select(int.Parse).ToArray())
    .ToList();

var count1 = reports.Count(IsSafe);

Console.WriteLine(count1);

var count2 = reports.Count(IsSafeWithProblemDampener);

Console.WriteLine(count2);

bool IsSafeWithProblemDampener(int[] levels) =>
    IsSafe(levels) || levels.Where((_, i) => IsSafe([..levels.Where((_, j) => i != j)])).Any();

bool IsSafe(int[] levels)
{
    var diffs = levels.Zip(levels.Skip(1), (a, b) => b - a).ToArray();
    var sign = Math.Sign(diffs.First());
    return diffs.All(diff => sign == Math.Sign(diff) && Math.Abs(diff) is >= 1 and <= 3);
}