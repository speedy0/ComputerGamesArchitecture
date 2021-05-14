using System;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    #region Animation
    /// <summary>
    /// This class takes care of Animation for the player
    /// </summary>
    #endregion 
    class Animation
    {
        #region Properties
        //Frames or textures
        public Texture2D Text
        {
            get { return text; }
        }
        Texture2D text;

        //Duration of each frame.
        public float Time
        {
            get { return time; }
        }
        float time;

        public bool IsLooping
        {
            get { return isLooping; }
        }
        bool isLooping;

        //Amount of frames per animation.
        public int FrameCount
        {
            get { return Text.Width / FrameWidth; }
        }

        //Width of the frame.
        public int FrameWidth
        {
            get { return Text.Height; }
        }

        //Height of the frame
        public int FrameHeight
        {
            get { return Text.Height; }
        }
        #endregion

        public Animation(Texture2D tex, float timer, bool loop)
        {
            this.text = tex;
            this.time = timer;
            this.isLooping = loop;
        }
    }
}
