using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using INM379CWCGA.Objects;

namespace INM379CWCGA
{
    #region Player Description
    /// <summary>
    /// Character has been designed by Aleksander Napieralski in pixelart. 
    /// The character has only walking animation and therefore, will not use various of different animations. 
    /// </summary>
    #endregion
    class Player
    {
        #region Properties
        //Animations
        private Animation idle;
        private Animation run;
        private Animation jump;
        private Animation celebrate;
        private Animation die;
        public PlayerAnimator sprite;

        //Player's Texture
        //public Texture2D playerText;
        //Character's Health
        public int Health
        {
            get { return health; }
        }
        int health;
        //Input's Speed
        public float Speed;
        //Checks whether the player is alive
        public bool isAlive;
        //Checks whether the player is buying
        public bool hasBought;
        public bool hasBoughtpowerup;
        //Position of the Player
        public Vector2 Position
        {
           get { return position; }
           set { position = value; }
        }
        Vector2 position;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;

        //Texture of the player
        //public int Width { get { return playerText.Width; } }
        //public int Height {  get { return playerText.Height; } }

        //Allows to flip the player
        private SpriteEffects flip = SpriteEffects.None;

        //Sounds
        private SoundEffect lose;
        private SoundEffect damage;
        private SoundEffect healthPotion;

        //Level
        public Level Level
        {
            get { return level; }
        }
        Level level;

        #region Input Description
        /// <summary>
        /// This game will allow for the player to move with Keyboard and shoot with mouse.
        /// </summary>
        #endregion
        //Input
        KeyboardState currentKS;
        KeyboardState previousKS;
        MouseState currentMS;
        MouseState previousMS;

        #region Physic Settings
        //Horizontal movement
        private const float Acceleration = 13000.0f;
        private const float MaxSpeed = 1750.0f;

        //Vertical Movement
        private const float MaxJump = 0.35f;
        private const float JumpVel = -3500.0f;
        private const float GravityAcceleration = 3400.0f;
        private const float MaxFallingSpeed = 550.0f;
        private const float JumpControlPower = 0.14f;

        //Drag Factors
        private const float GroundDragFactor = 0.48f;
        private const float AirDragFactor = 0.58f;

        public bool isJumping;
        private bool wasJumping;
        private float jumpTime;
        public bool IsGround
        {
            get { return isGround; }
        }
        bool isGround;

        private Rectangle Bounds;

        public Rectangle BoundingRect
        {
            get
            {
                int left = (int)Math.Round(Position.X - sprite.Origin.X) + Bounds.X;
                int top = (int)Math.Round(Position.Y - sprite.Origin.Y) + Bounds.Y;

                return new Rectangle(left, top, Bounds.Width, Bounds.Height);
            }
        }

        private float PrevBottom;
        #endregion
        #endregion

        #region Constructor
        public Player(Level level, Vector2 pos)
        {
            this.level = level;
            LoadContent();
            Initialise();
            ResetLevel(pos);
        }
        #endregion

        #region Level Settings
        public void ResetLevel(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.Play(idle);
        }
        #endregion

        #region Initialisation
        public void Initialise()
        {
            //Initialises texture and attributes of the player
            health = 3;
            Speed = 5f;
            isAlive = true;
            hasBought = false;
            hasBoughtpowerup = false;
        }
        #endregion

        #region Loading Content
        public void LoadContent()
        {
            // Load animations
            idle = new Animation(Level.Content.Load<Texture2D>("Graphics/Player/Idle"), 0.1f, true);
            celebrate = new Animation(Level.Content.Load<Texture2D>("Graphics/Player/Celebrate"), 0.1f, false);
            jump = new Animation(Level.Content.Load<Texture2D>("Graphics/Player/Jump"), 0.1f, false);
            run = new Animation(Level.Content.Load<Texture2D>("Graphics/Player/Run"), 0.1f, true);
            die = new Animation(Level.Content.Load<Texture2D>("Graphics/Player/Die"), 0.1f, false);

            //Load Sounds
            lose = Level.Content.Load<SoundEffect>("Audio/Lose");
            //damage = Level.Content.Load<SoundEffect>("Audio/Dmgtaken");
            healthPotion = Level.Content.Load<SoundEffect>("Audio/healthpotion");

            // Calculate bounds within texture size.            
            int width = (int)(idle.FrameWidth * 0.4);
            int left = (idle.FrameWidth - width) / 2;
            int height = (int)(idle.FrameWidth * 0.8);
            int top = idle.FrameHeight - height;
            Bounds = new Rectangle(left, top, width, height);
        }
        #endregion

        #region Update
        public void Update(GameTime dt, KeyboardState ks, DisplayOrientation orientation)
        {
            currentKS = Keyboard.GetState();
            //MovementInput(ks, orientation);
            Physics(dt);

            if(isAlive && isGround)
            {
                if (Math.Abs(Velocity.X) - 0.1f > 0)
                {
                    sprite.Play(run);
                }
                else
                {
                    sprite.Play(idle);
                }
            }

            Speed = 0.0f;
            isJumping = false;
        }
        #endregion

        #region (Unused) Movement
        /*private void MovementInput(KeyboardState ks, DisplayOrientation orientation)
        {
            //Stops running in place
            if (Math.Abs(Speed) < 0.05f)
                Speed = 0.0f;
            //Movement Keys
            *//*if (currentKS.IsKeyDown(Keys.A))
                Speed = -1.0f;
            if (currentKS.IsKeyDown(Keys.D))
                Speed = 1.0f;*//*
            //isJumping = ks.IsKeyDown(Keys.Space);

            if (currentKS.IsKeyDown(Keys.E))
            {
                level.Restart();
                health -= 1;
            }
            
        }*/
        #endregion

        #region Physics
        public void Physics(GameTime dt)
        {
            float timepass = (float)dt.ElapsedGameTime.TotalSeconds;
            Vector2 PreviousPosition = Position;

            velocity.X += Speed * Acceleration * timepass;
            velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * timepass, -MaxFallingSpeed, MaxFallingSpeed);
            velocity.Y = Jumping(dt, velocity.Y);

            //Applies correct Drag factor to the player.
            if (IsGround)
                velocity.X *= GroundDragFactor;
            else
                velocity.X *= AirDragFactor;

            //Stops player from running faster than allowed.
            velocity.X = MathHelper.Clamp(velocity.X, -MaxSpeed, MaxSpeed);

            //Applies velocity to the player
            Position += velocity * timepass;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

            Collisions();

            if (Position.X == PreviousPosition.X)
                velocity.X = 0;

            if (Position.Y == PreviousPosition.Y)
                velocity.Y = 0;
        }

        private void Collisions()
        {
            Rectangle bounds = BoundingRect;
            int left = (int)Math.Floor((float)bounds.Left / Tiles.TileWidth);
            int right = (int)Math.Ceiling(((float)bounds.Right / Tiles.TileWidth)) - 1;
            int top = (int)Math.Floor((float)bounds.Top / Tiles.TileHeight);
            int bottom = (int)Math.Ceiling(((float)bounds.Bottom / Tiles.TileHeight)) - 1;

            // Reset flag to search for ground collision.
            isGround = false;

            // For each potentially colliding tile,
            for (int y = top; y <= bottom; ++y)
            {
                for (int x = left; x <= right; ++x)
                {
                    // If this tile is collidable,
                    TilesCollision collision = Level.Collision(x, y);
                    if (collision != TilesCollision.Passable)
                    {
                        // Determine collision depth (with direction) and magnitude.
                        Rectangle tileBounds = Level.Bounds(x, y);
                        Vector2 depth = RectangleExtensions.IntersectionDepth(bounds, tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            // Resolve the collision along the shallow axis.
                            if (absDepthY < absDepthX || collision == TilesCollision.Platform)
                            {
                                // If we crossed the top of a tile, we are on the ground.
                                if (PrevBottom <= tileBounds.Top)
                                    isGround = true;

                                // Ignore platforms, unless we are on the ground.
                                if (collision == TilesCollision.Impassable || IsGround)
                                {
                                    // Resolve the collision along the Y axis.
                                    Position = new Vector2(Position.X, Position.Y + depth.Y);

                                    // Perform further collisions with the new bounds.
                                    bounds = BoundingRect;
                                }
                            }
                            else if (collision == TilesCollision.Impassable) // Ignore platforms.
                            {
                                // Resolve the collision along the X axis.
                                Position = new Vector2(Position.X + depth.X, Position.Y);

                                // Perform further collisions with the new bounds.
                                bounds = BoundingRect;
                            }
                            else if(collision == TilesCollision.Lava)
                            {
                                level.Restart();
                                health -= 1;

                                if(health <= 0)
                                {
                                    Kill(null);
                                }
                            }
                        }
                    }
                }
            }
            PrevBottom = bounds.Bottom;
        }
        #endregion

        #region Events
        public void Kill(Mage by)
        {
            isAlive = false;

            if(by != null)
            {
                sprite.Play(die);
                lose.Play();
            }
            else
                lose.Play();
        }

        public void LevelCompleted()
        {
            hasBought = false;
            hasBoughtpowerup = false;
            sprite.Play(celebrate);
        }

        private float Jumping(GameTime dt, float vel)
        {
            if (isJumping)
            {
                if ((!wasJumping && IsGround) || jumpTime > 0.0f)
                {
                    jumpTime += (float)dt.ElapsedGameTime.TotalSeconds;
                    sprite.Play(jump);
                }

                if (0.0f < jumpTime && jumpTime <= MaxJump)
                {
                    vel = JumpVel * (1.0f - (float)Math.Pow(jumpTime / MaxJump, JumpControlPower));
                }
                else
                    jumpTime = 0.0f;
            }
            else
                jumpTime = 0.0f;

            wasJumping = isJumping;
            return vel;
        }

        public void DrankHealthPotion(HealthPotion potion)
        {
            health += 1;
        }
        #endregion

        #region Draw
        public void Draw(GameTime dt, SpriteBatch spriteB)
        {
            //Used if using texture and not animator.
            //Vector2 Pos;
            //Pos.X = Position.X - Width / 2;
            //Pos.Y = Position.Y - Height / 2;
            //sprite.Draw(playerText, Position, null, Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);


            // Flip sprite in the way that it is moving
            if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;

                sprite.Draw(dt, spriteB, Position, flip);
        }
        #endregion
    }
}
