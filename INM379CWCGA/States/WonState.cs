using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace INM379CWCGA
{
    class WonState : StateManager
    {
        Texture2D texture;

        #region Constructor
        public WonState(Game1 g, ContentManager cm, GraphicsDevice gd) : base(g, cm, gd)
        {
            texture = content.Load<Texture2D>("Graphics\\HUD\\WinLayer");
        }
        #endregion

        #region Update
        public override void Update(GameTime dt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.ChangeCurrentState(new MainMenu(game, content, graphics));
        }
        #endregion

        #region Unload
        public override void Unload(GameTime dt)
        {
            content.Unload();
        }
        #endregion

        #region Draw
        public override void Draw(GameTime dt, SpriteBatch spriteB)
        {
            spriteB.Begin();

            spriteB.Draw(texture, new Rectangle(0, 0, 800, 480), Color.White);

            spriteB.End();
        }
        #endregion
    }
}
