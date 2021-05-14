using System;
using Microsoft.Xna.Framework.Input;

namespace INM379CWCGA.Controls
{
    class KeyboardEvents : EventArgs
    {
        #region Properties
        public readonly KeyboardState CurrKS;
        public readonly KeyboardState PrevKS;
        public readonly Keys Keys;
        #endregion

        #region Constructor
        public KeyboardEvents(Keys key, KeyboardState currKS, KeyboardState prevKS)
        {
            CurrKS = currKS;
            PrevKS = prevKS;
            Keys = key;
        }
        #endregion
    }
}
