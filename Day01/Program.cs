var lines = await File.ReadAllLinesAsync("input.txt");

var values = lines.Select(line => line.Split("   ").Select(int.Parse).ToArray()).ToList();

var list1 = values.Select(value => value[0]).Order().ToList();
var list2 = values.Select(value => value[1]).Order().ToList();

var totalDistance = list1.Zip(list2, (value1, value2) => Math.Abs(value1 - value2)).Sum();

Console.WriteLine(totalDistance);

var similarityScore = list1.Sum(value1 => value1 * list2.Count(value2 => value2 == value1));

Console.WriteLine(similarityScore);
