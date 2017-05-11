using System.IO;

namespace PP_OS
{
    class FileManager
    {

        string[,] gamePaths;

        public FileManager(string path)
        {

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

        string[,] GetGamesInDirectory(string path)
        {

            var currentDirectory = @"Games\" + path;
            var directories = Directory.GetDirectories(currentDirectory);
            string[,] fileNames = new string[directories.Length, 6];

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
}
