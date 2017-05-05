using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PP_OS
{
    class ParticleSystem
    {
        static List<Particle> particles;

        float cPart;
        int partNew;

        public ParticleSystem()
        {

            particles = new List<Particle>();
        }

        public void Update(GameTime gameTime)
        {

            for (int i = particles.Count - 1; i >= 0; i--)
            {

                particles[i].Update(gameTime);
                if (!particles[i].IsAlive)
                {

                    particles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Particle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }

        public void Emitter(Rectangle region, float direction, int randomDirection, float particlesPerUpdate, float speed, int randomSpeed, float deathTimer, int randomTime, Color color, Vector4 randomColor, float startTime, int size, int randomSize)
        {

            Random rand = new Random(DateTime.Now.Ticks.GetHashCode());
            randomSpeed *= 10;
            cPart += particlesPerUpdate;
            partNew = (int)Math.Round(cPart);
            cPart -= partNew;

            Vector2 position = new Vector2(rand.Next(region.X, region.Width), rand.Next(region.Y, region.Height));

            for (int i = 0; i < partNew; i++)
            {

                float dir = direction + rand.Next(-randomDirection, randomDirection);

                int tmpSize = size + rand.Next(randomSize);

                float spd = Math.Max(0.1f, speed + (rand.Next(-randomSpeed, randomSpeed) * 0.1f));

                float time = deathTimer + rand.Next(randomTime);

                Color tmpColor = Color.Black;
                if (randomColor.X > 0)
                {

                    tmpColor.R = (byte)(color.R + rand.Next(0, (int)randomColor.X));
                }
                else
                {

                    tmpColor.R = (byte)(color.R + rand.Next((int)randomColor.X, 0));
                }

                if (randomColor.Y > 0)
                {

                    tmpColor.G = (byte)(color.G + rand.Next(0, (int)randomColor.Y));
                }
                else
                {

                    tmpColor.G = (byte)(color.G + rand.Next((int)randomColor.Y, 0));
                }

                if (randomColor.Z > 0)
                {

                    tmpColor.B = (byte)(color.B + rand.Next(0, (int)randomColor.Z));
                }
                else
                {

                    tmpColor.B = (byte)(color.B + rand.Next((int)randomColor.Z, 0));
                }

                if (randomColor.W > 0)
                {

                    tmpColor.A = (byte)(color.A + rand.Next(0, (int)randomColor.W));
                }
                else
                {

                    tmpColor.A = (byte)(color.A + rand.Next((int)randomColor.W, 0));
                }

                Vector2 velocity = new Vector2(spd * (float)Math.Cos(MathHelper.ToRadians(dir)), spd * (float)Math.Sin(MathHelper.ToRadians(dir)));
                particles.Add(new Particle(position, velocity, 0.0f, 0.0f, 1f, time, 0.0f, tmpColor, startTime, tmpSize));
            }
        }
    }
}
