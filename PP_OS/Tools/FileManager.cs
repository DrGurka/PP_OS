using System.IO;

namespace PP_OS
{
    class FileManager
    {

        string[,] gamePaths;

        public FileManager()
        {

            gamePaths = GetGames();
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

        string[,] GetGames()
        {

            
            var directories = Directory.GetDirectories(@"Games");
            string[,] fileNames = new string[directories.Length, 5];

            for (int i = 0; i < directories.Length; i++)
            {

                var currentName = directories[i].Replace(@"Games\", "");

                fileNames[i, 4] = currentName;

                DirectoryInfo files = new DirectoryInfo(directories[i]);
                FileInfo[] filesInDir = files.GetFiles("*" + currentName + "*.*");
                for (int j = 0; j < filesInDir.Length; j++)
                {

                    for (int x = 0; x < filesInDir.Length; x++)
                    {

                        if (filesInDir[x].ToString().EndsWith(".exe"))
                        {

                            fileNames[i, 0] = directories[i] + @"\" + filesInDir[x];
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
