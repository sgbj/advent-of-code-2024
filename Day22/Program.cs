var initialSecretNumbers = (await File.ReadAllLinesAsync("input.txt")).Select(long.Parse).ToArray();

Console.WriteLine(initialSecretNumbers.Sum(initialSecretNumber => GenerateSecretNumbers(initialSecretNumber).Last()));

var sequences = new Dictionary<(string Sequence, int Monkey), long>();

for (var monkey = 0; monkey < initialSecretNumbers.Length; monkey++)
{
    var secretNumbers = GenerateSecretNumbers(initialSecretNumbers[monkey]).ToArray();
    var bananas = secretNumbers.Select(secretNumber => secretNumber % 10).ToArray();
    var changes = bananas.Zip(bananas.Skip(1), (a, b) => b - a).ToArray();

    for (var i = 4; i < secretNumbers.Length; i++)
    {
        var sequence = string.Join(",", changes[(i - 4)..i]);
        sequences.TryAdd((sequence, monkey), bananas[i]);
    }
}

Console.WriteLine(sequences.GroupBy(x => x.Key.Sequence).Select(g => g.Sum(x => x.Value)).Max());

static IEnumerable<long> GenerateSecretNumbers(long secretNumber)
{
    for (var i = 0; i < 2000; i++)
    {
        secretNumber ^= secretNumber * 64;
        secretNumber %= 16777216;
        secretNumber ^= secretNumber / 32;
        secretNumber %= 16777216;
        secretNumber ^= secretNumber * 2048;
        secretNumber %= 16777216;
        yield return secretNumber;
    }
}