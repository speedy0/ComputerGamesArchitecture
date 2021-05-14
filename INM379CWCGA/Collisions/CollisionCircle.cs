using Microsoft.Xna.Framework;

namespace INM379CWCGA
{
    struct CollisionCircle
    {
        #region Properties
        public Vector2 Center;
        public float Radius;
        #endregion

        #region Constructor
        public CollisionCircle(Vector2 position, float radius)
        {
            Center = position;
            Radius = radius;
        }
        #endregion

        #region Interaction
        public bool Intersects(Rectangle rectangle)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, rectangle.Left, rectangle.Right),
                                    MathHelper.Clamp(Center.Y, rectangle.Top, rectangle.Bottom));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < Radius * Radius));
        }
        #endregion
    }
}
