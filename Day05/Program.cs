var input = await File.ReadAllLinesAsync("input.txt");

var rules = input.Where(line => line.Contains('|'))
    .Select(line => line.Split('|').Select(int.Parse).ToArray())
    .ToList();
var updates = input.Where(line => line.Contains(','))
    .Select(line => line.Split(',').Select(int.Parse).ToArray())
    .ToList();

var answer1 = updates.Where(InOrder).Sum(update => update[update.Length / 2]);

Console.WriteLine(answer1);

var answer2 = updates.Where(update => !InOrder(update)).Select(Order).Sum(update => update[update.Length / 2]);

Console.WriteLine(answer2);

bool InOrder(int[] update) => Enumerable.Range(0, update.Length - 1)
    .All(i => !rules.Any(r => update[i] == r[1] && update[i + 1] == r[0]));

int[] Order(int[] update) =>
[
    ..update.Order(Comparer<int>.Create((a, b) =>
        rules.Any(r => r[0] == a && r[1] == b) ? -1 :
        rules.Any(r => r[0] == b && r[1] == a) ? 1 : 0))
];