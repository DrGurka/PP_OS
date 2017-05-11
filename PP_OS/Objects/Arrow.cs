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
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 4, position.Y + offsetY), null, Color.Black, 0.0f, Vector2.Zero, new Vector2(8, 10), SpriteEffects.None, 0.9f);

            spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 2, position.Y - (offsetY * 2f) - (up ? 10 : -16)), null, Color.Black, 0.0f, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 0.9f);
        }
    }
}
