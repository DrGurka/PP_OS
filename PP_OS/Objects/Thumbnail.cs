using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;
using System;
using Microsoft.Xna.Framework.Media;

namespace PP_OS
{
    class Thumbnail
    {

        Texture2D texture;
        Vector2 position;

        string info;
        string exePath;
        string name;

        float layer;
        float alpha;

        int index;
                
        bool processRunning;
        bool playing;
        bool paused;

        Song song;
        Button button;

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public bool ProcessRunning
        {
            get
            {
                return processRunning;
            }

            set
            {
                processRunning = value;
            }
        }

        public string ExePath
        {
            get
            {
                return exePath;
            }

            set
            {
                exePath = value;
            }
        }

        public Thumbnail(Texture2D texture, Vector2 position, string info, string exePath, float layer, int index, string name, Song song)
        {

            this.texture = texture;
            this.position = position;
            this.info = info;
            this.exePath = exePath;
            this.layer = layer;
            this.index = index;
            this.name = name;
            this.song = song;

            button = new Button(Button.ButtonTexture.ButtonY, new Vector2(position.X + (texture.Width / 2) - 45, position.Y + (texture.Height / 2f) + 4), layer, "Info", true, 400, 2, MainScreen.Button.CurrentFrame, MainScreen.Button.TimeSinceLastFrame);
        }

        public void Update(GameTime gameTime)
        {

            if (!Game1.Paused)
            {

                button.Position = new Vector2(position.X + (texture.Width / 2) - button.Width, position.Y + (texture.Height / 2f) + 4);
                button.Update(gameTime);

                position.X += (((((index - Game1.CurrentThumbnail) * 640) + ((Game1.ScreenSize.X / 2f))) - (position.X)) * 0.1f) * Game1.Delta;

                if (position.X < (Game1.ScreenSize.X / 2))
                {

                    alpha = Math.Max(Math.Min(position.X / (Game1.ScreenSize.X / 2), 1), 0);
                }
                else
                {

                    alpha = Math.Max(Math.Min((Game1.ScreenSize.X - position.X) / (Game1.ScreenSize.X / 2), 1), 0);
                }
            }

            processRunning = IsProcessOpen(name);

            if(Game1.CurrentThumbnail == index)
            {

                if(Game1.Paused && !processRunning)
                {

                    Game1.Paused = false;
                }

                if (song != null)
                {
                    if (!processRunning)
                    {

                        if (!playing)
                        {

                            if (!paused)
                            {

                                MediaPlayer.Play(song);
                                playing = true;
                            }
                            else
                            {

                                MediaPlayer.Resume();
                                paused = false;
                                playing = true;
                            }
                        }
                    }
                    else
                    {

                        if (playing)
                        {

                            MediaPlayer.Pause();
                            playing = false;
                            paused = true;
                        }
                    }
                }
                else
                {

                    MediaPlayer.Stop();
                }
            }
            else
            {

                playing = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (processRunning)
            {

                spriteBatch.Draw(Game1.Rect, new Rectangle((int)position.X - (texture.Width / 2), (int)position.Y - (texture.Height / 2), texture.Width, texture.Height), null, Color.Black * 0.5f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                spriteBatch.DrawString(Game1.SpriteFont, "Running!", new Vector2(position.X, position.Y), Color.White, 0.0f, Game1.SpriteFont.MeasureString("Running!") / 2f, 1, SpriteEffects.None, 1.0f);
            }

            spriteBatch.Draw(texture, position, null, Color.White * alpha, 0.0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, layer);

            if (Game1.CurrentThumbnail == index)
            {

                if(info != null)
                {

                    button.Draw(spriteBatch);
                }

                spriteBatch.DrawString(Game1.SpriteFont, name, new Vector2(Game1.ScreenSize.X / 2f, Game1.ScreenSize.Y / 5f), Color.Black * alpha, 0.0f, new Vector2(Game1.SpriteFont.MeasureString(name).X / 2, 0), 2, SpriteEffects.None, 0.1f);

                spriteBatch.Draw(Game1.Rect, new Rectangle((int)(Game1.ScreenSize.X / 2f) - (int)Game1.SpriteFont.MeasureString(name).X, (int)(Game1.ScreenSize.Y / 4f), (int)Game1.SpriteFont.MeasureString(name).X * 2, 4), null, Color.Black * alpha, 0.0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(Game1.Rect, new Rectangle((int)(Game1.ScreenSize.X / 2f) - (int)Game1.SpriteFont.MeasureString(name).X, (int)(Game1.ScreenSize.Y / 4f) - (int)(Game1.SpriteFont.MeasureString(name).Y * 2) - 22, (int)(Game1.SpriteFont.MeasureString(name).X * 2), 4), null, Color.Black * alpha, 0.0f, Vector2.Zero, SpriteEffects.None, 0.1f);

                if (!Game1.Paused && processRunning)
                {

                    Game1.Paused = true;
                }
            }
        }

        public void OpenThumbnail(IGameScreenManager screenManager)
        {

            if(info != null)
            {

                screenManager.PushScreen(new ThumbnailScreen(screenManager, info, exePath, this));
            }
        }

        public void RunProgram()
        {

            if(!processRunning)
            {

                Process.Start(Directory.GetCurrentDirectory() + @"\" + exePath);
            }
        }

        bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
