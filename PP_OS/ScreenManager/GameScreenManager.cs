using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PP_OS
{
    class GameScreenManager : IGameScreenManager
    {

        private readonly SpriteBatch spriteBatch;
        private readonly ContentManager contentManager;
        private Action onGameExit;

        private readonly List<IGameScreen> gameScreen = new List<IGameScreen>();

        public GameScreenManager(SpriteBatch spriteBatch, ContentManager contentManager)
        {

            this.spriteBatch = spriteBatch;
            this.contentManager = contentManager;
        }

        public void ChangeBetweenScreens()
        {

            if (!IsScreenListEmpty)
            {

                var screen = GetCurrentScreen();

                if (!screen.IsPaused)
                {

                    screen.ChangeBetweenScreens();
                }
            }
        }

        public void PauseCurrentScreen()
        {

            var screen = GetCurrentScreen();

            screen.Pause();
        }

        public void ChangeScreen(IGameScreen screen)
        {

            RemoveAllScreens();

            gameScreen.Add(screen);

            screen.Initialize(contentManager);
        }

        public void PushScreen(IGameScreen screen)
        {

            if (!IsScreenListEmpty)
            {

                var curScreen = GetCurrentScreen();

                curScreen.Pause();
            }

            gameScreen.Add(screen);

            screen.Initialize(contentManager);
        }

        private bool IsScreenListEmpty
        {

            get
            {

                return gameScreen.Count <= 0;
            }
        }

        public IGameScreen GetCurrentScreen()
        {

            return gameScreen[gameScreen.Count - 1];
        }

        private void RemoveAllScreens()
        {

            while (!IsScreenListEmpty)
            {

                RemoveCurrentScreen();
            }
        }

        private void RemoveCurrentScreen()
        {

            var screen = GetCurrentScreen();

            screen.Dispose();

            gameScreen.Remove(screen);
        }

        public void PopScreen()
        {

            if (!IsScreenListEmpty)
            {

                RemoveCurrentScreen();

                var screen = GetCurrentScreen();

                screen.Resume();
            }
        }

        public void HandleInput(GameTime gameTime)
        {

            if (!IsScreenListEmpty)
            {

                var screen = GetCurrentScreen();

                if (!screen.IsPaused)
                {

                    screen.HandleInput(gameTime);
                }
            }
        }

        public void Update(GameTime gameTime)
        {

            if (!IsScreenListEmpty)
            {

                foreach (IGameScreen screen in gameScreen)
                {

                    screen.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (!IsScreenListEmpty)
            {

                foreach (IGameScreen screen in gameScreen)
                {

                    screen.Draw(spriteBatch);
                }
            }
        }

        public void Exit()
        {

            if (onGameExit != null)
            {

                onGameExit();
            }
        }

        public event Action OnGameExit
        {

            add { onGameExit += value; }
            remove { onGameExit -= value; }
        }

        public void Dispose()
        {

            RemoveAllScreens();
        }
    }
}
