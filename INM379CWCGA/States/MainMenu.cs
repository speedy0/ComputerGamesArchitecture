using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    #region Main Menu State
    /// <summary>
    /// This state will deal with resources important for Main Menu state. 
    /// </summary>
    #endregion
    public class MainMenu : StateManager
    {
        #region Properties
        Texture2D Texture;
        SpriteFont Font;

        //Buttons for the Main Menu
        readonly Button NewGamebutton;
        readonly Button Highscorebutton;
        readonly Button Controlsbutton;
        readonly Button Quitbutton;
        
        private List<ComponentSkeleton> components;

        //Background
        Texture2D Backtext;
        #endregion

        #region Constructor
        public MainMenu(Game1 g, ContentManager cm, GraphicsDevice gd) : base(g, cm, gd)
        {
            Texture = content.Load<Texture2D>("Graphics/Button/Button");
            Font = content.Load<SpriteFont>("FOnt/Font");

            Backtext = content.Load<Texture2D>("Graphics\\Background\\MainMenuBG");

            NewGamebutton = new Button(Font, Texture)
            {
                Position = new Vector2(315, 200),
                text = "New Game",
            };

            Highscorebutton = new Button(Font, Texture)
            {
                Position = new Vector2(315, 250),
                text = "Highscore",
            };

            Controlsbutton = new Button(Font, Texture)
            {
                Position = new Vector2(315, 300),
                text = "Controls",
            };

            Quitbutton = new Button(Font, Texture)
            {
                Position = new Vector2(315, 400),
                text = "Quit",
            };

            NewGamebutton.Press += NewGame_Pressed;
            Highscorebutton.Press += Highscore_Pressed;
            Controlsbutton.Press += Controls_Pressed;
            Quitbutton.Press += Quit_Pressed;

            //List of components
            components = new List<ComponentSkeleton>()
            {
                NewGamebutton,
                Highscorebutton,
                Controlsbutton,
                Quitbutton,
            };
        }
        #endregion

        #region Update
        public override void Update(GameTime dt)
        {
            foreach (var comp in components)
            {
                comp.Update(dt);
            }
        }

        #endregion

        #region Draw
        public override void Draw(GameTime dt, SpriteBatch spriteB)
        {
            spriteB.Begin();

            spriteB.Draw(Backtext, new Rectangle(0, 0, 800, 600), Color.White);

            foreach (var comp in components)
            {
                comp.Draw(dt, spriteB);
            }

            spriteB.End();
        }
        #endregion

        #region Unload
        public override void Unload(GameTime dt)
        {
            content.Unload();
        }
        #endregion

        #region Pressed Buttons
        private void NewGame_Pressed(object a, EventArgs b)
        {
            game.ChangeCurrentState(new GameState(game, content, graphics));
        }

        private void Highscore_Pressed(object a, EventArgs b)
        {
            game.ChangeCurrentState(new HighscoreState(game, content, graphics));
        }

        private void Controls_Pressed(object a, EventArgs b)
        {
            game.ChangeCurrentState(new ControlsState(game, content, graphics));
        }

        private void Quit_Pressed(object a, EventArgs b)
        {
            game.Exit();
        }
        #endregion
    }
}
