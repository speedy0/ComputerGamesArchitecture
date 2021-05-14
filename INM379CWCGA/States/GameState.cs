using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using INM379CWCGA.Collisions;
using INM379CWCGA.Controls;

namespace INM379CWCGA
{
    #region GameState
    /// <summary>
    /// State to start the game.
    /// </summary>
    #endregion
    class GameState : StateManager
    {
        #region Properties
        //Sets some object. 
        Camera camera;

        //Background
        Texture2D Backtext;
        Background background;

        //Levels
        private int levelIndex = -1;
        public Level level;
        private const int numOfLevels = 10;
        private bool Continuebut;

        //Inputs
        private KeyboardState ks;
        private MouseState ms;

        //HUD
        private SpriteFont font;
        private Texture2D winLayer;
        private Texture2D lostLayer;
        private static readonly TimeSpan Warning = TimeSpan.FromSeconds(15);

        //Saves Total score achieved throughout the levels. 
        public int score;

        //Powerup
        public bool hasBoughtpowerup;

        //Object for bounding boxes
        CollisionLayer cls = new CollisionLayer();
        Texture2D outlayer;
        private bool isLayersOn;

        //Command Manager for bindings
        CommandManager cman = new CommandManager();
        #endregion

        #region Constructor
        public GameState(Game1 g, ContentManager cm, GraphicsDevice gd) : base(g, cm, gd)
        {
            camera = new Camera();
            background = new Background();

            //Loads layers for the HUD.
            winLayer = content.Load<Texture2D>("Graphics\\HUD\\WinLayer");
            lostLayer = content.Load<Texture2D>("Graphics\\HUD\\Lostlayer");
            font = content.Load<SpriteFont>("FOnt\\gameFont");

            //Loads Background for scrolling and stable background. 
            Backtext = content.Load<Texture2D>("Graphics\\Background\\BG1");
            background.LoadContent(Backtext, graphics);

            //Loads the level in. 
            LoadLevel();

            outlayer = content.Load<Texture2D>("Graphics\\HUD\\boundingBox");

            isLayersOn = true;

            Bindings();
        }
        #endregion

        #region Level Settings
        //Loads the levels. 
        private void LoadLevel()
        {
            //Sets current level loaded in using level Index. 
            levelIndex = (levelIndex + 1) % numOfLevels;

            if (level != null)
                level.Clear();

            string levelPath = string.Format("Content/Levels/{0}.txt", levelIndex);
            using (Stream filestream = TitleContainer.OpenStream(levelPath))
                level = new Level(game.Services, filestream, levelIndex);

            //Uses Current level method to set status which level is played out. This will help in changing tiles, backgrounds and enemies. 
            SetLevelStatus(levelIndex);
        }

        //Sets which level is currently played based on the Index given. This will be used to change background and tiles throughout the game. 
        private void SetLevelStatus(int Index)
        {
            if (Index == 0)
            {
                level.isLevel1 = true;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 1)
            {
                level.isLevel1 = false;
                level.isLevel2 = true;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 2)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = true;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 3)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = true;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 4)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = true;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 5)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = true;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 6)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = true;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 7)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = true;
                level.isLevel9 = false;
                level.isBoss = false;
            }
            else if (Index == 8)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = true;
                level.isBoss = false;
            }
            else if (Index == 9)
            {
                level.isLevel1 = false;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = true;
            }
            else
            {
                level.isLevel1 = true;
                level.isLevel2 = false;
                level.isLevel3 = false;
                level.isLevel4 = false;
                level.isLevel5 = false;
                level.isLevel6 = false;
                level.isLevel7 = false;
                level.isLevel8 = false;
                level.isLevel9 = false;
                level.isBoss = false;
            }
        }

        private void StateCheck(GameTime dt)
        {
            //Button to pass onto the next level
            bool continuelevel = ks.IsKeyDown(Keys.F);

            if (!Continuebut && continuelevel)
            {
                if (!level.Player.isAlive)
                {
                    level.Restart();
                }
                if (level.TimeLeft == TimeSpan.Zero)
                {
                    level.Restart();
                }
                if (level.LevelCompleted)
                {
                    if (level.Player.Health > 3)
                    {
                        int a = level.Player.Health - 3;
                        score += (a * 100);
                    }
                    score += level.Score;
                    LoadLevel();
                }
            }
            Continuebut = continuelevel;
        }
        #endregion

        #region Bindings
        private void Bindings()
        {
            //Player Movement
            cman.AddKeyboardBinding(Keys.A, Left); //Moves to the left
            cman.AddKeyboardBinding(Keys.D, Right); //Moves to the right
            cman.AddKeyboardBinding(Keys.Space, Jump);

            //Back Button
            cman.AddKeyboardBinding(Keys.Escape, BackButton);

            //Debugging Purposes
            cman.AddKeyboardBinding(Keys.L, Layers); //Turns on/off bounding box layers

            //Shop
            cman.AddKeyboardBinding(Keys.S, Shop);
        }
        #endregion

        #region Buttons
        public void Layers(ButtonStatess button, Vector2 amount)
        {
            if (button == ButtonStatess.UP)
            {
                Layers();
            }
        }

        public void Left(ButtonStatess button, Vector2 amount)
        {
            if (button == ButtonStatess.DOWN)
            {
                level.Player.Speed = -1;
            }
            else
            {
                level.Player.Speed = 0;
            }
        }

        public void Right(ButtonStatess button, Vector2 amount)
        {
            if (button == ButtonStatess.DOWN)
            {
                level.Player.Speed = 1;
            }
            else
            {
                level.Player.Speed = 0;
            }
        }

        public void Jump(ButtonStatess button, Vector2 amount)
        {
            if (button == ButtonStatess.DOWN)
            {
                level.Player.isJumping = true;
            }
            else
            {
                level.Player.isJumping = false;
            }
        }

        public void BackButton(ButtonStatess button, Vector2 amount)
        {
            if (button == ButtonStatess.DOWN)
            {
                game.ChangeCurrentState(new MainMenu(game, content, graphics));
            }
            else
            {

            }
        }

        public void Shop(ButtonStatess button, Vector2 amount)
        {
            if (button == ButtonStatess.DOWN && level.Player.hasBought == false)
            {
                level.Player.hasBoughtpowerup = true;
            }
            else
            {

            }
        }
            #endregion

        #region Layers
            public void Layers()
        {
            if (!isLayersOn)
            {
                isLayersOn = true;
            }
            else
            {
                isLayersOn = false;
            }
        }
        #endregion

        #region Unload
        public override void Unload(GameTime dt)
        {
            content.Unload();
        }
        #endregion

        #region Update
        public override void Update(GameTime dt)
        {
            //Uses timepass float for the scrolling background. 
            float Timepass = (float)dt.ElapsedGameTime.TotalSeconds;

            ks = Keyboard.GetState();

            //Updates Level's update function.
            level.Update(dt, ks, game.Window.CurrentOrientation);

            //Checks current state of the player. 
            StateCheck(dt);

            //Sets camera position using Matrix depending on the player. 
            camera.Follow(level);

            //Sets scrolling background. Possible to speed or slow down the animation.
            if (level.isLevel1)
                background.Update(Timepass * 10);

            //Checks whether the game finishes
            if (level.isBoss && level.LevelCompleted)
            {
                game.UpdateScores(score);
                game.ChangeCurrentState(new WonState(game, content, graphics));
            }

            if(level.Player.Health <= 0)
            {
                level.Player.Kill(null);
            }

            cman.Update();
        }
        #endregion

        #region Draw
        public override void Draw(GameTime dt, SpriteBatch spriteB)
        {
            spriteB.Begin(transformMatrix: camera.Transform);

            //Sets Rolling & static background depending on the current level loaded in. 
            if (level.isLevel1)
                background.Draw(spriteB);
            else
            {
                spriteB.Draw(Backtext, new Rectangle((int)camera.Position.X, (int)camera.Position.Y - 350, 1920, 1080), Color.White);
                spriteB.Draw(Backtext, new Rectangle((int)camera.Position.X - 1920, (int)camera.Position.Y - 350, 1920, 1080), Color.White);
                spriteB.Draw(Backtext, new Rectangle((int)camera.Position.X + 1920, (int)camera.Position.Y - 350, 1920, 1080), Color.White);
            }

            //Draws from level including the player. 
            level.Draw(dt, spriteB);
            //Player.Draw(spriteBatch);

            //Draws HUD. 
            Hud(spriteB);

            if (isLayersOn)
                BoundingBoxes(spriteB);

            spriteB.End();
        }
        #endregion

        #region HUD
        private void Hud(SpriteBatch spriteB)
        {
            Vector2 center = new Vector2(level.Player.Position.X, level.Player.Position.Y);
            Vector2 Location = new Vector2(level.Player.Position.X - 380, level.Player.Position.Y - 250);
            Texture2D status = null;

            // Determine the status overlay message to show.
            if (level.TimeLeft == TimeSpan.Zero)
            {
                if (level.LevelCompleted)
                {
                    status = winLayer;
                }
                else
                {
                    status = lostLayer;
                }
            }
            else if (!level.Player.isAlive)
            {
                game.ChangeCurrentState(new LostState(game, content, graphics));
            }

            if (status != null)
            {
                // Draw status message.
                Vector2 layerHud = new Vector2(level.Player.Position.X - 368, level.Player.Position.Y - 300);
                spriteB.Draw(status, layerHud, Color.White); //center - statusSize / 2
            }

            // Draw time remaining. Uses modulo division to cause blinking when the
            // player is running out of time.
            string timeString = "Time Left: " + level.TimeLeft.Minutes.ToString("00") + ":" + level.TimeLeft.Seconds.ToString("00");
            Color timeColor;
            if (level.TimeLeft > Warning ||
                level.LevelCompleted ||
                (int)level.TimeLeft.TotalSeconds % 2 == 0)
            {
                timeColor = Color.Cyan;
            }
            else
            {
                timeColor = Color.Red;
            }

            string cdt = "Power-up Time Left: " + level.CountDownTimer.Minutes.ToString("00") + ":" + level.CountDownTimer.Seconds.ToString("00");

            DrawShadowedString(font, timeString, center + new Vector2(-350, -200), timeColor, spriteB);

            if (level.CountDownTimer > TimeSpan.Zero)
            {
                DrawShadowedString(font, cdt, center + new Vector2(0, 230), Color.Red, spriteB);
            }
            
            // Draw score
            DrawShadowedString(font, "Level Points: " + level.Score.ToString(), center + new Vector2(-347, -180), Color.Cyan, spriteB);
            DrawShadowedString(font, "Total Points: " + score.ToString(), center + new Vector2(-347, 230), Color.Yellow, spriteB);

            spriteB.DrawString(font, "Health: " + level.Player.Health, center + new Vector2(300, -200), Color.Red);
        }

        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color, SpriteBatch spriteB)
        {
            spriteB.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black);
            spriteB.DrawString(font, value, position, color);
        }
        void BoundingBoxes(SpriteBatch spriteB)
        {
            //Player
            cls.Rectangle(Color.Blue, level.Player.BoundingRect, 1, spriteB, outlayer);

            //Mages
            foreach (var e in level.mages)
            {
                cls.Rectangle(Color.Red, e.BoundingRect, 1, spriteB, outlayer);
            }

            //Coins
            foreach (var c in level.coins)
            {
                cls.Circles(Color.Green, new Vector2(c.Position.X, c.Position.Y), c.Width / 4, 1, spriteB, outlayer);
            }

            //Diamonds
            foreach (var d in level.diamonds)
            {
                cls.Circles(Color.Green, new Vector2(d.Position.X, d.Position.Y), d.Width / 2, 1, spriteB, outlayer);
            }

            //Shops
            foreach (var s in level.shop)
            {
                cls.Circles(Color.Green, new Vector2(s.Position.X, s.Position.Y), s.Width / 2, 1, spriteB, outlayer);
            }

            //Health Packs
            foreach (var h in level.potions)
            {
                cls.Circles(Color.Green, new Vector2(h.Position.X, h.Position.Y), h.Width / 2, 1, spriteB, outlayer);
            }
        }
        #endregion
    }
}
