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

            Color color = Color.Black;
            switch (Game1.Theme)
            {

                case "Winter":

                    color = Color.White;
                    break;
                case "Valentines":

                    color = new Color(162, 13, 30);
                    break;
                case "Halloween":

                    color = new Color(239, 140, 17);
                    break;
                case "Friday the 13th":

                    color = Color.White;
                    break;
                case "Summer":

                    color = new Color(172, 240, 242);
                    break;
                case "Spring":

                    color = new Color(255, 240, 165);
                    break;
                case "Autumn":

                    color = new Color(239, 236, 202);
                    break;
                default:

                    color = Color.Black;
                    break;
            }

            spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 4, position.Y + offsetY), null, color, 0.0f, Vector2.Zero, new Vector2(8, 10), SpriteEffects.None, 0.3f);
            spriteBatch.Draw(Game1.Rect, new Vector2(position.X - 2, position.Y - (offsetY * 2f) - (up ? 10 : -16)), null, color, 0.0f, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 0.3f);
        }
    }
}
