var input = await File.ReadAllLinesAsync("input.txt");

var equations = input.Select(line => line.Split(": "))
    .Select(split => (Result: long.Parse(split[0]), Operands: split[1].Split(' ').Select(long.Parse).ToArray()))
    .ToList();

var answer1 = equations.Where(eq => IsSolvable(eq.Result, eq.Operands, ["+", "*"], eq.Operands[0]))
    .Sum(eq => eq.Result);

Console.WriteLine(answer1);

var answer2 = equations.Where(eq => IsSolvable(eq.Result, eq.Operands, ["+", "*", "||"], eq.Operands[0]))
    .Sum(eq => eq.Result);

Console.WriteLine(answer2);

bool IsSolvable(long result, long[] operands, string[] operators, long current, int index = 1) =>
    index == operands.Length
        ? current == result
        : operators.Any(op => IsSolvable(result, operands, operators, op switch
        {
            "+" => current + operands[index],
            "*" => current * operands[index],
            "||" => long.Parse($"{current}{operands[index]}"),
            _ => throw new InvalidOperationException()
        }, index + 1));