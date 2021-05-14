using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    #region Player Animator
    /// <summary>
    /// Defines player's animations
    /// </summary>
    #endregion  
    struct PlayerAnimator
    {
        #region Properties
        public Animation Animation
        {
            get { return animation; }
        }
        Animation animation;

        public int FrameIndex
        {
            get { return frameIndex; }
        }
        int frameIndex;

        public Vector2 Origin
        {
            get { return new Vector2(Animation.FrameWidth / 2.0f, Animation.FrameHeight); }
        }

        public int Width
        {
            get { return Animation.FrameWidth; }
        }

        public int Height
        {
            get { return Animation.FrameHeight; }
        }

        private float time;
        #endregion

        //Plays the animation
        public void Play(Animation animation)
        {
            if (Animation == animation)
                return;

            // Start the new animation.
            this.animation = animation;
            this.frameIndex = 0;
            this.time = 0.0f;
        }

        #region Draw
        public void Draw(GameTime dt, SpriteBatch sprite, Vector2 pos, SpriteEffects spriteEffects)
        {
            //Throws error if no animation is playing.
            if (Animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            time += (float)dt.ElapsedGameTime.TotalSeconds;
            while (time > Animation.Time)
            {
                time -= Animation.Time;

                if (Animation.IsLooping)
                {
                    frameIndex = (frameIndex + 1) % Animation.FrameCount;
                }
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, Animation.FrameCount - 1);
                }
            }

            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(FrameIndex * Animation.Text.Height, 0, Animation.Text.Height, Animation.Text.Height);

            // Draw the current frame.
            sprite.Draw(Animation.Text, pos, source, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.0f);
        }
        #endregion
    }
}
