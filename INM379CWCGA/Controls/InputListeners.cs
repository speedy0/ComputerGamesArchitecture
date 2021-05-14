using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using static INM379CWCGA.Controls.MouseEvents;

namespace INM379CWCGA.Controls
{
    class InputListeners
    {
        #region Properties
        //List of Keys
        public HashSet<Keys> KeyboardButtons;
        public HashSet<MouseButton> MouseButtons;

        //Keyboard States
        private KeyboardState PrevKS { get; set; }
        private KeyboardState CurrKS { get; set; }
        
        //Mouse States
        private MouseState PrevMS { get; set; }
        private MouseState CurrMS { get; set; }

        //Event Handlers
        public event EventHandler<KeyboardEvents> KeyDown = delegate { };
        public event EventHandler<KeyboardEvents> KeyPressed = delegate { };
        public event EventHandler<KeyboardEvents> KeyUp = delegate { };
        public event EventHandler<MouseEvents> MouseButtonDown = delegate { };
        #endregion

        #region Constructor
        public InputListeners()
        {
            //Keyboard
            CurrKS = Keyboard.GetState();
            PrevKS = CurrKS;

            KeyboardButtons = new HashSet<Keys>();

            //Mouse
            CurrMS = Mouse.GetState();
            PrevMS = CurrMS;

            MouseButtons = new HashSet<MouseButton>();
        }
        #endregion

        #region Events
        public void AddKButton(Keys key)
        {
            KeyboardButtons.Add(key);
        }

        public void AddClick(MouseButton button)
        {
            MouseButtons.Add(button);
        }

        private void KeyboardEvents()
        {
            foreach (Keys key in KeyboardButtons)
            {
                //Checks whether key is currently pressed down
                if (CurrKS.IsKeyDown(key))
                {
                    if (KeyDown != null)
                    {
                        KeyDown(this, new KeyboardEvents(key, CurrKS, PrevKS));
                    }
                }

                //Checks whether key was released
                if (PrevKS.IsKeyDown(key) && CurrKS.IsKeyUp(key))
                {
                    if (KeyUp!= null)
                    {
                        KeyUp(this, new KeyboardEvents(key, CurrKS, PrevKS));
                    }
                }

                //Checks whether key was pressed 
                if (PrevKS.IsKeyUp(key) && CurrKS.IsKeyDown(key))
                {
                    if (KeyPressed != null)
                    {
                        KeyPressed(this, new KeyboardEvents(key, CurrKS, PrevKS));
                    }
                }
            }
        }

        private void MouseEvents()
        {
            foreach (MouseButton mouse in MouseButtons)
            {
                if (CurrMS.LeftButton == ButtonState.Pressed)
                {
                    if (MouseButtonDown != null)
                        MouseButtonDown(this, new MouseEvents(mouse, CurrMS, PrevMS));
                }
            }
        }
        #endregion

        #region Update
        public void Update()
        {
            PrevKS = CurrKS;
            CurrKS = Keyboard.GetState();
            PrevMS = CurrMS;
            CurrMS = Mouse.GetState();

            KeyboardEvents();
            MouseEvents();
        }
        #endregion
    }
}
