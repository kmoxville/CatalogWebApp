using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAllStringsFromFiles
{
    internal class Utils
    {
        public static async Task<List<string>> ReadAllAStringsFromFilesAsync(params string[] filePaths)
        {
            List<string> result = new List<string>();
            
            foreach (string path in filePaths)
            {
                result.AddRange(await File.ReadAllLinesAsync(path));
            }

            return result;
        }
    }
}
