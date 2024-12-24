using System.Globalization;

var input = await File.ReadAllLinesAsync("input.txt");
var values = input.Where(line => line.Contains(": ")).Select(line => line.Split(": "))
    .ToDictionary(split => split[0], split => int.Parse(split[1]));
var gates = input.Where(line => line.Contains("->")).Select(line => line.Split())
    .ToDictionary(split => split[^1], split => split[..^2]);

Console.WriteLine(long.Parse(
    string.Join("", gates.Keys.OrderDescending().Where(k => k.StartsWith('z')).Select(GetValue)),
    NumberStyles.BinaryNumber));

int GetValue(string wire)
{
    if (values.TryGetValue(wire, out var output))
    {
        return output;
    }

    var gate = gates[wire];

    var input1 = GetValue(gate[0]);
    var input2 = GetValue(gate[2]);

    output = gate[1] switch
    {
        "AND" => input1 & input2,
        "OR" => input1 | input2,
        "XOR" => input1 ^ input2,
        _ => throw new InvalidOperationException()
    };

    values[wire] = output;

    return output;
}