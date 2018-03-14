using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;
using System;
using Microsoft.Xna.Framework.Media;

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
        float pressedTime;
        static bool reset;

        static Texture2D buttons;

        static SpriteFont spriteFont;
        static Texture2D rect;

        IGameScreenManager screenManager;

        static Vector2 screenSize;

        static int currentThumbnail;
        static int currentPlatform;
        static int thumbnailCount;

        static string theme;

        static float delta;

        int lastDay;

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

        public static string Theme
        {
            get
            {
                return theme;
            }

            set
            {
                theme = value;
            }
        }

        public static bool Reset1
        {
            get
            {
                return reset;
            }

            set
            {
                reset = value;
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

            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));

            Window.IsBorderless = true;
            Content.RootDirectory = "Content";
        }

        void LoadConfig()
        {

            ConfigurationManager.RefreshSection("appSettings");
            settings = new string[2];

            settings[0] = ConfigurationManager.AppSettings["PortablePath"];
            settings[1] = ConfigurationManager.AppSettings["GamePath"];
        }

        void Reset()
        {

            MediaPlayer.Stop();

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

            if(lastDay != DateTime.Now.Day)
            {

                switch (DateTime.Now.Month)
                {

                    case 1:

                        theme = "Winter";
                        break;
                    case 2:

                        if (DateTime.Now.Day != 14)
                        {

                            theme = "Winter";
                        }
                        else
                        {

                            theme = "Valentines";
                        }
                        break;
                    case 3:

                        theme = "Spring";
                        break;
                    case 4:

                        theme = "Spring";
                        break;
                    case 5:

                        theme = "Spring";
                        break;
                    case 6:

                        theme = "Summer";
                        break;
                    case 7:

                        theme = "Summer";
                        break;
                    case 8:

                        theme = "Summer";
                        break;
                    case 9:

                        theme = "Autumn";
                        break;
                    case 10:

                        if (DateTime.Now.Day != 31)
                        {

                            theme = "Autumn";
                        }
                        else
                        {

                            theme = "Halloween";
                        }
                        break;
                    case 11:

                        theme = "Autumn";
                        break;
                    case 12:

                        theme = "Winter";
                        break;

                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && DateTime.Now.Day == 13)
                {

                    theme = "Friday the 13th";
                }

                lastDay = DateTime.Now.Day;
            }

            input.HandleInput();

            if (!paused)
            {

                delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                screenManager.ChangeBetweenScreens();

                screenManager.HandleInput(gameTime);

                Color color = Color.White;
                Vector4 colorAttractor = new Vector4(0, 0, 0, 0);

                switch(theme)
                {


                    case "Winter":

                        color = new Color(255, 255, 255, 255);
                        colorAttractor = new Vector4(0, 0, 0, -128);
                        break;
                    case "Valentines":

                        color = new Color(188, 13, 53);
                        colorAttractor = new Vector4(-26, 0, -23, 0);
                        break;
                    case "Halloween":

                        color = new Color(239, 140, 17);
                        colorAttractor = new Vector4(-16, -62, 40, 0);
                        break;
                    case "Friday the 13th":

                        color = new Color(239, 22, 58, 255);
                        colorAttractor = new Vector4(-159, 17, 177, -128);
                        break;
                    case "Summer":

                        color = new Color(243, 255, 226, 255);
                        colorAttractor = new Vector4(0, 0, 0, -128);
                        break;
                    case "Spring":

                        color = new Color(179, 246, 255);
                        colorAttractor = new Vector4(76, 9, 0, -90);
                        break;
                    case "Autumn":

                        color = new Color(4, 99, 128);
                        colorAttractor = new Vector4(0, 0, 0, 0);
                        break;
                    default:

                        color = new Color(128, 255, 255, 255);
                        colorAttractor = new Vector4(50, -128, 0, 0);
                        break;
                }

                particleSystem.Emitter(new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), 0, 360, 0.4f, 0.1f, 1, (float)gameTime.TotalGameTime.TotalMilliseconds + 2500, 2500, color, colorAttractor, (float)gameTime.TotalGameTime.TotalMilliseconds, 4, 4);
                particleSystem.Update(gameTime);
            }

            base.Update(gameTime);

            if (Input.Button3IsPressed && Input.Button4IsPressed && !reset)
            {

                pressedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(pressedTime > 2000)
                {

                    Reset();
                    pressedTime = 0;
                    reset = true;
                }
                
            }

            if(!Input.Button3IsPressed && !Input.Button4IsPressed)
            {

                reset = false;
            }

            screenManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            switch (theme)
            {

                case "Winter":

                    GraphicsDevice.Clear(new Color(19, 19, 25));
                    break;
                case "Valentines":

                    GraphicsDevice.Clear(new Color(246, 177, 195));
                    break;
                case "Halloween":

                    GraphicsDevice.Clear(new Color(19, 11, 41));
                    break;
                case "Friday the 13th":

                    GraphicsDevice.Clear(new Color(9, 9, 11));
                    break;
                case "Summer":

                    GraphicsDevice.Clear(new Color(34, 83, 120));
                    break;
                case "Spring":

                    GraphicsDevice.Clear(new Color(158, 197, 255));
                    break;
                case "Autumn":

                    GraphicsDevice.Clear(new Color(0, 47, 47));
                    break;
                default:

                    GraphicsDevice.Clear(Color.White);
                    break;
            }
                
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp);
            screenManager.Draw(spriteBatch);
            particleSystem.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
