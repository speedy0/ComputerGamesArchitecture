using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    #region States
    /// <summary>
    /// This class is an incomplete class to provide structure for other State classes. 
    /// </summary>
    #endregion
    public abstract class StateManager
    {
        #region Properties
        protected ContentManager content;
        protected GraphicsDevice graphics;

        protected Game1 game;
        #endregion

        #region Constructor
        public StateManager(Game1 g, ContentManager cm, GraphicsDevice gd)
        {
            game = g;

            content = cm;

            graphics = gd;
        }
        #endregion

        #region Update
        public abstract void Update(GameTime dt);
        #endregion

        #region Draw
        public abstract void Draw(GameTime dt, SpriteBatch spriteB);
        #endregion

        public abstract void Unload(GameTime dt);
    }
}
