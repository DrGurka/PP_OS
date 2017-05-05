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

                    switch(j)
                    {

                        case 0:

                            for (int x = 0; x < filesInDir.Length; x++)
                            {

                                if (filesInDir[x].ToString().EndsWith(".exe"))
                                {

                                    fileNames[i, j] = directories[i] + @"\" + filesInDir[x];
                                }
                            }
                            break;
                        case 1:
                            for (int x = 0; x < filesInDir.Length; x++)
                            {

                                if (filesInDir[x].ToString().EndsWith(".png"))
                                {

                                    fileNames[i, j] = directories[i] + @"\" + filesInDir[x];
                                }
                            }
                            break;
                        case 2:
                            for (int x = 0; x < filesInDir.Length; x++)
                            {

                                if (filesInDir[x].ToString().EndsWith(".txt"))
                                {

                                    fileNames[i, j] = directories[i] + @"\" + filesInDir[x];
                                }
                            }
                            break;
                        case 3:
                            for (int x = 0; x < filesInDir.Length; x++)
                            {

                                if (filesInDir[x].ToString().EndsWith(".mp3"))
                                {

                                    fileNames[i, j] = directories[i] + @"\" + filesInDir[x];
                                }
                            }
                            break;
                    }
                }
                
            }

            return fileNames;
        }
    }
}
