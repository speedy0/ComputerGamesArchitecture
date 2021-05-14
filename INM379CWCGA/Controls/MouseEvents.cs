using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace INM379CWCGA.Controls
{
        public enum MouseButton
        {
            NONE = 0x00,
            LEFT = 0x01,
            RIGHT = 0x02,
            MIDDLE = 0x04,
            XBUTTON1 = 0x08,
            XBUTTON2 = 0x10,
        }

    class MouseEvents : EventArgs
    {
            public readonly MouseState CurrState;
            public readonly MouseState PrevState;
            public readonly MouseButton Button;

            public MouseEvents(MouseButton button, MouseState currState, MouseState prevState)
            {
                CurrState = currState;
                PrevState = prevState;
                Button = button;
            }
    }
}
