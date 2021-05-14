using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    #region Collision detection 
    /// <summary>
    /// Sets collision for the tiles that will be used within the levels.
    /// </summary>
    #endregion

        enum TilesCollision
        {
            Platform = 0,
            Passable = 1,
            Impassable = 2,
            Lava = 3,
        }

        struct Tiles
        {
            //Data important for Tiles. 
            public Texture2D text;
            public TilesCollision Collision;
            public const int TileWidth = 40;
            public const int TileHeight = 32;
            public static readonly Vector2 Tilesize = new Vector2(TileWidth, TileHeight);

            public Tiles(Texture2D texture, TilesCollision collision)
            {
                text = texture;
                Collision = collision;
            }
        }
}
