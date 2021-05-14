using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace INM379CWCGA.Objects
{
    class Shop
    {
        #region Properties
        private Texture2D text;
        private Vector2 origin;
        private SoundEffect Win;

        public bool isPowerup;
        public readonly Color Color = Color.Red;

        private Vector2 basePos;
        private float bounce;

        public int Width
        {
            get { return text.Width; }
        }

        public int Height
        {
            get { return text.Height; }
        }

        public Level Level
        {
            get { return level; }
        }
        Level level;

        public Vector2 Position
        {
            get { return basePos + new Vector2(0.0f, bounce); }
        }

        public CollisionCircle BoundingCircle
        {
            get { return new CollisionCircle(Position, Tiles.TileWidth / 3.0f); }
        }
        #endregion

        #region Constructor
        public Shop(Level level, Vector2 position)
        {
            this.level = level;
            this.basePos = position;

            LoadResources();
        }
        #endregion

        #region Load
        public void LoadResources()
        {
            text = Level.Content.Load<Texture2D>("Graphics/Tiles/Others/Chest");
            origin = new Vector2(text.Width / 2.0f, text.Height / 2.0f);
            Win = Level.Content.Load<SoundEffect>("Audio/Win");
        }
        #endregion

        #region Events
        public void UsedShop(Player collis)
        {
            Win.Play();
        }
        #endregion

        #region Update
        public void Update(GameTime dt)
        {
            
        }
        #endregion

        #region Draw
        public void Draw(GameTime dt, SpriteBatch spriteB)
        {
            spriteB.Draw(text, Position, null, Color, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
        }
        #endregion
    }
}
