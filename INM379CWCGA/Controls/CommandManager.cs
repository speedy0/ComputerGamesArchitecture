using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace INM379CWCGA.Controls
{
    public delegate void Action(ButtonStatess butState, Vector2 amount);
    class CommandManager
    {
        private InputListeners Input;

        private Dictionary<Keys, Action> m_KeyBindings = new Dictionary<Keys, Action>();
        private Dictionary<MouseButton, Action> m_MouseButtonBindings = new Dictionary<MouseButton, Action>();

        public CommandManager()
        {
            Input = new InputListeners();

            // Register events with the input listener
            Input.KeyDown += this.KeyDown;
            Input.KeyPressed += this.KeyPressed;
            Input.KeyUp += this.KeyUp;
            Input.MouseButtonDown += this.MouseButtonDown;
        }

        public void Update()
        {
            // Update polling input listener, everything else is handled by events
            Input.Update();
        }

        public void KeyDown(object sender, KeyboardEvents a)
        {
            Action action = m_KeyBindings[a.Keys];

            if (action != null)
            {
                action(ButtonStatess.DOWN, new Vector2(1.0f));
            }
        }

        public void KeyUp(object sender, KeyboardEvents a)
        {
            Action action = m_KeyBindings[a.Keys];

            if (action != null)
            {
                action(ButtonStatess.UP, new Vector2(1.0f));
            }
        }

        public void KeyPressed(object sender, KeyboardEvents a)
        {
            Action action = m_KeyBindings[a.Keys];

            if (action != null)
            {
                action(ButtonStatess.PRESSED, new Vector2(1.0f));
            }
        }

        public void MouseButtonDown(object sender, MouseEvents a)
        {
            Action action = m_MouseButtonBindings[a.Button];

            if (action != null)
            {
                action(ButtonStatess.DOWN, new Vector2(a.CurrState.X, a.CurrState.Y));
            }
        }

        public void AddKeyboardBinding(Keys key, Action action)
        {
            // Add key to listen for when polling
            Input.AddKButton(key);

            // Add the binding to the command map
            m_KeyBindings.Add(key, action);
        }

        public void AddMouseBinding(MouseButton button, Action action)
        {
            // Add key to listen for when polling
            Input.AddClick(button);

            // Add the binding to the command map
            m_MouseButtonBindings.Add(button, action);
        }
    }
}
