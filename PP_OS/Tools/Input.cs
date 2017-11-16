using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PP_OS
{
    class Input
    {

        static bool button1Pressed;
        static bool button1IsPressed;
        static bool button1Released;

        static bool button3Pressed;
        static bool button3IsPressed;
        static bool button3Released;

        static bool button4Pressed;
        static bool button4IsPressed;
        static bool button4Released;

        static bool dPadLeftPressed;
        static bool dPadLeftIsPressed;
        static bool dPadLeftReleased;

        static bool dPadRightPressed;
        static bool dPadRightIsPressed;
        static bool dPadRightReleased;

        static bool dPadUpPressed;
        static bool dPadUpIsPressed;
        static bool dPadUpReleased;

        static bool dPadDownPressed;
        static bool dPadDownIsPressed;
        static bool dPadDownReleased;

        static Vector2 leftThumbstick;

        public static bool Button1Pressed
        {
            get
            {
                return button1Pressed;
            }

            set
            {
                button1Pressed = value;
            }
        }

        public static bool Button1IsPressed
        {
            get
            {
                return button1IsPressed;
            }

            set
            {
                button1IsPressed = value;
            }
        }

        public static bool Button1Released
        {
            get
            {
                return button1Released;
            }

            set
            {
                button1Released = value;
            }
        }

        public static bool Button4Pressed
        {
            get
            {
                return button4Pressed;
            }

            set
            {
                button4Pressed = value;
            }
        }

        public static bool Button4IsPressed
        {
            get
            {
                return button4IsPressed;
            }

            set
            {
                button4IsPressed = value;
            }
        }

        public static bool Button4Released
        {
            get
            {
                return button4Released;
            }

            set
            {
                button4Released = value;
            }
        }

        public static Vector2 LeftThumbstick
        {
            get
            {
                return leftThumbstick;
            }

            set
            {
                leftThumbstick = value;
            }
        }

        public static bool DPadLeftPressed
        {
            get
            {
                return dPadLeftPressed;
            }

            set
            {
                dPadLeftPressed = value;
            }
        }

        public static bool DPadLeftIsPressed
        {
            get
            {
                return dPadLeftIsPressed;
            }

            set
            {
                dPadLeftIsPressed = value;
            }
        }

        public static bool DPadLeftReleased
        {
            get
            {
                return dPadLeftReleased;
            }

            set
            {
                dPadLeftReleased = value;
            }
        }

        public static bool DPadRightPressed
        {
            get
            {
                return dPadRightPressed;
            }

            set
            {
                dPadRightPressed = value;
            }
        }

        public static bool DPadRightIsPressed
        {
            get
            {
                return dPadRightIsPressed;
            }

            set
            {
                dPadRightIsPressed = value;
            }
        }

        public static bool DPadRightReleased
        {
            get
            {
                return dPadRightReleased;
            }

            set
            {
                dPadRightReleased = value;
            }
        }

        public static bool Button3Pressed
        {
            get
            {
                return button3Pressed;
            }

            set
            {
                button3Pressed = value;
            }
        }

        public static bool Button3IsPressed
        {
            get
            {
                return button3IsPressed;
            }

            set
            {
                button3IsPressed = value;
            }
        }

        public static bool Button3Released
        {
            get
            {
                return button3Released;
            }

            set
            {
                button3Released = value;
            }
        }

        public static bool DPadUpPressed
        {
            get
            {
                return dPadUpPressed;
            }

            set
            {
                dPadUpPressed = value;
            }
        }

        public static bool DPadUpIsPressed
        {
            get
            {
                return dPadUpIsPressed;
            }

            set
            {
                dPadUpIsPressed = value;
            }
        }

        public static bool DPadUpReleased
        {
            get
            {
                return dPadUpReleased;
            }

            set
            {
                dPadUpReleased = value;
            }
        }

        public static bool DPadDownPressed
        {
            get
            {
                return dPadDownPressed;
            }

            set
            {
                dPadDownPressed = value;
            }
        }

        public static bool DPadDownIsPressed
        {
            get
            {
                return dPadDownIsPressed;
            }

            set
            {
                dPadDownIsPressed = value;
            }
        }

        public static bool DPadDownReleased
        {
            get
            {
                return dPadDownReleased;
            }

            set
            {
                dPadDownReleased = value;
            }
        }

        public Input()
        {

            
        }

        public void HandleInput()
        {

            KeyboardState ks = Keyboard.GetState();

            if (!button1IsPressed)
            {

                button1Pressed = ks.IsKeyDown(Keys.R);
            }
            else
            {

                button1Pressed = false;
            }

            button1IsPressed = ks.IsKeyDown(Keys.R);
            button1Released = ks.IsKeyUp(Keys.R);

            if (!button3IsPressed)
            {

                button3Pressed = ks.IsKeyDown(Keys.Enter);
            }
            else
            {

                button3Pressed = false;
            }

            button3IsPressed = ks.IsKeyDown(Keys.Enter);
            button3Released = ks.IsKeyUp(Keys.Enter);

            if (!button4IsPressed)
            {

                button4Pressed = ks.IsKeyDown(Keys.Space);
            }
            else
            {

                button4Pressed = false;
            }

            button4IsPressed = ks.IsKeyDown(Keys.Space);
            button4Released = ks.IsKeyUp(Keys.Space);

            if (!dPadLeftIsPressed)
            {

                dPadLeftPressed = ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A);
            }
            else
            {

                dPadLeftPressed = false;
            }

            dPadLeftIsPressed = ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A);
            dPadLeftReleased = ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.A);

            if (!dPadRightIsPressed)
            {

                dPadRightPressed = ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D);
            }
            else
            {

                dPadRightPressed = false;
            }

            dPadRightIsPressed = ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D);
            dPadRightReleased = ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.D);

            if (!dPadUpIsPressed)
            {

                dPadUpPressed = ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W);
            }
            else
            {

                dPadUpPressed = false;
            }

            dPadUpIsPressed = ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W);
            dPadUpReleased = ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.W);

            if (!dPadDownIsPressed)
            {

                dPadDownPressed = ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S);
            }
            else
            {

                dPadDownPressed = false;
            }

            dPadDownIsPressed = ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S);
            dPadDownReleased = ks.IsKeyUp(Keys.Down) && ks.IsKeyUp(Keys.S);
        }
    }
}
