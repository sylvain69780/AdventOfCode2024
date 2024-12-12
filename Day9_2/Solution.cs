
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
        var end = diskMap.Count - 1;
        while (end >= 1)
        {
            var found = false;
            for (var i = 0; i < end; i++)
            {
                if (diskMap[i].free >= diskMap[end].size)
                {
                    var item = diskMap[end];
                    diskMap[end - 1] = (diskMap[end - 1].id, diskMap[end - 1].size, diskMap[end - 1].free + item.size + item.free);
                    diskMap.RemoveAt(end);
                    diskMap.Insert(i + 1, (item.id, item.size, diskMap[i].free-item.size));
                    diskMap[i] = (diskMap[i].id, diskMap[i].size, 0);
                    found = true;
                    break;
                }
            }
            if (!found)
                end--;
        }

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

            return disk.Select((value, id) => id * (long)(value == -1 ? 0 : value)).Sum();
    }
}