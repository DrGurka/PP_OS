using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework.Media;

namespace PP_OS
{
    class MainScreen : IGameScreen
    {

        private readonly IGameScreenManager screenManager;
        private bool exitGame;

        GraphicsDevice graphicsDevice;

        List<Thumbnail> thumbnails;
        static List<string> platforms;
        static Button button;
        FileManager fileManager;
        Thumbnail pausedThumbNail;
        static Thumbnail currentThumbnail;

        static bool signInvert;

        int[] currentIndex;
        int lastPlatform;

        Arrow arrowUp;
        Arrow arrowDown;

        Texture2D sign;
        SpriteFont font;

        bool releasedY, releasedX;

        public bool IsPaused { get; private set; }

        public static int PlatformsCount
        {

            get { return platforms.Count; }
        }

        public static Button Button
        {
            get
            {
                return button;
            }

            set
            {
                button = value;
            }
        }

        public static bool SignInvert
        {
            get
            {
                return signInvert;
            }

            set
            {
                signInvert = value;
            }
        }

        internal static Thumbnail CurrentThumbnail
        {
            get
            {
                return currentThumbnail;
            }

            set
            {
                currentThumbnail = value;
            }
        }

        public MainScreen(IGameScreenManager screenManager, GraphicsDevice graphicsDevice)
        {

            this.screenManager = screenManager;
            this.graphicsDevice = graphicsDevice;
        }

        public void Initialize(ContentManager contentManager)
        {

            platforms = new List<string>();

            var directories = Directory.GetDirectories(@"Games");

            for (int i = 0; i < directories.Length; i++)
            {

                platforms.Add(directories[i].Replace(@"Games\", ""));
            }

            button = new Button(Button.ButtonTexture.ButtonA, new Vector2(32, Game1.ScreenSize.Y - 66), 1.0f, "Select", false, 400, 3, 0, 0);
            button.Alpha = 1;

            sign = contentManager.Load<Texture2D>(@"sign");
            font = contentManager.Load<SpriteFont>(@"font2");

            currentIndex = new int[platforms.Count];

            LoadThumbnails(platforms[Game1.CurrentPlatform]);

            arrowDown = new Arrow(new Vector2(Game1.ScreenSize.X / 2f, (Game1.ScreenSize.Y / 2f) - 210), true);
            arrowUp = new Arrow(new Vector2(Game1.ScreenSize.X / 2f, (Game1.ScreenSize.Y / 2f) + 210), false);
        }

        void LoadThumbnails(string path)
        {

            Game1.CurrentThumbnail = currentIndex[Game1.CurrentPlatform];

            fileManager = new FileManager(path);
            thumbnails = new List<Thumbnail>();

            FileStream fileStream;

            for (int i = 0; i < fileManager.GamePaths.GetLength(0); i++)
            {

                if (fileManager.GamePaths[i, 1] != null)
                {

                    fileStream = new FileStream(fileManager.GamePaths[i, 1], FileMode.Open);
                    Texture2D texture = Texture2D.FromStream(graphicsDevice, fileStream);

                    string songPath = Directory.GetCurrentDirectory() + @"\" + fileManager.GamePaths[i, 3];

                    Song song;

                    if (fileManager.GamePaths[i, 3] != null)
                    {

                        song = Song.FromUri(songPath, new Uri(songPath, UriKind.Relative));
                    }
                    else
                    {

                        song = null;
                    }

                    string tmpString;

                    if (fileManager.GamePaths[i, 2] != null)
                    {

                        tmpString = File.ReadAllText(fileManager.GamePaths[i, 2]);
                    }
                    else
                    {

                        tmpString = null;
                    }

                    thumbnails.Add(new Thumbnail(texture, new Vector2((Game1.ScreenSize.X / 2f) + ((i - Game1.CurrentThumbnail)), (Game1.ScreenSize.Y / 2f) + ((lastPlatform) * 135)), tmpString, fileManager.GamePaths[i, 0], 0.1f, i, fileManager.GamePaths[i, 4], song, fileManager.GamePaths[i, 5], Game1.CurrentPlatform));
                }
            }

            Game1.ThumbnailCount = fileManager.GamePaths.GetLength(0) - 1;
        }

        public void ChangeBetweenScreens()
        {

            if (exitGame)
            {

                screenManager.Exit();
            }
        }

        public void Pause()
        {

            IsPaused = true;
        }

        public void Resume()
        {

            IsPaused = false;
        }

        public void Update(GameTime gameTime)
        {

            if (pausedThumbNail == null)
            {

                if(!Game1.Paused)
                {

                    foreach (var thumbnail in thumbnails)
                    {

                        thumbnail.Update(gameTime);
                        if(thumbnail.Index == Game1.CurrentThumbnail)
                        {

                            currentThumbnail = thumbnail;
                        }

                        if (thumbnail.ProcessRunning)
                        {

                            Pause();
                            pausedThumbNail = thumbnail;
                        }
                    }

                    arrowUp.Update(gameTime);
                    arrowDown.Update(gameTime);
                    button.Update(gameTime);
                }
            }
            else
            {

                pausedThumbNail.Update(gameTime);
                if (!pausedThumbNail.ProcessRunning)
                {

                    Resume();
                    pausedThumbNail = null;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (var thumbnail in thumbnails)
            {

                thumbnail.Draw(spriteBatch);

                if(thumbnail.Index == Game1.CurrentThumbnail && thumbnail.ExePath != null)
                {

                    button.Draw(spriteBatch);
                }
            }

            if (thumbnails.Count <= 0)
            {

                spriteBatch.DrawString(Game1.SpriteFont, "No games in the Games folder!", Game1.ScreenSize / 2f, Color.Black, 0.0f, Game1.SpriteFont.MeasureString("No games in the game folder!") / 2f, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(font, "Read Info.txt in the Games folder for more information", new Vector2(Game1.ScreenSize.X / 2f, (Game1.ScreenSize.Y / 2f) + 16), Color.Black, 0.0f, new Vector2(font.MeasureString("Read Info.txt in the Games folder for more information").X / 2f, 0), 1f, SpriteEffects.None, 0.1f);
            }

            if (platforms.Count > 1)
            {

                arrowUp.Draw(spriteBatch);
                arrowDown.Draw(spriteBatch);

                var upperPlatform = Game1.CurrentPlatform < platforms.Count - 1 ? platforms[Game1.CurrentPlatform + 1] : platforms[0];
                spriteBatch.DrawString(Game1.SpriteFont, upperPlatform, new Vector2(Game1.ScreenSize.X / 2f, arrowUp.Position.Y - 26), Color.Black, 0.0f, Game1.SpriteFont.MeasureString(upperPlatform) / 2f, 1f, SpriteEffects.None, 0.5f);

                var lowerPlatform = Game1.CurrentPlatform > 0 ? platforms[Game1.CurrentPlatform - 1] : platforms[platforms.Count - 1];
                spriteBatch.DrawString(Game1.SpriteFont, lowerPlatform, new Vector2(Game1.ScreenSize.X / 2f, arrowDown.Position.Y + 36), Color.Black, 0.0f, Game1.SpriteFont.MeasureString(lowerPlatform) / 2f, 1f, SpriteEffects.None, 0.5f);
            }
            
            spriteBatch.Draw(sign, new Vector2(Game1.ScreenSize.X / 2f, Game1.ScreenSize.Y), null, (signInvert ? Color.White : Color.Black), 0.0f, new Vector2(sign.Width / 2f, sign.Height + 1), 2f, SpriteEffects.None, 1.0f);
        }

        public void HandleInput(GameTime gameTime)
        {

            if (releasedY)
            {

                if (Input.LeftThumbstick.Y > 0.2 || Input.DPadDownPressed)
                {

                    if (platforms.Count > 1)
                    {

                        if (Game1.CurrentPlatform < platforms.Count - 1)
                        {

                            lastPlatform = 1;
                            Game1.CurrentPlatform++;
                            LoadThumbnails(platforms[Game1.CurrentPlatform]);
                            arrowUp.Activate();
                        }
                        else
                        {

                            lastPlatform = 1;
                            Game1.CurrentPlatform = 0;
                            LoadThumbnails(platforms[Game1.CurrentPlatform]);
                            arrowUp.Activate();
                        }
                    }
                    releasedY = false;
                }
                else if (Input.LeftThumbstick.Y < -0.2 || Input.DPadUpPressed)
                {

                    if (platforms.Count > 1)
                    {

                        if (Game1.CurrentPlatform > 0)
                        {

                            lastPlatform = -1;
                            Game1.CurrentPlatform--;
                            LoadThumbnails(platforms[Game1.CurrentPlatform]);
                            arrowDown.Activate();
                        }
                        else
                        {

                            lastPlatform = -1;
                            Game1.CurrentPlatform = platforms.Count - 1;
                            LoadThumbnails(platforms[Game1.CurrentPlatform]);
                            arrowDown.Activate();
                        }
                    }
                    releasedY = false;
                }

                
            }
            else if (Input.LeftThumbstick.Y > -0.2 && Input.LeftThumbstick.Y < 0.2)
            {

                releasedY = true;
            }

            if (releasedX)
            {

                if (Input.LeftThumbstick.X > 0.2 || Input.DPadRightPressed)
                {

                    if (Game1.CurrentThumbnail < Game1.ThumbnailCount)
                    {

                        Game1.CurrentThumbnail++;
                        currentIndex[Game1.CurrentPlatform] = Game1.CurrentThumbnail;
                    }
                    else
                    {

                        Game1.CurrentThumbnail = 0;
                        currentIndex[Game1.CurrentPlatform] = Game1.CurrentThumbnail;
                    }
                    releasedX = false;
                }
                else if (Input.LeftThumbstick.X < -0.2 || Input.DPadLeftPressed)
                {

                    if (Game1.CurrentThumbnail > 0)
                    {

                        Game1.CurrentThumbnail--;
                        currentIndex[Game1.CurrentPlatform] = Game1.CurrentThumbnail;
                    }
                    else
                    {

                        Game1.CurrentThumbnail = Game1.ThumbnailCount;
                        currentIndex[Game1.CurrentPlatform] = Game1.CurrentThumbnail;
                    }
                    releasedX = false;
                }
            }
            else if (Input.LeftThumbstick.X > -0.2 && Input.LeftThumbstick.X < 0.2)
            {

                releasedX = true;
            }

            if (Input.Button3Pressed)
            {

                foreach(var thumbnail in thumbnails)
                {

                    if(thumbnail.Index == Game1.CurrentThumbnail)
                    {

                        thumbnail.OpenThumbnail(screenManager);
                        thumbnail.DisplayingInfo = true;
                    }
                }
            }

            if (Input.Button1Pressed)
            {

                foreach (var thumbnail in thumbnails)
                {

                    if (thumbnail.Index == Game1.CurrentThumbnail)
                    {

                        thumbnail.RunProgram();
                    }
                }
            }
        }

        public void Dispose()
        {


        }
    }
}
