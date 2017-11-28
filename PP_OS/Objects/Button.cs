using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PP_OS
{
    class Button
    {

        Rectangle sourceRectangle;

        int currentFrame;
        int totalFrames = 2;
        int millisecondsPerFrame;
        int timeSinceLastFrame;

        float alpha;

        float layer;
        Vector2 position;
        string text;
        bool leftSide;

        int size;

        public enum ButtonTexture
        {

            ButtonLeft,
            ButtonMiddle,
            ButtonRight
        };

        List<ButtonTexture> buttonList;

        public int Width
        {

            get
            {

                return 17 * size;
            }
        }

        public int TimeSinceLastFrame
        {
            get
            {
                return timeSinceLastFrame;
            }

            set
            {
                timeSinceLastFrame = value;
            }
        }

        public int CurrentFrame
        {
            get
            {
                return currentFrame;
            }

            set
            {
                currentFrame = value;
            }
        }

        public float Alpha
        {
            get
            {
                return alpha;
            }

            set
            {
                alpha = value;
            }
        }

        public Button(List<ButtonTexture> buttonList, Vector2 position, float layer, string text, bool leftSide, int millisecondsPerFrame, int size, int currentFrame, int timeSinceLastFrame)
        {

            this.buttonList = buttonList;
            this.layer = layer;
            this.position = position;
            this.text = text;
            this.leftSide = leftSide;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.size = size;
            this.currentFrame = currentFrame;
            this.timeSinceLastFrame = timeSinceLastFrame;
        }

        public void Update(GameTime gameTime)
        {

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame && millisecondsPerFrame != 0)
            {

                timeSinceLastFrame -= millisecondsPerFrame;

                currentFrame++;
                timeSinceLastFrame = 0;

                if (currentFrame == totalFrames)
                {

                    currentFrame = 0;
                }
            }

            sourceRectangle = AnimationRectangle();
        }

        public Rectangle AnimationRectangle()
        {

            int width = Game1.Buttons.Width / 2;
            int height = Game1.Buttons.Height / 2;
            int row = (int)((float)currentFrame / 2);
            int column = currentFrame % 2;

            return new Rectangle(width * column, (height * row), width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            Rectangle newSource = new Rectangle(sourceRectangle.X, sourceRectangle.Y + (Game1.Buttons.Height / 2), sourceRectangle.Width, sourceRectangle.Height);

            if (buttonList.Contains(ButtonTexture.ButtonLeft))
            {

                spriteBatch.Draw(Game1.Buttons, position - new Vector2(Game1.Buttons.Width + (6 * size), 0), newSource, Color.White * alpha, 0.0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }
            else
            {

                spriteBatch.Draw(Game1.Buttons, position - new Vector2(Game1.Buttons.Width + (6 * size), 0), new Rectangle(0, 0, Game1.Buttons.Width / 2, Game1.Buttons.Height / 2), Color.White * alpha, 0.0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }

            if(buttonList.Contains(ButtonTexture.ButtonMiddle))
            {

                spriteBatch.Draw(Game1.Buttons, position, newSource, Color.White * alpha, 0.0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }
            else
            {

                spriteBatch.Draw(Game1.Buttons, position, new Rectangle(0, 0, Game1.Buttons.Width / 2, Game1.Buttons.Height / 2), Color.White * alpha, 0.0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }

            if (buttonList.Contains(ButtonTexture.ButtonRight))
            {

                spriteBatch.Draw(Game1.Buttons, position + new Vector2(Game1.Buttons.Width + (6 * size), 0), newSource, Color.White * alpha, 0.0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }
            else
            {

                spriteBatch.Draw(Game1.Buttons, position + new Vector2(Game1.Buttons.Width + (6 * size), 0), new Rectangle(0, 0, Game1.Buttons.Width / 2, Game1.Buttons.Height / 2), Color.White * alpha, 0.0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }

            Color colorNormal = Color.Black;
            Color colorInverted = Color.Black;

            switch (Game1.Theme)
            {

                case "Winter":

                    colorNormal = Color.White;
                    colorInverted = Color.White;
                    break;
                case "Valentines":

                    colorNormal = Color.White;
                    colorInverted = Color.White;
                    break;
                case "Halloween":

                    colorNormal = Color.White;
                    colorInverted = Color.White;
                    break;
                case "Friday the 13th":

                    colorNormal = Color.White;
                    colorInverted = Color.White;
                    break;
                case "Summer":

                    colorNormal = new Color(172, 240, 242);
                    colorInverted = new Color(172, 240, 242);
                    break;
                case "Spring":

                    colorNormal = Color.White;
                    colorInverted = Color.White;
                    break;
                case "Autumn":

                    colorNormal = new Color(239, 236, 202);
                    colorInverted = new Color(239, 236, 202);
                    break;
                default:

                    colorNormal = Color.White;
                    colorInverted = Color.Black;
                    break;
            }

            spriteBatch.DrawString(Game1.SpriteFont, text, position - new Vector2(-9 * size, 4 * size), (ThumbnailScreen.IsActive ? colorInverted * alpha : colorNormal * alpha), 0.0f, Game1.SpriteFont.MeasureString(text) / 2f, 1f, SpriteEffects.None, layer);
        }
    }
}
