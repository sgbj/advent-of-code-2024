using System.Text.RegularExpressions;

var input = Regex.Split(await File.ReadAllTextAsync("input.txt"), "\r?\n\r?\n");
var registers = Regex.Matches(input[0], @"\d+").Select(m => int.Parse(m.Value)).ToArray();
var program = Regex.Matches(input[1], @"\d+").Select(m => int.Parse(m.Value)).ToArray();

var instructionPointer = 0;
var output = new List<int>();

while (instructionPointer < program.Length)
{
    var opcode = program[instructionPointer];
    var operand = program[instructionPointer + 1];

    switch (opcode)
    {
        case 0:
            registers[0] = (int)(registers[0] / Math.Pow(2, GetCombo(operand)));
            break;

        case 1:
            registers[1] ^= operand;
            break;

        case 2:
            registers[1] = GetCombo(operand) % 8;
            break;

        case 3:
            if (registers[0] != 0)
            {
                instructionPointer = operand;
                continue;
            }

            break;

        case 4:
            registers[1] ^= registers[2];
            break;

        case 5:
            output.Add(GetCombo(operand) % 8);
            break;

        case 6:
            registers[1] = (int)(registers[0] / Math.Pow(2, GetCombo(operand)));
            break;

        case 7:
            registers[2] = (int)(registers[0] / Math.Pow(2, GetCombo(operand)));
            break;
    }

    instructionPointer += 2;
}

Console.WriteLine(string.Join(",", output));

int GetCombo(int comboOperator) => comboOperator switch
{
    >= 0 and <= 3 => comboOperator,
    4 => registers[0],
    5 => registers[1],
    6 => registers[2],
    _ => throw new InvalidOperationException()
};