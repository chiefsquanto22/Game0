using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Game0
{
    public class Man
    {
        /// <summary>
        /// The position of the man
        /// </summary>
        private Vector2 position = new Vector2(200, 200);
        private bool flipped;
        private Texture2D texture;
        private double animationTimer;
        private short animationFrameX = 0;
        private short animationFrameY = 0;
        KeyboardState keyboardState;
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

            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) position += new Vector2(0, -1);
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) position += new Vector2(0, 1);
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                flipped = false;
            }
            if (flipped)
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
        }
        /// <summary>
        /// Draws the sprite 
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            var source = new Rectangle(animationFrameX * 32, animationFrameY * 32, 32, 32);
            spriteBatch.Draw(texture, position, source, Color.White);
        }
    }
}
