using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    #region Camera
    /// <summary>
    /// Follows the player movement. 
    /// </summary>
    #endregion
    class Camera
    {
        #region Properties
        public Matrix Transform { get; private set; }

        public Vector2 Position
        {
            get { return position; }
        }
        Vector2 position;
        #endregion

        public void Follow(Level level)
        {
            Transform = Matrix.CreateTranslation(-level.Player.Position.X - (level.Player.sprite.Width / 2), -level.Player.Position.Y - (level.Player.sprite.Height / 2), 0) * Matrix.CreateTranslation(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2, 0);
        }
    }
}
