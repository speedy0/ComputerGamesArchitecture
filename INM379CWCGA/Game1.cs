using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#region Description for this INM379 coursework
/// <summary>
/// This is coursework for Computer Games Architecture (INM379) for City, University of London.
/// The game written in this code is made by Aleksander Napieralski
/// The game is a basic platformer (mario-like type of game) which will request player to finish the game in shortest time possible.
/// Player will be able to collect coins which will be available to be used to purchase items from the store to help to finish harder levels.
/// Player will be able to kill enemies or chose to avoid them if wish to do so.
/// 
/// There will be five levels and one boss room. If the player dies, the game will be lost and restarted. 
/// Although if player manages to finish all the levels and kill the boss, win condition will be met. 
/// </summary>
#endregion
namespace INM379CWCGA
{
    #region Project
    /// <summary>
    /// Project made as a coursework for INM379 at City, University of London.
    /// </summary>
    #endregion
    public class Game1 : Game
    {
        #region Properties
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Screen size of the game.
        public static int ScreenWidth;
        public static int ScreenHeight;

        //States
        private StateManager currState;
        private StateManager nextState;

        //Highscore Manager and properties (Due to the fact that Game is managed by GameState).
        ScoreManager rankingManager;
        SpriteFont Font;
        #endregion

        #region Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //graphics.IsFullScreen = true; //Allows for the fullscreen game. 
        }
        #endregion

        #region Initialisation
        protected override void Initialize()
        {
            //Initialises screen size and objects.
            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            base.Initialize();
        }
        #endregion

        #region Loading
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("FOnt/gameFont");

            //Loads Main Menu
            currState = new MainMenu(this, Content, graphics.GraphicsDevice);

            rankingManager = ScoreManager.LoadFile();

        }
        #endregion

        #region Update
        protected override void Update(GameTime dt)
        {
            if(nextState != null)
            {
                currState = nextState;
                nextState = null;
            }

            currState.Update(dt);

            base.Update(dt);
        }

        public void UpdateScores(int scores)
        {
            rankingManager.AddScore(new ScoresFile()
            {
                Value = scores,
                playerN = "Anski",
            });

            ScoreManager.SaveFile(rankingManager);
        }
        #endregion

        #region Events
        public void ChangeCurrentState(StateManager state)
        {
            nextState = state;
        }
        #endregion

        #region Draw
        protected override void Draw(GameTime dt)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currState.Draw(dt, spriteBatch);

           base.Draw(dt);
        }

        public void DrawHighscore(GameTime dt, SpriteBatch spriteB)
        {
            spriteB.DrawString(Font, string.Join("\n", rankingManager.Highscores.Select(a => a.playerN + ": " + a.Value).ToArray()), new Vector2(350, 150), Color.Cyan);
        }
        #endregion
    }
}
