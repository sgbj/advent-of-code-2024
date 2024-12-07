using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

var answer1 = Regex.Matches(input, @"mul\((\d+),(\d+)\)")
    .Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

Console.WriteLine(answer1);

var (_, answer2) = Regex.Matches(input, @"do\(\)|don't\(\)|mul\((\d+),(\d+)\)")
    .Aggregate((mulEnabled: true, total: 0), (state, match) => match.Value switch
    {
        "do()" => (true, state.total),
        "don't()" => (false, state.total),
        _ => state.mulEnabled
            ? (true, state.total + int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value))
            : state
    });

Console.WriteLine(answer2);