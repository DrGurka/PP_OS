using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PP_OS
{
    interface IGameScreenManager : IDisposable
    {

        void ChangeScreen(IGameScreen screen);
        void PushScreen(IGameScreen screen);
        void PauseCurrentScreen();
        void PopScreen();
        IGameScreen GetCurrentScreen();

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void HandleInput(GameTime gameTime);

        void ChangeBetweenScreens();

        void Exit();

        event Action OnGameExit;
    }

    interface IGameScreen : IDisposable
    {

        bool IsPaused { get; }

        void Pause();

        void Resume();

        void Initialize(ContentManager contentManager);

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void HandleInput(GameTime gameTime);

        void ChangeBetweenScreens();
    }
}
