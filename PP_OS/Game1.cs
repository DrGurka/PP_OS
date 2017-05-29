﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;

namespace PP_OS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleSystem particleSystem;
        Input input;

        static string[] settings;

        static bool paused;
        bool rested;

        static Texture2D buttons;

        static SpriteFont spriteFont;
        static Texture2D rect;

        IGameScreenManager screenManager;

        static Vector2 screenSize;

        static int currentThumbnail;
        static int currentPlatform;
        static int thumbnailCount;

        static float delta;

        public static int CurrentThumbnail
        {
            get
            {
                return currentThumbnail;
            }

            set
            {
                currentThumbnail = value;
            }
        }

        public static Vector2 ScreenSize
        {
            get
            {
                return screenSize;
            }
        }

        public static SpriteFont SpriteFont
        {
            get
            {
                return spriteFont;
            }

            set
            {
                spriteFont = value;
            }
        }

        public static Texture2D Rect
        {
            get
            {
                return rect;
            }

            set
            {
                rect = value;
            }
        }

        public static int ThumbnailCount
        {
            get
            {
                return thumbnailCount;
            }

            set
            {
                thumbnailCount = value;
            }
        }

        public static Texture2D Buttons
        {
            get
            {
                return buttons;
            }

            set
            {
                buttons = value;
            }
        }

        public Texture2D Buttons1
        {
            get
            {
                return buttons;
            }

            set
            {
                buttons = value;
            }
        }

        public static bool Paused
        {
            get
            {
                return paused;
            }

            set
            {
                paused = value;
            }
        }

        public static float Delta
        {
            get
            {
                return delta * 60;
            }
        }

        public static int CurrentPlatform
        {
            get
            {
                return currentPlatform;
            }

            set
            {
                currentPlatform = value;
            }
        }

        public static string[] Settings
        {
            get
            {
                return settings;
            }

            set
            {
                settings = value;
            }
        }

        public Game1()
        {

            int width = (int.TryParse(ConfigurationManager.AppSettings["ScreenWidth"], out width) ? width : GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            int height = (int.TryParse(ConfigurationManager.AppSettings["ScreenHeight"], out height) ? height : GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            screenSize = new Vector2(width, height);

            LoadConfig();

            graphics = new GraphicsDeviceManager(this)
            {

                PreferredBackBufferWidth = (int)screenSize.X,
                PreferredBackBufferHeight = (int)screenSize.Y,
                SynchronizeWithVerticalRetrace = false
            };

            IsFixedTimeStep = false;

            Window.IsBorderless = true;
            Content.RootDirectory = "Content";
        }

        void LoadConfig()
        {

            settings = new string[3];

            settings[0] = ConfigurationManager.AppSettings["PortablePath"];
            settings[1] = ConfigurationManager.AppSettings["GamePath"];
            settings[2] = ConfigurationManager.AppSettings["Theme"];
        }

        void Reset()
        {

            LoadConfig();

            int width = (int.TryParse(ConfigurationManager.AppSettings["ScreenWidth"], out width) ? width : GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            int height = (int.TryParse(ConfigurationManager.AppSettings["ScreenHeight"], out height) ? height : GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            screenSize = new Vector2(width, height);

            graphics.PreferredBackBufferWidth = (int)screenSize.X;
            graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            particleSystem = new ParticleSystem();

            currentPlatform = 0;
            currentThumbnail = 0;

            screenManager = new GameScreenManager(spriteBatch, Content);
            screenManager.OnGameExit += Exit;

            screenManager.ChangeScreen(new MainScreen(screenManager, GraphicsDevice));
        }

        protected override void Initialize()
        {

            particleSystem = new ParticleSystem();
            input = new Input();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteFont = Content.Load<SpriteFont>(@"font");

            buttons = Content.Load<Texture2D>(@"buttons");

            rect = new Texture2D(graphics.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            for (int i = 0; i < data.Length; ++i)
            {

                data[i] = Color.White;
                rect.SetData(data);
            }

            screenManager = new GameScreenManager(spriteBatch, Content);
            screenManager.OnGameExit += Exit;

            screenManager.ChangeScreen(new MainScreen(screenManager, GraphicsDevice));
        }

        protected override void UnloadContent()
        {

            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {

            if (!paused)
            {

                delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                screenManager.ChangeBetweenScreens();

                input.HandleInput();
                screenManager.HandleInput(gameTime);

                particleSystem.Emitter(new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), 0, 360, 0.4f, 0.1f, 1, (float)gameTime.TotalGameTime.TotalMilliseconds + 2500, 2500, Color.FromNonPremultiplied(128, 255, 255, 255), new Vector4(50, -128, 0, 0), (float)gameTime.TotalGameTime.TotalMilliseconds, 4, 4);
                particleSystem.Update(gameTime);

                base.Update(gameTime);
            }

            if(Input.Button3IsPressed && Input.Button4IsPressed && !rested)
            {

                Reset();
                rested = true;
            }

            if(Input.Button3Released || Input.Button4Released)
            {

                rested = false;
            }

            screenManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (!paused)
            {

                GraphicsDevice.Clear(Color.White);
                spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
                particleSystem.Draw(spriteBatch);
                screenManager.Draw(spriteBatch);
                spriteBatch.End();

                base.Draw(gameTime);
            }

        }
    }
}
