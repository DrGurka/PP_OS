using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PP_OS
{
    class Arrow
    {

        Vector2 position;

        float offsetY;
        int targetY;

        bool up;

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Arrow(Vector2 position, bool up)
        {

            this.position = position;
            this.up = up;

            targetY = up ? 8 : -8;
        }

        public void Activate()
        {

            offsetY = targetY;
        }

        public void Update(GameTime gameTime)
        {


            offsetY += (-offsetY * 0.1f) * Game1.Delta;

            if(MainScreen.CurrentThumbnail != null)
            {

                position.Y = (MainScreen.CurrentThumbnail.Position.Y) - ((up ? 1 : -1) * ((MainScreen.CurrentThumbnail.Height / 2f) + 42)) - ((up ? 1 : 0) * 10);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if(Game1.Settings[2] == "White")
            {

                spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 4, position.Y + offsetY), null, Color.Black, 0.0f, Vector2.Zero, new Vector2(8, 10), SpriteEffects.None, 0.9f);

                spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 2, position.Y - (offsetY * 2f) - (up ? 10 : -16)), null, Color.Black, 0.0f, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 0.9f);

            }
            else if(Game1.Settings[2] == "Dark")
            {

                spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 4, position.Y + offsetY), null, Color.White, 0.0f, Vector2.Zero, new Vector2(8, 10), SpriteEffects.None, 0.9f);

                spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 2, position.Y - (offsetY * 2f) - (up ? 10 : -16)), null, Color.White, 0.0f, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 0.9f);

            }
            else
            {

                spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 4, position.Y + offsetY), null, Color.Black, 0.0f, Vector2.Zero, new Vector2(8, 10), SpriteEffects.None, 0.9f);

                spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 2, position.Y - (offsetY * 2f) - (up ? 10 : -16)), null, Color.Black, 0.0f, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 0.9f);

            }
        }
    }
}
