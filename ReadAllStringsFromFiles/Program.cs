
using ReadAllStringsFromFiles;

foreach (var str in await Utils.ReadAllAStringsFromFilesAsync("test1.txt", "test2.txt"))
{
    Console.WriteLine(str);
}