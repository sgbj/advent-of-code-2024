var input = await File.ReadAllTextAsync("input.txt");

Console.WriteLine(Compact());
Console.WriteLine(CompactWholeFiles());

int[] GetBlocks() => [..input.SelectMany((c, i) => Enumerable.Repeat(i % 2 == 0 ? i / 2 : -1, c - '0'))];

long GetChecksum(int[] blocks) => blocks.Select((id, index) => id == -1 ? 0L : id * index).Sum();

long Compact()
{
    var blocks = GetBlocks();

    for (int start = 0, end = blocks.Length - 1; start < end;)
    {
        if (blocks[start] != -1)
        {
            start++;
        }
        else if (blocks[end] == -1)
        {
            end--;
        }
        else
        {
            blocks[start] = blocks[end];
            blocks[end] = -1;
            start++;
            end--;
        }
    }

    return GetChecksum(blocks);
}

long CompactWholeFiles()
{
    var blocks = GetBlocks();

    for (var end = blocks.Length - 1; end >= 0;)
    {
        if (blocks[end] == -1)
        {
            end--;
            continue;
        }

        var size = 1;

        while (end - size >= 0 && blocks[end - size] == blocks[end])
        {
            size++;
        }

        for (int start = 0, free = 0; start < end; start++)
        {
            if (blocks[start] == -1)
            {
                if (++free == size)
                {
                    for (var i = 0; i < size; i++)
                    {
                        blocks[start - i] = blocks[end - i];
                        blocks[end - i] = -1;
                    }

                    break;
                }
            }
            else
            {
                free = 0;
            }
        }

        end -= size;
    }

    return GetChecksum(blocks);
}