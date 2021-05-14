using System;
using Microsoft.Xna.Framework;

namespace INM379CWCGA
{
    #region Description
    /// <summary>
    /// Helpful methods for work with rectangles.
    /// </summary>
    #endregion 
    public static class RectangleExtensions
    {
        public static Vector2 IntersectionDepth(this Rectangle a, Rectangle b)
        {
            //Half sizes for A
            float halfWidthA = a.Width / 2.0f;
            float halfHeightA = a.Height / 2.0f;

            //Half sizes for B
            float halfWidthB = b.Width / 2.0f;
            float halfHeightB = b.Height / 2.0f;

            //Calculates centers
            Vector2 centerA = new Vector2(a.Left + halfWidthA, a.Top + halfHeightA);
            Vector2 centerB = new Vector2(b.Left + halfWidthB, b.Top + halfHeightB);

            //Calculates current non-intersecting distances between centers.
            float distX = centerA.X - centerB.X;
            float distY = centerA.Y - centerB.Y;

            //Calculates minimum non-intersecting distances between centers.
            float minDistX = halfWidthA + halfWidthB;
            float minDistY = halfHeightA + halfHeightB;

            //If there is no intersection, program will return (0,0)
            if (Math.Abs(distX) >= minDistX || Math.Abs(distY) >= minDistY)
                return Vector2.Zero;

            //Calculates intersection depths
            float depthX = distX > 0 ? minDistX - distX : -minDistX - distX;
            float depthY = distY > 0 ? minDistY - distY : -minDistY - distY;

            return new Vector2(depthX, depthY);
        }

        public static Vector2 BottomCenter(this Rectangle a)
        {
            return new Vector2(a.X + a.Width / 2.0f, a.Bottom);
        }
    }
}
