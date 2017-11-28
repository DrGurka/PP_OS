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

        Vector2 boxPosition;
        Vector2 boxSize;
        Rectangle boxBounds;

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

            MainScreen.SignInvert = true;
        }

        public void Initialize(ContentManager contentManager)
        {

            button = new Button(Button.ButtonTexture.ButtonB, new Vector2(Game1.ScreenSize.X - 60, Game1.ScreenSize.Y - 66), 1.0f, "Back", true, 400, 3, MainScreen.Button.CurrentFrame, MainScreen.Button.TimeSinceLastFrame);
            isActive = true;
            button.Alpha = 1;

            boxPosition = new Vector2((int)(Game1.ScreenSize.X / 2f), (int)(Game1.ScreenSize.Y / 2f));
            boxBounds = new Rectangle(0, 0, (int)Game1.ScreenSize.X, (int)Game1.ScreenSize.Y);
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

            if(boxSize.X < boxBounds.Width)
            {

                float x = (boxBounds.X - boxPosition.X) * 0.1f;
                float y = (boxBounds.Y - boxPosition.Y) * 0.1f;
                float width = (boxBounds.Width - boxSize.X) * 0.1f;
                float height = (boxBounds.Height - boxSize.Y) * 0.1f;

                boxSize += new Vector2(width, height);
                boxPosition += new Vector2(x, y);
            }
            else
            {

                boxPosition = new Vector2(boxBounds.X, boxBounds.Y);
                boxSize = new Vector2(boxBounds.Width, boxBounds.Height);
            }

            if (!Game1.Paused)
            {

                button.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            button.Draw(spriteBatch);
            spriteBatch.Draw(Game1.Rect, boxPosition, null, Color.Black * 0.75f, 0.0f, Vector2.Zero, boxSize, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Game1.SpriteFont, WrapText(Game1.SpriteFont, infoText, Game1.ScreenSize.X), new Vector2(16, 16), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
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

            if (Input.Button4IsPressed && Input.Button1IsPressed && !Input.Button3IsPressed)
            {

                isActive = false;
                currentThumbnail.DisplayingInfo = false;
                MainScreen.SignInvert = false;
                screenManager.PopScreen();
                MainScreen.thumbnailOpen = false;
                Input.DPadLeftIsPressed = false;
                Input.DPadRightIsPressed = false;
                Input.Button1IsPressed = false;
                Input.Button1Pressed = false;
                Input.DPadRightPressed = false;
                Input.DPadLeftPressed = false;
            }

            if (Input.Button1Released && !Input.Button4IsPressed && !Input.Button3IsPressed)
            {

                isActive = false;
                currentThumbnail.RunProgram();
                screenManager.PopScreen();
                MainScreen.thumbnailOpen = false;
                Input.DPadLeftIsPressed = false;
                Input.DPadRightIsPressed = false;
                Input.Button1IsPressed = false;
                Input.Button1Pressed = false;
                Input.DPadRightPressed = false;
                Input.DPadLeftPressed = false;
            }
        }

        public void Dispose()
        {


        }
    }
}
