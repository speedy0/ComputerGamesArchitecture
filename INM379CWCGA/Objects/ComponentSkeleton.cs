using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    public abstract class ComponentSkeleton
    {
        public abstract void Update(GameTime dt);

        public abstract void Draw(GameTime dt, SpriteBatch spriteB);
    }
}
