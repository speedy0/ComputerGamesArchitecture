using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace INM379CWCGA
{
    /// <summary>
    /// Diamonds help with the score.
    /// </summary>
    class Diamonds
    {
        #region Properties
        private Texture2D text;
        private Vector2 origin;
        private SoundEffect collect;

        public const int AddPoints = 60;
        public readonly Color Color = Color.Blue;

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
        public Diamonds(Level level, Vector2 position)
        {
            this.level = level;
            this.basePos = position;

            LoadResources();
        }
        #endregion

        #region Load
        public void LoadResources()
        {
            text = Level.Content.Load<Texture2D>("Graphics/Tiles/Others/Gem");
            origin = new Vector2(text.Width / 2.0f, text.Height / 2.0f);
            collect = Level.Content.Load<SoundEffect>("Audio/collect");
        }
        #endregion

        #region Events
        public void Collected(Player collis)
        {
            collect.Play();
        }
        #endregion

        #region Update
        public void Update(GameTime dt)
        {
            //Bounce properties
            const float BounceH = 0.15f;
            const float BounceR = 2.0f;
            const float BounceSync = -0.75f;

            //Bounce along sin curve 
            double t = dt.TotalGameTime.TotalSeconds * BounceR + Position.X * BounceSync;
            bounce = (float)Math.Sin(t) * BounceH * text.Height;
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
