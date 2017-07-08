﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

            ButtonA = 0,
            ButtonB = 19,
            ButtonX = 38,
            ButtonY = 57
        };

        ButtonTexture buttonTexture;

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

        public Button(ButtonTexture buttonTexture, Vector2 position, float layer, string text, bool leftSide, int millisecondsPerFrame, int size, int currentFrame, int timeSinceLastFrame)
        {

            this.buttonTexture = buttonTexture;
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
            int height = Game1.Buttons.Height / 4;
            int row = (int)((float)currentFrame / 2);
            int column = currentFrame % 2;

            return new Rectangle(width * column, (height * row) + (int)buttonTexture, width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Game1.Buttons, position, sourceRectangle, Color.White * alpha, 0.0f, Vector2.Zero, size, SpriteEffects.None, layer);

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

            spriteBatch.DrawString(Game1.SpriteFont, text, position + new Vector2((20 * size) * (leftSide ? 0 : 1) - ((3 * size) * (leftSide ? 1 : 0)), 10 * size), (ThumbnailScreen.IsActive && buttonTexture != ButtonTexture.ButtonY ? colorInverted * alpha : colorNormal * alpha), 0.0f, new Vector2(Game1.SpriteFont.MeasureString(text).X * (leftSide ? 1 : 0), Game1.SpriteFont.MeasureString(text).Y / 2f), 1f, SpriteEffects.None, layer);
        }
    }
}
