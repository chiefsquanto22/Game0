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
        private Vector2 origin = new Vector2(200, 200);
        public Vector2 Center;
        public bool Flipped;
        public bool Moving;
        private Texture2D texture;
        private double animationTimer;
        private short animationFrameX = 0;
        private short animationFrameY = 0;
        KeyboardState keyboardState;
        Game game;
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        public bool Colliding { get; set; }

        public Man(Game game)
        {
            
            this.game = game;
            this.bounds = new BoundingRectangle(new Vector2(200, 200), 32, 32);
            Center = new Vector2(origin.X + 8, origin.Y + 8);
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
                origin += new Vector2(0, -1);
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                origin += new Vector2(0, 1);
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                origin += new Vector2(-1, 0);
                Flipped = true;
                Moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                origin += new Vector2(1, 0);
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

            if (origin.X < -16) origin.X = game.GraphicsDevice.Viewport.Width;
            if (origin.Y < -32) origin.Y = game.GraphicsDevice.Viewport.Height;
            if (origin.X > game.GraphicsDevice.Viewport.Width) origin.X = -16;
            if (origin.Y > game.GraphicsDevice.Viewport.Height) origin.Y = -32;
            Colliding = false;
            bounds.Position = origin;
        }
        /// <summary>
        /// Draws the sprite 
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = (Colliding) ? Color.Red : Color.White;
            SpriteEffects spriteEffects = (Flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            var source = new Rectangle(animationFrameX * 32, animationFrameY * 32, 32, 32);
            spriteBatch.Draw(texture, origin, source, Color.White);
        }
        private bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;
            return true;
        }
    }
}
