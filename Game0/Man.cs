using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using TheDesert.Collision;

namespace Game0
{
    public class Man
    {
        /// <summary>
        /// The position of the man
        /// </summary>
        private Vector2 position;
        public Vector2 Center;
        public bool Flipped;
        public bool Moving;
        private Texture2D texture;
        private double animationTimer;
        private double healthTimer;
        private short animationFrameX = 0;
        private short animationFrameY = 0;
        private SoundEffect cactusHurt;
        KeyboardState keyboardState;
        Game game;

        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        public Vector2 Position => position;
        public bool Colliding { get; set; }
        public int Health => health;
        private int health = 10;

        public Man(Game game)
        {
            position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            this.game = game;
            this.bounds = new BoundingRectangle(new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2), 32, 32);
            Center = new Vector2(position.X + 8, position.Y + 8);
        }
        /// <summary>
        /// Loads the texture to render
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("spritesheet_caveman");
            cactusHurt = content.Load<SoundEffect>("Hit_Hurt");
        }
        /// <summary>
        /// Updates the sprite to run
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            Moving = false;

            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            healthTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -1);
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 1);
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                Flipped = true;
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                Flipped = false;
                Moving = true;
            }

            if (Flipped && Moving)
            {
                if (animationTimer > .5)
                {
                    animationFrameX++;
                    if (animationFrameX > 3)
                    {
                        animationFrameX = 0;
                        animationFrameY++;
                    }
                    if (animationFrameY > 3) animationFrameY = 0;
                    animationTimer -= .5;
                }
            }
            else if (Moving)
            {
                if (animationTimer > .5)
                {
                    animationFrameX--;
                    if (animationFrameX < 0)
                    {
                        animationFrameX = 3;
                        animationFrameY--;
                    }
                    if (animationFrameY < 0) animationFrameY = 3;
                    animationTimer -= .5;
                }
            }
            if (healthTimer > 1.0)
            {
                if (Colliding)
                {
                    health--;
                    cactusHurt.Play();
                }
                healthTimer -= 1.0;
            }
            if (position.X < -16) position.X = game.GraphicsDevice.Viewport.Width;
            if (position.Y < -32) position.Y = game.GraphicsDevice.Viewport.Height;
            if (position.X > game.GraphicsDevice.Viewport.Width) position.X = -16;
            if (position.Y > game.GraphicsDevice.Viewport.Height) position.Y = -32;
            Colliding = false;
            bounds.X = position.X;
            bounds.Y = position.Y;
        }
        /// <summary>
        /// Draws the sprite 
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = (Colliding) ? Color.Red : Color.White;
            var source = new Rectangle(animationFrameX * 32, animationFrameY * 32, 32, 32);
            spriteBatch.Draw(texture, position, source, color);
        }
    }
}
