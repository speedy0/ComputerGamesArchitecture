using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace INM379CWCGA
{
    class Background
    {
        #region Properties
        private Texture2D texture;

        private Vector2 Position;

        private Vector2 Origin;

        private Vector2 Size;

        private int Height;

        private int Width;
        #endregion

        #region Load
        public void LoadContent(Texture2D text, GraphicsDevice graphics)
        {
            texture = text;
            Height = graphics.Viewport.Height;
            Width = graphics.Viewport.Width;

            Position = new Vector2(Width / 2, Height / 2);

            Origin = new Vector2(texture.Width / 2, 0);
            Size = new Vector2(0, texture.Height);
        }
        #endregion

        #region Update
        public void Update(float height)
        {
            Position.Y += height;
            Position.Y = Position.Y % texture.Height;
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteB)
        {
            if (Position.Y < Height)
            {
                spriteB.Draw(texture, Position, null, Color.White, 0.0f, Origin, 1.0f, SpriteEffects.None, 0.0f);
            }

            spriteB.Draw(texture, Position - Size, null, Color.White, 0.0f, Origin, 1.0f, SpriteEffects.None, 0.0f);
        }
        #endregion
    }
}
