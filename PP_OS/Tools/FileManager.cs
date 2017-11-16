using System.IO;
using System.Collections.Generic;

namespace PP_OS
{
    class FileManager
    {

        string[,] gamePaths;

        public FileManager(string path)
        {

            MoveDirectory(Game1.Settings[0], Game1.Settings[1]);
            gamePaths = GetGamesInDirectory(path);
        }

        public string[,] GamePaths
        {
            get
            {
                return gamePaths;
            }

            set
            {
                gamePaths = value;
            }
        }

        public static void MoveDirectory(string source, string target)
        {
            var stack = new Stack<Folders>();
            stack.Push(new Folders(source, target));

            while (stack.Count > 0)
            {
                var folders = stack.Pop();
                Directory.CreateDirectory(folders.Target);
                if(Directory.Exists(folders.Source))
                {

                    foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
                    {

                        string targetFile = Path.Combine(folders.Target, Path.GetFileName(file));

                        if (File.Exists(targetFile))
                        {

                            File.Delete(targetFile);
                        }
                        File.Copy(file, targetFile);
                    }

                    foreach (var folder in Directory.GetDirectories(folders.Source))
                    {

                        stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
                    }
                }
            }
        }

        string[,] GetGamesInDirectory(string path)
        {

            var currentDirectory = Game1.Settings[1] + path;
            var directories = Directory.GetDirectories(currentDirectory);
            string[,] fileNames = new string[directories.Length, 7];

            for (int i = 0; i < directories.Length; i++)
            {

                var currentName = directories[i].Replace(currentDirectory + @"\", "");

                fileNames[i, 4] = currentName;
                fileNames[i, 5] = path;

                DirectoryInfo files = new DirectoryInfo(directories[i]);
                FileInfo[] filesInDir = files.GetFiles("*" + currentName + "*.*");
                for (int j = 0; j < filesInDir.Length; j++)
                {

                    for (int x = 0; x < filesInDir.Length; x++)
                    {

                        if (filesInDir[x].ToString().EndsWith(".bat") || filesInDir[x].ToString().EndsWith(".exe"))
                        {

                            if(fileNames[i, 0] == null)
                            {

                                fileNames[i, 0] = directories[i] + @"\" + filesInDir[x];
                            }
                        }
                        else if (filesInDir[x].ToString().EndsWith("Title.png"))
                        {


                            fileNames[i, 6] = directories[i] + @"\" + filesInDir[x];
                        }
                        else if (filesInDir[x].ToString().EndsWith(".png"))
                        {

                            fileNames[i, 1] = directories[i] + @"\" + filesInDir[x];
                        }
                        else if (filesInDir[x].ToString().EndsWith(".txt"))
                        {

                            fileNames[i, 2] = directories[i] + @"\" + filesInDir[x];
                        }
                        else if (filesInDir[x].ToString().EndsWith(".mp3"))
                        {

                            fileNames[i, 3] = directories[i] + @"\" + filesInDir[x];
                        }
                        
                    }
                }
                
            }

            return fileNames;
        }
    }

    public class Folders
    {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public Folders(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }
}
