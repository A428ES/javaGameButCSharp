using System.IO;

namespace JavaGameButCSharp{
    class FileManagement{
    public static void CopyDirectory(string source, string destination)
    {
        Directory.CreateDirectory(destination);

        Directory.GetFiles(source)
            .ToList()
            .ForEach(file => CopyFile(file, destination));

        Directory.GetDirectories(source)
            .ToList()
            .ForEach(directory => 
                CopyDirectory(directory, Path.Combine(destination, Path.GetFileName(directory))));
    }

    public static void CopyFile(string sourceFile, string destinationDirectory)
    {
        var fileName = Path.GetFileName(sourceFile);
        var destFile = Path.Combine(destinationDirectory, fileName);

        try
        {
            File.Copy(sourceFile, destFile, overwrite: true);
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error copying file {sourceFile}: {e.Message}");
        }
    }

        public static void DeleteDirectory(string target){
            string[] directories = Directory.GetFiles(target);

            try{
                foreach(string directory in directories){
                    File.Delete(directory);
                }

                File.Delete(target);
            } catch (IOException e){
                Console.WriteLine($"implement outputhandler for ioxexception {e}");
            }
        }
    }
}