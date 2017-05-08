using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PP_OS
{
    class Particle
    {

        //inits
        Vector2 position;
        Vector2 velocity;
        float layer;
        float rotation;
        SpriteEffects spriteEffects;
        float alpha;
        float deathTimer;
        bool isAlive;
        float baseLayer;
        float acceleration;
        Color color;
        float startTime;
        int size;

        //accerssors
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

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public float Layer
        {
            get
            {
                return layer;
            }

            set
            {
                layer = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public SpriteEffects SpriteEffects
        {
            get
            {
                return spriteEffects;
            }

            set
            {
                spriteEffects = value;
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

        public float DeathTimer
        {
            get
            {
                return deathTimer;
            }

            set
            {
                deathTimer = value;
            }
        }

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }

            set
            {
                isAlive = value;
            }
        }

        public float Acceleration
        {
            get
            {
                return acceleration;
            }

            set
            {
                acceleration = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }


        //Construktor
        public Particle(Vector2 position, Vector2 velocity, float layer, float rotation, float alpha, float deathTimer, float acceleration, Color color, float startTime, int size)
        {
            this.position = position;
            this.velocity = velocity;
            this.layer = layer;
            this.rotation = rotation;
            this.alpha = alpha;
            this.deathTimer = deathTimer;
            this.acceleration = acceleration;
            this.color = color;
            this.size = size;
            this.startTime = startTime;

            isAlive = true;
            baseLayer = layer;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity * Game1.Delta;

            layer = baseLayer + (position.Y / 10000);

            var tmpTime = startTime + ((deathTimer - gameTime.TotalGameTime.TotalMilliseconds) / 2);

            if ((float)gameTime.TotalGameTime.TotalMilliseconds < tmpTime)
            {

                alpha = Math.Min((float)((gameTime.TotalGameTime.TotalMilliseconds - startTime) / (tmpTime - startTime)), 1);
            }
            else
            {

                
                alpha = 1 - Math.Min((float)((gameTime.TotalGameTime.TotalMilliseconds - startTime) / (deathTimer - startTime)), 1);
            }

            if (gameTime.TotalGameTime.TotalMilliseconds > deathTimer)
            {
                isAlive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.Rect, new Rectangle((int)position.X, (int)position.Y, size, size), null, color * alpha, rotation, Vector2.Zero, spriteEffects, layer);
        }
    }
}
