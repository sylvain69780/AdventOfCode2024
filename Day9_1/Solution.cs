
internal class Solution
{
    private List<(int id, int size, int free)> diskMap;

    public Solution(string test)
    {
        diskMap = [];
        test = test + '0';
        for (int i = 0; i < test.Length / 2; i++)
        {
            diskMap.Add((i, test[i * 2] - '0', test[i * 2 + 1] - '0'));
        }
    }

    internal long Run()
    {
        var disk = new int[diskMap.Select(x => x.size + x.free).Sum()];
        var index = 0;
        var queue = 0;
        foreach (var (id, size, free) in diskMap)
        {
            for (int i = 0; i < size; i++)
                disk[index++] = id;
            queue = index - 1;
            for (int i = 0; i < free; i++)
                disk[index++] = -1;
        }
        var head = 0;
        while (disk[head] != -1) head++;
        do
        {
            disk[head] = disk[queue];
            disk[queue] = -1;
            while (disk[head] != -1) head++;
            while (disk[queue] == -1) queue--;
        } while (head < queue);

            return disk.Select((value, id) => id * (long)(value == -1 ? 0 : value)).Sum();
    }
}