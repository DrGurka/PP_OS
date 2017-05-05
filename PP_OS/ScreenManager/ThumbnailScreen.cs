using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PP_OS
{
    class ThumbnailScreen : IGameScreen
    {

        private readonly IGameScreenManager screenManager;
        private bool exitGame;

        string infoText;
        string exePath;

        Button button;
        Thumbnail currentThumbnail;

        static bool isActive;

        public bool IsPaused { get; private set; }

        public static bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }

        public ThumbnailScreen(IGameScreenManager screenManager, string infoText, string exePath, Thumbnail currentThumbnail)
        {

            this.screenManager = screenManager;
            this.infoText = infoText;
            this.exePath = exePath;
            this.currentThumbnail = currentThumbnail;
        }

        public void Initialize(ContentManager contentManager)
        {

            button = new Button(Button.ButtonTexture.ButtonB, new Vector2(Game1.ScreenSize.X - 60, Game1.ScreenSize.Y - 66), 1.0f, "Back", true, 400, 3, MainScreen.Button.CurrentFrame, MainScreen.Button.TimeSinceLastFrame);
            isActive = true;
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

            if (!Game1.Paused)
            {

                button.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            button.Draw(spriteBatch);
            spriteBatch.Draw(Game1.Rect, new Rectangle(0, 0, (int)Game1.ScreenSize.X, (int)Game1.ScreenSize.Y), null, Color.Black * 0.75f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Game1.SpriteFont, WrapText(Game1.SpriteFont, infoText, Game1.ScreenSize.X), new Vector2(16, 16), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
        }

        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

        public void HandleInput(GameTime gameTime)
        {

            if (Input.Button4Pressed)
            {

                isActive = false;
                screenManager.PopScreen();
            }

            if (Input.Button1Pressed)
            {

                isActive = false;
                currentThumbnail.RunProgram();
                screenManager.PopScreen();
            }
        }

        public void Dispose()
        {


        }
    }
}
