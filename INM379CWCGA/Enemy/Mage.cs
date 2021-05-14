using System;
using INM379CWCGA.EnemyStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace INM379CWCGA
{
    class Mage
    {
        #region Properties
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }
        private Vector2 position;

        public Level Level
        {
            get { return level; }
        }
        Level level;

        public bool Spotted
        {
            get { return Spotted; }
            set { Spotted = value; }
        }

        //private FSM fsm;

        #region Physics
        private Rectangle Bounds;
        public Rectangle BoundingRect
        {
            get
            {
                int left = (int)Math.Round(Position.X - mage.Origin.X) + Bounds.X;
                int top = (int)Math.Round(Position.Y - mage.Origin.Y) + Bounds.Y;

                return new Rectangle(left - 10, top - 20, (int)(Bounds.Width * 2), (int)(Bounds.Height * 1.5f));
            }
        }

        private Rectangle Eyes;

        public float speed = 100.0f;
        public int health = 1;
        #endregion

        #region Animator
        private PlayerAnimator mage;
        private Animation Idle;

        enum Direction
        {
            Left = -1,
            Right = 1,
        }

        private Direction direction = Direction.Left;

        private float waiting;

        private const float maxWaiting = 0.5f;
        #endregion
        #endregion

        #region Constructor
        public Mage(Level level, Vector2 position)
        {
            this.level = level;
            this.position = position;

            //fsm = new FSM(this);

            //Initialise();
            Load();
        }
        #endregion

        #region Initialise
        public void Initialise()
        {
            /*Idlestate idle = new Idlestate();
            Chasestate chase = new Chasestate();

            idle.AddTransition(new Transition(chase, () => Spotted));
            chase.AddTransition(new Transition(idle, () => !Spotted));

            fsm.AddState(idle);
            fsm.AddState(chase);

            fsm.Initialise("Idle");*/
        }
        #endregion

        #region Load
        public void Load()
        {
            Idle = new Animation(level.Content.Load<Texture2D>("Graphics/Enemy/mageL"), 0.5f, true);
            mage.Play(Idle);

            int width = (int)(Idle.FrameWidth * 0.35);
            int left = (Idle.FrameWidth - width) / 2;
            int height = (int)(Idle.FrameWidth * 0.7);
            int top = Idle.FrameHeight - height;
            Bounds = new Rectangle(left, top, width, height);

            Eyes = new Rectangle(left * 2, top * 2, width * 2, height * 2);
        }
        #endregion

        #region Update
        public void Update(GameTime dt)
        {
            IdleAction(dt); 
        }
        #endregion

        #region Idle
        public void IdleAction(GameTime dt)
        {
            float timePassed = (float)dt.ElapsedGameTime.TotalSeconds;

            // Calculate tile position based on the side we are walking towards.
            float posX = Position.X + Bounds.Width / 2 * (int)direction;
            int tileX = (int)Math.Floor(posX / Tiles.TileWidth) - (int)direction;
            int tileY = (int)Math.Floor(Position.Y / Tiles.TileHeight);

            if (waiting > 0)
            {
                // Wait for some amount of time.
                waiting = Math.Max(0.0f, waiting - (float)dt.ElapsedGameTime.TotalSeconds);
                if (waiting <= 0.0f)
                {
                    // Then turn around.
                    direction = (Direction)(-(int)direction);
                }
            }
            else
            {
                // If we are about to run into a wall or off a cliff, start waiting.
                if (level.Collision(tileX + (int)direction, tileY - 1) == TilesCollision.Impassable ||
                    level.Collision(tileX + (int)direction, tileY) == TilesCollision.Passable)
                {
                    waiting = maxWaiting;
                }
                else
                {
                    // Move in the current direction.
                    //Vector2 velocity = new Vector2((int)direction * GameEngine.Instance.MageSets.speed * timePassed, 0.0f);
                    Vector2 velocity = new Vector2((int)direction * speed * timePassed, 0.0f);
                    position = position + velocity;
                }
            }
        }
        #endregion

        #region Draw        
        public void Draw(GameTime dt, SpriteBatch spriteB)
        {
            // Draw facing the way the enemy is moving.
            SpriteEffects flip = direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            mage.Draw(dt, spriteB, Position, flip);
        }
        #endregion
    }
}
