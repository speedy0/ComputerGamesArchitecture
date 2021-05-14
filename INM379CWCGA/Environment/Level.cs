using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using INM379CWCGA.Objects;

#region File Description
//Level file will contain levels for the game and the background.
//There will be five levels and one boss room level.
//If the player completes all the levels and the boss room, the game will be won.

//Levels will be obtained from text document.
#endregion

namespace INM379CWCGA
{
    #region Levels
    /// <summary>
    /// Loads the game levels.
    /// </summary>
    #endregion 
    class Level
    {
        #region Properties
        //Structure of the level
        private Tiles[,] tiles;
        private Loader loader;

        //Sounds
        private readonly SoundEffect nextLevel;
        private readonly SoundEffect win;

        //Entities in the level
        public Player Player
        {
            get { return player; }
        }
        Player player;

        public List<Mage> mages = new List<Mage>();

        //Start and end
        private Vector2 start;
        private Point exit = InvalidPosition;
        private static readonly Point InvalidPosition = new Point(-1, -1);

        //Levels
        public bool LevelCompleted
        {
            get { return levelCompleted; }
        }
        bool levelCompleted;

        public bool isLevel1;
        public bool isLevel2;
        public bool isLevel3;
        public bool isLevel4;
        public bool isLevel5;
        public bool isLevel6;
        public bool isLevel7;
        public bool isLevel8;
        public bool isLevel9;
        public bool isBoss;

        //Random Game State
        private Random random = new Random(354668);

        //Content Manager
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        //Level Score System
        public int Score
        {
            get { return score; }
        }
        int score;

        private const int timerpoints = 5;

        //Time Managemer
        public TimeSpan TimeLeft
        {
            get { return timeleft; }
        }
        TimeSpan timeleft;

        public TimeSpan CountDownTimer
        {
            get { return cdt; }
        }
        TimeSpan cdt;

        public int Width
        {
            get { return tiles.GetLength(0); }
        }

        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        //Lists for Diamonds, Coins and chests
        public List<Diamonds> diamonds = new List<Diamonds>();
        public List<Coins> coins = new List<Coins>();
        public List<Shop> shop = new List<Shop>();
        public List<HealthPotion> potions = new List<HealthPotion>();

        //Powerup
        public bool isPowerup;
        #endregion

        #region Constructor
        public Level(IServiceProvider sp, Stream filestream, int LevelInd)
        {
            content = new ContentManager(sp, "Content");
            loader = new Loader(filestream);
            loader.ReadXML("content/info.xml");

            timeleft = TimeSpan.FromMinutes(1.0);

            LoadTiles(filestream);

            nextLevel = Content.Load<SoundEffect>("Audio/teleport");
            win = Content.Load<SoundEffect>("Audio/Win");

            isPowerup = false;
        }
        #endregion

        #region Loading Tiles
        /// <summary>
        /// Loads tiles
        /// </summary>
        private void LoadTiles(Stream fileStream)
        {
            // Loads the level and checks whether all lines are the same length.
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.
            tiles = new Tiles[width, lines.Count];

            // Loops through every tile position,
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // to load each tile.
                    char tileType = lines[y][x];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }

            // Confirms whether the game has starting and ending point.
            //If not, the game will throw the error that will be easily identifiable. 
            if (Player == null)
                throw new NotSupportedException("A level must have a starting point.");
            if (exit == InvalidPosition)
                throw new NotSupportedException("A level must have an exit point.");

        }
        private Tiles LoadTile(string name, TilesCollision collision)
        {
            if (isLevel1)
                return new Tiles(Content.Load<Texture2D>("Graphics/Tiles/Level1/" + name), collision);
            else if (isLevel2)
                return new Tiles(Content.Load<Texture2D>("Graphics/Tiles/Level2/" + name), collision);
            else if (isLevel3)
                return new Tiles(Content.Load<Texture2D>("Graphics/Tiles/Level3/" + name), collision);
            else
                return new Tiles(Content.Load<Texture2D>("Graphics/Tiles/Level1/" + name), collision);
        }

        private Tiles LoadOtherTiles(string name, TilesCollision collision)
        {
            return new Tiles(Content.Load<Texture2D>("Graphics/Tiles/Others/" + name), collision);
        }

        private Tiles RandomTile()
        {
            Random rand = new Random();
            int x = rand.Next(1, 2);

            if (x == 1)
                return LoadTile("Ground", TilesCollision.Impassable);
            else
                return LoadTile("Platform", TilesCollision.Passable);
        }

        private Tiles LoadStart(int x, int y)
        {
            if (Player != null)
                throw new NotSupportedException("The level can only have one starting point.");

            start = RectangleExtensions.BottomCenter(Bounds(x, y));
            player = new Player(this, start);

            return new Tiles(null, TilesCollision.Passable);
        }

        private Tiles LoadExit(int x, int y)
        {
            if (exit != InvalidPosition)
                throw new NotSupportedException("The level can have only one one exit point.");

            exit = Bounds(x, y).Center;

                return LoadTile("Exit", TilesCollision.Passable);
        }

        private Tiles LoadDiamonds(int x, int y)
        {
           Point position = Bounds(x, y).Center;
           diamonds.Add(new Diamonds(this, new Vector2(position.X, position.Y)));

           return new Tiles(null, TilesCollision.Passable);
        }

        private Tiles LoadCoins(int x, int y)
        {
           Point position = Bounds(x, y).Center;
           coins.Add(new Coins(this, new Vector2(position.X, position.Y)));
               return new Tiles(null, TilesCollision.Passable);
        }

        private Tiles LoadHealthPotions(int x, int y)
        {
            Point position = Bounds(x, y).Center;
            potions.Add(new HealthPotion(this, new Vector2(position.X, position.Y)));
            return new Tiles(null, TilesCollision.Passable);
        }

        private Tiles LoadShop(int x, int y)
        {
            Point position = Bounds(x, y).Center;
            shop.Add(new Shop(this, new Vector2(position.X, position.Y)));
            return new Tiles(null, TilesCollision.Passable);
        }

        private Tiles LoadMageTile(int x, int y)
        {
            Vector2 position = RectangleExtensions.BottomCenter(Bounds(x, y));
            mages.Add(new Mage(this, position));

            return new Tiles(null, TilesCollision.Passable);
        }

        private Tiles LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                // Blank space
                case '.':
                    return new Tiles(null, TilesCollision.Passable);

                // Start point
                case 'S':
                    return LoadStart(x, y);

                // Exit Point
                case 'X':
                    return LoadExit(x, y);

                // Diamonds
                case 'D':
                    return LoadDiamonds(x, y);

                // Coins
                case 'C':
                    return LoadCoins(x, y);

                case 'O':
                    return LoadHealthPotions(x, y);
                // Platform
                case '-':
                        return LoadTile("Platform", TilesCollision.Platform);

                // Impassable block
                case '#':
                    return LoadTile("Ground", TilesCollision.Impassable);

                case 'L':
                    return LoadOtherTiles("Lava", TilesCollision.Lava);

                case 'P':
                    return LoadShop(x, y);

                case 'R':
                    return RandomTile();

                // Various enemies
                case 'M':
                    return LoadMageTile(x, y);
                //                case 'B':
                //                    return LoadEnemyTile(x, y, "MonsterB");
                //                case 'C':
                //                    return LoadEnemyTile(x, y, "MonsterC");
                //                case 'D':
                //                    return LoadEnemyTile(x, y, "MonsterD");

                // Unknown tile type character
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }
        #endregion

        #region Unload
        public void Clear()
        {
            Content.Unload();
        }
        #endregion

        #region Physics of the tiles
        public Rectangle Bounds(int x, int y)
        {
            return new Rectangle(x * Tiles.TileWidth, y * Tiles.TileHeight, Tiles.TileWidth, Tiles.TileHeight);
        }

        public TilesCollision Collision(int x, int y)
        {
            if (x < 0 || x >= Width)
                return TilesCollision.Impassable;

            if (y < 0 || y >= Height)
                return TilesCollision.Passable;

            return tiles[x, y].Collision;
        }
        #endregion

        #region Update
        public void Update(GameTime dt, KeyboardState ks, DisplayOrientation orientation)
        {
            if (!Player.isAlive || TimeLeft == TimeSpan.Zero)
                Player.Physics(dt);
            else if (LevelCompleted)
            {
                //Collect points for the seconds left on the timer.
                int seconds = (int)Math.Round(dt.ElapsedGameTime.TotalSeconds * 100.0f);
                seconds = Math.Min(seconds, (int)Math.Ceiling(TimeLeft.TotalSeconds));
                timeleft -= TimeSpan.FromSeconds(seconds);
                score += seconds * timerpoints;
            }
            else
            {
                timeleft -= dt.ElapsedGameTime;
                Player.Update(dt, ks, orientation);
                UpdateDiamonds(dt);
                UpdateCoins(dt);
                UpdatePotions(dt);
                UpdateMages(dt);
                Shopped(dt);

                if (Player.BoundingRect.Top >= Height * Tiles.TileHeight)
                    Kill(null);

                if (Player.isAlive && Player.IsGround && Player.BoundingRect.Contains(exit))
                    ExitReached();
            }

            //Ensures that timer does not pass zero and becomes negative. 
            if (timeleft < TimeSpan.Zero)
                timeleft = TimeSpan.Zero;

            if (cdt > TimeSpan.Zero)
            {
                cdt -= dt.ElapsedGameTime;
            }

            if (Player.hasBoughtpowerup && cdt == TimeSpan.Zero)
            {
                player.hasBoughtpowerup = false;
            }
        }

        private void UpdateDiamonds(GameTime dt)
        {
            for (int i = 0; i < diamonds.Count; ++i)
            {
                Diamonds diam = diamonds[i];
                diam.Update(dt);

                if (diam.BoundingCircle.Intersects(Player.BoundingRect))
                {
                    diamonds.RemoveAt(i--);
                    CollectDiamond(diam, Player);
                }
            }
        }

        private void UpdateCoins(GameTime dt)
        {
            for (int i = 0; i < coins.Count; ++i)
            {
                Coins coin = coins[i];
                coin.Update(dt);

                if (coin.BoundingCircle.Intersects(Player.BoundingRect))
                {
                    coins.RemoveAt(i--);
                    CollectCoins(coin, Player);
                }
            }
        }

        private void UpdatePotions(GameTime dt)
        {
            for (int i = 0; i < potions.Count; ++i)
            {
                HealthPotion health = potions[i];
                health.Update(dt);

                if (health.BoundingCircle.Intersects(Player.BoundingRect))
                {
                    potions.RemoveAt(i--);
                    CollectHealthPot(health, Player);
                }
            }
        }

        private void Shopped(GameTime dt)
        {
            for (int i = 0; i < shop.Count; ++i)
            {
                Shop shops = shop[i];
                shops.Update(dt);

                if (shops.BoundingCircle.Intersects(Player.BoundingRect) && Player.hasBoughtpowerup)
                {
                    shop.RemoveAt(i--);
                    Shopping(shops, Player);
                    player.hasBought = true;
                }
            }
        }

        private void UpdateMages(GameTime dt)
        {
            foreach(Mage Mages in mages){
                Mages.Update(dt);
                if (Mages.BoundingRect.Intersects(player.BoundingRect) && Player.hasBoughtpowerup)
                {
                    //KillMage(Mages);
                }
                else if (Mages.BoundingRect.Intersects(player.BoundingRect) && Player.hasBoughtpowerup == false)
                {
                    Kill(Mages);
                }
            }
        }
        #endregion

        #region Collecting pick-ups
        private void CollectDiamond(Diamonds diam, Player pl)
        {
            score += Diamonds.AddPoints;
            diam.Collected(pl);
        }

        private void CollectCoins(Coins coin, Player pl)
        {
            score += Coins.AddPoints;
            coin.CollectedCoins(pl);
        }
        #endregion

        #region Collecting power-ups & Shop
        private void Shopping(Shop shops, Player pl)
        {
            if (player.hasBoughtpowerup)
            {
                cdt = TimeSpan.FromMinutes(0.15f);
                shops.UsedShop(pl);
            }
        }

        private void CollectHealthPot(HealthPotion potion, Player pl)
        {
            pl.DrankHealthPotion(potion);

        }
        #endregion

        #region Events
        private void Kill(Mage by)
        {
            Player.Kill(by);
        }

        private void ExitReached()
        {
            Player.LevelCompleted();
            nextLevel.Play();
            levelCompleted = true;
        }

        public void Restart()
        {
            Player.ResetLevel(start);
        }
        #endregion

        #region Draw
        public void Draw(GameTime dt, SpriteBatch spriteB)
        {
            DrawTiles(spriteB);

            foreach (Diamonds diam in diamonds)
                diam.Draw(dt, spriteB);

            foreach (Coins coin in coins)
                coin.Draw(dt, spriteB);

            foreach (HealthPotion health in potions)
                health.Draw(dt, spriteB);

            foreach (Shop shops in shop)
                shops.Draw(dt, spriteB);

            Player.Draw(dt, spriteB);

            foreach (Mage Mages in mages)
                Mages.Draw(dt, spriteB);
        }

        private void DrawTiles(SpriteBatch spriteB)
        {
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    Texture2D texture = tiles[j, i].text;
                    if (texture != null)
                    {
                        Vector2 pos = new Vector2(j, i) * Tiles.Tilesize;
                        spriteB.Draw(texture, pos, Color.White);
                    }
                }
            }
        }
        #endregion
    }
}