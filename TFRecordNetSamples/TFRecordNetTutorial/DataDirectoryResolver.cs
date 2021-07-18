using System.IO;
using System.Reflection;

namespace TFRecordNetTutorial
{
    public class DataDirectoryResolver
    {
        public DataDirectoryResolver()
        {
            var baseDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var dataRootDirectory = baseDirectory;
            var binSubPath = $"bin{Path.DirectorySeparatorChar}";
            var binSubPathIndex = baseDirectory.IndexOf(binSubPath); 
            if (binSubPathIndex >= 0)
            {
                baseDirectory = baseDirectory.Substring(0, binSubPathIndex).TrimEnd(Path.DirectorySeparatorChar);
                dataRootDirectory = Directory.GetParent(baseDirectory).FullName; 
            }
            // var cwd = Environment.CurrentDirectory;
            // Console.WriteLine("CurrentDirectory : {0}, SourcePath : {1}",  cwd, sourcePath);
            ProjectRoot = baseDirectory;
            DataBaseDirectory = Path.Combine(dataRootDirectory, "data");
        }

        public string DataBaseDirectory { get; set; }

        public string? ProjectRoot { get; set; }

        public string GetDataDirectory(string directoryName)
        {
            var dir = Path.Combine(DataBaseDirectory, directoryName);
            return dir;
        }
    }
}