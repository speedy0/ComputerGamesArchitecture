using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace INM379CWCGA.Collisions
{
    #region CollisionLayers
    /// <summary>
    /// This class adds additional layer in the game that allows to locate bounding box
    /// for debugging purposes. 
    /// </summary>
    #endregion
    class CollisionLayer
    {
        #region Lines for Bounding Box
        public void DrawLines(Color color, Vector2 p1, Vector2 p2, int lineW, SpriteBatch spriteB, Texture2D text)
        {

            float angle = (float)Math.Atan2(p1.Y - p2.Y, p1.X - p2.X);
            float length = Vector2.Distance(p1, p2);

            spriteB.Draw(text, p2, new Rectangle((int)p2.X, (int)p2.Y, (int)length, 3),
                color, angle, Vector2.Zero, lineW, SpriteEffects.None, 0);
        }
        #endregion

        #region Polygons for the Bounding Box
        public void Polygons(Color color, Vector2[] vtx, int count, int lineW, SpriteBatch spriteB, Texture2D text)
        {
            if (count > 0)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    DrawLines(color, vtx[i], vtx[i + 1], lineW, spriteB, text);
                }
                DrawLines(color, vtx[count - 1], vtx[0], lineW, spriteB, text);
            }
        }
        #endregion

        #region Rectangle Bounding Box
        public void Rectangle(Color color, Rectangle rect, int lineW, SpriteBatch spriteB, Texture2D text)
        {
            Vector2[] vtx = new Vector2[4];
            vtx[0] = new Vector2(rect.Left, rect.Top);
            vtx[1] = new Vector2(rect.Right, rect.Top);
            vtx[2] = new Vector2(rect.Right, rect.Bottom);
            vtx[3] = new Vector2(rect.Left, rect.Bottom);

            Polygons(color, vtx, 4, lineW, spriteB, text);
        }
        #endregion

        #region Circle Bounding Box
        public void Circles(Color color, Vector2 center, float radius, int lineW, SpriteBatch spriteB, Texture2D text, int segs = 16) { 

            Vector2[] vtx = new Vector2[segs];

            double add = Math.PI * 2.0 / segs;
            double theta = 0.0;

            for (int i = 0; i < segs; i++)
            {
                vtx[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += add;
            }

            Polygons(color, vtx, segs, lineW, spriteB, text);
        }
        #endregion
    }
}
