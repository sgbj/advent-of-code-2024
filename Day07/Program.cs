var input = await File.ReadAllLinesAsync("input.txt");

var equations = input.Select(line => line.Split(": "))
    .Select(split => (Result: long.Parse(split[0]), Operands: split[1].Split(' ').Select(long.Parse).ToArray()))
    .ToList();

var answer1 = equations.Where(eq => IsSolvable(eq, ["+", "*"], eq.Operands[0]))
    .Sum(eq => eq.Result);

Console.WriteLine(answer1);

var answer2 = equations.Where(eq => IsSolvable(eq, ["+", "*", "||"], eq.Operands[0]))
    .Sum(eq => eq.Result);

Console.WriteLine(answer2);

bool IsSolvable((long Result, long[] Operands) equation, string[] operators, long current, int index = 1) =>
    index == equation.Operands.Length
        ? current == equation.Result
        : operators.Any(op => IsSolvable(equation, operators, op switch
        {
            "+" => current + equation.Operands[index],
            "*" => current * equation.Operands[index],
            "||" => long.Parse($"{current}{equation.Operands[index]}"),
            _ => throw new InvalidOperationException()
        }, index + 1));