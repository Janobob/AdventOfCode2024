using AoCHelper;

namespace AdventOfCode2024;

public sealed class Day09 : BaseDay
{
    private readonly List<Block> _blocks = [];

    public Day09()
    {
        var input = File.ReadAllText(InputFilePath);
        var isFile = true;
        var i = 0L;

        foreach (var length in input.Select(t => t - '0'))
        {
            if (length > 0)
            {
                _blocks.Add(new Block(isFile ? i++ : -1, length));
            }

            isFile = !isFile;
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var blocks = _blocks.ToList();

        // compact blocks
        while (true)
        {
            var spaceIndex = blocks.FindIndex(t => t.Id == -1);
            var fileIndex = blocks.FindLastIndex(t => t.Id != -1);

            if (spaceIndex > fileIndex)
            {
                break;
            }

            var spaceBlock = blocks[spaceIndex];
            var fileBlock = blocks[fileIndex];

            if (spaceBlock.Length >= fileBlock.Length)
            {
                var moveAmount = fileBlock.Length;
                blocks[spaceIndex] = new Block(fileBlock.Id, moveAmount);
                blocks[fileIndex] = fileBlock.Length > moveAmount
                    ? new Block(fileBlock.Id, fileBlock.Length - moveAmount)
                    : new Block(-1, 0);

                if (spaceBlock.Length > moveAmount)
                {
                    blocks.Insert(spaceIndex + 1, new Block(-1, spaceBlock.Length - moveAmount));
                }
            }
            else
            {
                var moveAmount = spaceBlock.Length;
                blocks[spaceIndex] = new Block(fileBlock.Id, moveAmount);
                blocks[fileIndex] = new Block(fileBlock.Id, fileBlock.Length - moveAmount);
                blocks.Insert(fileIndex + 1, new Block(-1, moveAmount));
            }
        }

        // Calculate checksum
        long checksum = 0, position = 0;

        foreach (var block in blocks)
        {
            if (block.Id != -1)
            {
                for (var j = 0; j < block.Length; j++)
                {
                    checksum += block.Id * position++;
                }
            }
            else
            {
                position += block.Length;
            }
        }

        return new ValueTask<string>($"{checksum}");
    }

    public override ValueTask<string> Solve_2()
    {
        var blocks = _blocks.ToList();

        while (true)
        {
            var didUpdate = false;
            foreach (var space in blocks.Where(x => x.Id == -1).ToList())
            {
                var file = blocks.LastOrDefault(t =>
                    t.Id != -1 && t.Length <= space.Length && blocks.IndexOf(t) > blocks.IndexOf(space));

                if (file == null)
                {
                    continue;
                }

                didUpdate = true;
                if (space.Length == file.Length)
                {
                    space.Id = file.Id;
                    file.Id = -1;
                }

                if (space.Length > file.Length)
                {
                    space.Id = file.Id;
                    var newSpace = new Block(-1, space.Length - file.Length);
                    blocks.Insert(blocks.IndexOf(space) + 1, newSpace);
                    space.Length = file.Length;
                    file.Id = -1;
                }
            }

            if (!didUpdate)
            {
                break;
            }
        }

        long checksum = 0, position = 0;

        foreach (var block in blocks)
        {
            if (block.Id != -1)
            {
                for (var j = 0; j < block.Length; j++)
                {
                    checksum += block.Id * position++;
                }
            }
            else
            {
                position += block.Length;
            }
        }

        return new ValueTask<string>($"{checksum}");
    }

    private static void printBlocks(List<Block> blocks)
    {
        foreach (var block in blocks)
        {
            Enumerable.Repeat(block.Id == -1 ? "." : block.Id.ToString(), (int)block.Length).ToList()
                .ForEach(Console.Write);
        }

        Console.WriteLine();
    }

    private class Block(long id, long length)
    {
        public long Id { get; set; } = id;
        public long Length { get; set; } = length;
    }
}