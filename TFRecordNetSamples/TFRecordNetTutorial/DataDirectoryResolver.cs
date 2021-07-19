using System.IO;
using System.Reflection;

namespace TFRecordNetTutorial
{
    public class DataDirectoryResolver
    {
        public DataDirectoryResolver()
        {
            var currentProjectDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var solutionDirectory = currentProjectDirectory;
            var rootDirectory = solutionDirectory;
            var binSubPath = $"bin{Path.DirectorySeparatorChar}";
            var binSubPathIndex = currentProjectDirectory.IndexOf(binSubPath); 
            if (binSubPathIndex >= 0)
            {
                currentProjectDirectory = currentProjectDirectory.Substring(0, binSubPathIndex).TrimEnd(Path.DirectorySeparatorChar);
                solutionDirectory = Directory.GetParent(currentProjectDirectory).FullName;
                rootDirectory = Directory.GetParent(solutionDirectory).FullName;
            }
            // var cwd = Environment.CurrentDirectory;
            // Console.WriteLine("CurrentDirectory : {0}, SourcePath : {1}",  cwd, sourcePath);
            ProjectRoot = currentProjectDirectory;
            DataBaseDirectory = Path.Combine(rootDirectory, "data");
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