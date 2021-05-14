using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace INM379CWCGA
{

    class HighscoreState : StateManager
    {
        #region Properties
        //Background
        Texture2D texture;

        //Button for Quit
        Texture2D buttonTexture;
        SpriteFont ButtonFont;
        readonly Button QuitButton;
        #endregion

        #region Constructor
        public HighscoreState(Game1 g, ContentManager cm, GraphicsDevice gd) : base(g, cm, gd)
        {
            texture = content.Load<Texture2D>("Graphics\\Background\\HighscoresBG");
            buttonTexture = content.Load<Texture2D>("Graphics/Button/Button");
            ButtonFont = content.Load<SpriteFont>("FOnt/Font");

            QuitButton = new Button(ButtonFont, buttonTexture)
            {
                Position = new Vector2(315, 400),
                text = "Quit",
            };

            QuitButton.Press += Quit_Pressed;
        }
        #endregion

        #region Update
        public override void Update(GameTime dt)
        {
            QuitButton.Update(dt);
        }
        #endregion

        #region Event
        private void Quit_Pressed(object sender, EventArgs e)
        {
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

            spriteB.Draw(texture, new Rectangle(0, 0, 800, 600), Color.White);

            game.DrawHighscore(dt, spriteB);

            QuitButton.Draw(dt, spriteB);

            spriteB.End();
        }
        #endregion
    }
}
