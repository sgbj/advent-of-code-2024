var input = await File.ReadAllTextAsync("input.txt");

Console.WriteLine(Compact());
Console.WriteLine(Compact(true));

int[] GetBlocks() => [..input.SelectMany((c, i) => Enumerable.Repeat(i % 2 == 0 ? i / 2 : -1, c - '0'))];

long GetChecksum(int[] blocks) => blocks.Select((id, index) => id == -1 ? 0L : id * index).Sum();

long Compact(bool wholeFiles = false)
{
    var blocks = GetBlocks();

    var start = 0;
    var end = blocks.Length - 1;

    while (start < end)
    {
        if (blocks[start] != -1)
        {
            start++;
        }
        else if (blocks[end] == -1)
        {
            end--;
        }
        else if (wholeFiles)
        {
            end -= CompactWholeFile(blocks, start, end);
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

int CompactWholeFile(int[] blocks, int start, int end)
{
    var size = 1;

    while (end - size >= 0 && blocks[end - size] == blocks[end])
    {
        size++;
    }

    for (int i = start, free = 0; i < end; i++)
    {
        if (blocks[i] == -1)
        {
            free++;

            if (free == size)
            {
                for (var j = 0; j < size; j++)
                {
                    blocks[i - j] = blocks[end - j];
                    blocks[end - j] = -1;
                }

                break;
            }
        }
        else
        {
            free = 0;
        }
    }

    return size;
}