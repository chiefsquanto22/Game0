using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TheDesert.Collision;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
namespace Game0
{
    public class Man
    {
        /// <summary>
        /// The position of the man
        /// </summary>
        public Vector2 Position = new Vector2(200, 200);
        public bool Flipped;
        public bool Moving;
        private Texture2D texture;
        private double animationTimer;
        private short animationFrameX = 0;
        private short animationFrameY = 0;
        KeyboardState keyboardState;
        Game game;
        private Body body;
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        public bool Colliding { get; protected set; }

        public Man(Game game, Body body)
        {
            this.body = body;
            this.game = game;
            this.bounds = new BoundingRectangle(new Vector2(200 - 16, 200 - 16), 16, 16);
            this.body.OnCollision += CollisionHandler;
        }
        /// <summary>
        /// Loads the texture to render
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("spritesheet_caveman");
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
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                Position += new Vector2(0, -1);
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                Position += new Vector2(0, 1);
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Position += new Vector2(-1, 0);
                Flipped = true;
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                Position += new Vector2(1, 0);
                Flipped = false;
                Moving = true;
            }

            if (Flipped)
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
            else
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

            if (Position.X < 0) Position.X = game.GraphicsDevice.Viewport.Width;
            if (Position.Y < 0) Position.Y = game.GraphicsDevice.Viewport.Height;
            if (Position.X > game.GraphicsDevice.Viewport.Width) Position.X = 0;
            if (Position.Y > game.GraphicsDevice.Viewport.Height) Position.Y = 0;
            Colliding = false;
        }
        /// <summary>
        /// Draws the sprite 
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (Flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            var source = new Rectangle(animationFrameX * 32, animationFrameY * 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
        private bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;
            return true;
        }
    }
}
