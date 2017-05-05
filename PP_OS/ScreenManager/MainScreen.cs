using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
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
        static Button button;
        FileManager fileManager;
        Thumbnail pausedThumbNail;

        Texture2D sign;
        SpriteFont font;

        bool released;

        public bool IsPaused { get; private set; }

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

        public MainScreen(IGameScreenManager screenManager, GraphicsDevice graphicsDevice)
        {

            this.screenManager = screenManager;
            this.graphicsDevice = graphicsDevice;
        }

        public void Initialize(ContentManager contentManager)
        {

            thumbnails = new List<Thumbnail>();
            fileManager = new FileManager();

            button = new Button(Button.ButtonTexture.ButtonA, new Vector2(32, Game1.ScreenSize.Y - 66), 1.0f, "Play", false, 400, 3, 0, 0);
            Game1.ThumbnailCount = fileManager.GamePaths.GetLength(0) - 1;

            sign = contentManager.Load<Texture2D>(@"sign");
            font = contentManager.Load<SpriteFont>(@"font2");

            FileStream fileStream;

            for (int i = 0; i < fileManager.GamePaths.GetLength(0); i++)
            {

                if (fileManager.GamePaths[i, 1] != null)
                {

                    fileStream = new FileStream(fileManager.GamePaths[i, 1], FileMode.Open);
                    Texture2D texture = Texture2D.FromStream(graphicsDevice, fileStream);

                    string songPath = Directory.GetCurrentDirectory() + @"\" + fileManager.GamePaths[i, 3];

                    Song song;

                    if(fileManager.GamePaths[i, 3] != null)
                    {

                        song = Song.FromUri(songPath, new Uri(songPath, UriKind.Relative));
                    }
                    else
                    {

                        song = null;
                    }

                    string tmpString;

                    if(fileManager.GamePaths[i, 2] != null)
                    {

                        tmpString = File.ReadAllText(fileManager.GamePaths[i, 2]);
                    }
                    else
                    {

                        tmpString = null;
                    }

                    thumbnails.Add(new Thumbnail(texture, new Vector2((Game1.ScreenSize.X / 2f) + (640 * i), (Game1.ScreenSize.Y / 2f)), tmpString, fileManager.GamePaths[i, 0], 0.1f, i, fileManager.GamePaths[i, 4], song));
                }
            }
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

                        if (thumbnail.ProcessRunning)
                        {

                            Pause();
                            pausedThumbNail = thumbnail;
                        }
                    }

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

            if(thumbnails.Count <= 0)
            {

                spriteBatch.DrawString(Game1.SpriteFont, "No games in the Games folder!", Game1.ScreenSize / 2f, Color.Black, 0.0f, Game1.SpriteFont.MeasureString("No games in the game folder!") / 2f, 1f, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(font, "Read Info.txt in the Games folder for more information", new Vector2(Game1.ScreenSize.X / 2f, (Game1.ScreenSize.Y / 2f) + 16), Color.Black, 0.0f, new Vector2(font.MeasureString("Read Info.txt in the Games folder for more information").X / 2f, 0), 1f, SpriteEffects.None, 0.1f);
            }

            spriteBatch.Draw(sign, new Vector2(Game1.ScreenSize.X / 2f, 0), null, Color.White, 0.0f, new Vector2(sign.Width / 2f, -1), 2f, SpriteEffects.None, 0.2f);
        }

        public void HandleInput(GameTime gameTime)
        {

            if (released)
            {

                if (Input.LeftThumbstick.X > 0.2 || Input.DPadRightPressed)
                {

                    if (Game1.CurrentThumbnail < Game1.ThumbnailCount)
                    {

                        Game1.CurrentThumbnail++;
                    }
                    else
                    {

                        Game1.CurrentThumbnail = 0;
                    }
                    released = false;
                }
                else if (Input.LeftThumbstick.X < -0.2 || Input.DPadLeftPressed)
                {

                    if (Game1.CurrentThumbnail > 0)
                    {

                        Game1.CurrentThumbnail--;
                    }
                    else
                    {

                        Game1.CurrentThumbnail = Game1.ThumbnailCount;
                    }
                    released = false;
                }
            }
            else if (Input.LeftThumbstick.X > -0.2 && Input.LeftThumbstick.X < 0.2)
            {

                released = true;
            }

            if(Input.Button3Pressed)
            {

                foreach(var thumbnail in thumbnails)
                {

                    if(thumbnail.Index == Game1.CurrentThumbnail)
                    {

                        thumbnail.OpenThumbnail(screenManager);
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
