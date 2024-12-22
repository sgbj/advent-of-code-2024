var initialSecretNumbers = (await File.ReadAllLinesAsync("input.txt")).Select(long.Parse).ToArray();

var sum = 0L;

foreach (var initialSecretNumber in initialSecretNumbers)
{
    var secretNumber = initialSecretNumber;

    for (var i = 0; i < 2000; i++)
    {
        secretNumber ^= secretNumber * 64;
        secretNumber %= 16777216;
        secretNumber ^= secretNumber / 32;
        secretNumber %= 16777216;
        secretNumber ^= secretNumber * 2048;
        secretNumber %= 16777216;
    }

    sum += secretNumber;
}

Console.WriteLine(sum);