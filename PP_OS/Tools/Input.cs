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

        KeyboardState ksLast;

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

                if (!ksLast.IsKeyDown(Keys.R))
                {

                    button1Pressed = ks.IsKeyDown(Keys.R);
                    button1Released = false;
                }
            }
            else
            {

                button1Released = ks.IsKeyUp(Keys.R);
                button1Pressed = false;
            }

            if (button1Pressed)
            {

                button1IsPressed = true;
            }
            else if (button1Released)
            {

                button1IsPressed = false;
            }

            if (!button3IsPressed)
            {

                button3Pressed = ks.IsKeyDown(Keys.Enter);
                button3Released = false;
            }
            else
            {

                button3Released = ks.IsKeyUp(Keys.Enter);
                button3Pressed = false;
            }

            button3IsPressed = ks.IsKeyDown(Keys.Enter);

            if (!button4IsPressed)
            {

                button4Pressed = ks.IsKeyDown(Keys.Space);
                button4Released = false;
            }
            else
            {

                button4Released = ks.IsKeyUp(Keys.Space);
                button4Pressed = false;
            }

            button4IsPressed = ks.IsKeyDown(Keys.Space);
            
            if (!dPadLeftIsPressed)
            {

                if (!ksLast.IsKeyDown(Keys.Left) && !ksLast.IsKeyDown(Keys.A) && !ksLast.IsKeyDown(Keys.Enter))
                {

                    dPadLeftPressed = ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Enter);
                    dPadLeftReleased = false;
                }
            }
            else
            {

                dPadLeftReleased = ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.A) && ks.IsKeyUp(Keys.Enter);
                dPadLeftPressed = false;
                
            }

            if (dPadLeftPressed)
            {

                dPadLeftIsPressed = true;
            }
            else if (dPadLeftReleased)
            {

                dPadLeftIsPressed = false;
            }

            if (!dPadRightIsPressed)
            {

                if (!ksLast.IsKeyDown(Keys.Right) && !ksLast.IsKeyDown(Keys.D) && !ksLast.IsKeyDown(Keys.Space))
                {

                    dPadRightPressed = ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Space);
                    dPadRightReleased = false;
                }
            }
            else
            {

                dPadRightReleased = ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.D) && ks.IsKeyUp(Keys.Space);
                dPadRightPressed = false;
            }

            if(dPadRightPressed)
            {

                dPadRightIsPressed = true;
            }
            else if (dPadRightReleased)
            {

                dPadRightIsPressed = false;
            }

            if (!dPadUpIsPressed)
            {

                dPadUpPressed = ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W);
                dPadUpReleased = false;
            }
            else
            {

                dPadUpReleased = ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.W);
                dPadUpPressed = false;
            }

            dPadUpIsPressed = ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W);

            if (!dPadDownIsPressed)
            {

                dPadDownPressed = ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S);
                dPadDownReleased = false;
            }
            else
            {

                dPadDownReleased = ks.IsKeyUp(Keys.Down) && ks.IsKeyUp(Keys.S);
                dPadDownPressed = false;
            }

            dPadDownIsPressed = ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S);

            ksLast = ks;
        }
    }
}
