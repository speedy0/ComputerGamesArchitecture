using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace INM379CWCGA
{
    #region Button
    /// <summary>
    /// Used for a buttons that will be visible in Main Menu.
    /// </summary>
    #endregion
    public class Button : ComponentSkeleton
    {
        #region Properties
        //Button settings
        private SpriteFont Font;
        private Texture2D Texture;

        //Mouse settings
        private MouseState currentMS;
        private MouseState previousMS;
        private bool target;
        public Color textcolor { get; set; }

        //Checks whether player has pressed the button.
        public event EventHandler Press;
        public bool Pressed
        {
            get; private set;
        }

        //Position
        public Vector2 Position
        {
            get; set;
        }

        //Specifies the size of a button.
        public Rectangle button
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public string text { get; set; }

        #endregion

        #region Constructor
        public Button(SpriteFont font, Texture2D text)
        {
            Texture = text;
            Font = font;
            textcolor = Color.Red;
        }
        #endregion

        #region Update
        public override void Update(GameTime dt)
        {
            previousMS = currentMS;
            currentMS = Mouse.GetState();

            var mouseBox = new Rectangle(currentMS.X, currentMS.Y, 1, 1);

            target = false;

            if (mouseBox.Intersects(button))
            {
                target = true;
                if (currentMS.LeftButton == ButtonState.Released && previousMS.LeftButton == ButtonState.Pressed)
                    Press?.Invoke(this, new EventArgs());
            }
        }
        #endregion

        #region Draw
        public override void Draw(GameTime dt, SpriteBatch spriteB)
        {
            //Color used for highlighting of the button
            Color color = Color.White;

            //if mouse will hover the button, the button should get highlighted.
            if (target)
            {
                color = Color.Blue;
            }

            //Used for Text
            if (!string.IsNullOrEmpty(text))
            {
                float x = (button.X + (button.Width / 2)) - (Font.MeasureString(text).X / 2);
                float y = (button.Y + (button.Height / 2)) - (Font.MeasureString(text).Y / 2);

                spriteB.Draw(Texture, button, color);
                spriteB.DrawString(Font, text, new Vector2(x, y), textcolor);
            }
        }
        #endregion
    }
}
