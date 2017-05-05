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

        public Input()
        {

            
        }

        public void HandleInput()
        {

            GamePadState gp = GamePad.GetState(PlayerIndex.One);

            KeyboardState ks = Keyboard.GetState();

            leftThumbstick = gp.ThumbSticks.Left;

            if (!button1IsPressed)
            {

                button1Pressed = gp.IsButtonDown(Buttons.A) || ks.IsKeyDown(Keys.Enter);

            }
            else
            {

                button1Pressed = false;
            }

            button1IsPressed = gp.IsButtonDown(Buttons.A) || ks.IsKeyDown(Keys.Enter);
            button1Released = gp.IsButtonUp(Buttons.A) || ks.IsKeyUp(Keys.Enter);

            if (!button3IsPressed)
            {

                button3Pressed = gp.IsButtonDown(Buttons.Y) || ks.IsKeyDown(Keys.Y);

            }
            else
            {

                button3Pressed = false;
            }

            button3IsPressed = gp.IsButtonDown(Buttons.Y) || ks.IsKeyDown(Keys.Y);
            button3Released = gp.IsButtonUp(Buttons.Y) || ks.IsKeyUp(Keys.Y);

            if (!button4IsPressed)
            {

                button4Pressed = gp.IsButtonDown(Buttons.B) || ks.IsKeyDown(Keys.Back);

            }
            else
            {

                button4Pressed = false;
            }

            button4IsPressed = gp.IsButtonDown(Buttons.B) || ks.IsKeyDown(Keys.Back);
            button4Released = gp.IsButtonUp(Buttons.B) || ks.IsKeyUp(Keys.Back);

            if (!dPadLeftIsPressed)
            {

                dPadLeftPressed = gp.IsButtonDown(Buttons.DPadLeft) || ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A);

            }
            else
            {

                dPadLeftPressed = false;
            }

            dPadLeftIsPressed = gp.IsButtonDown(Buttons.DPadLeft) || ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A);
            dPadLeftReleased = gp.IsButtonUp(Buttons.DPadLeft) || ks.IsKeyUp(Keys.Left) || ks.IsKeyUp(Keys.A);

            if (!dPadRightIsPressed)
            {

                dPadRightPressed = gp.IsButtonDown(Buttons.DPadRight) || ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D);

            }
            else
            {

                dPadRightPressed = false;
            }

            dPadRightIsPressed = gp.IsButtonDown(Buttons.DPadRight) || ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D);
            dPadRightReleased = gp.IsButtonUp(Buttons.DPadRight) || ks.IsKeyUp(Keys.Right) || ks.IsKeyUp(Keys.D);
        }
    }
}
