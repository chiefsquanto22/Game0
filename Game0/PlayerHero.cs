using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Game0
{
    public class PlayerHero
    {
        private Texture2D texture;
        private KeyboardState keyboardState;
        private bool flipped;
        private Vector2 position = new Vector2(200, 200);

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("chars");

        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffect = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            var source = new Rectangle(208, 48, 16, 16);
            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(0, 0), 0, spriteEffect, 0);
        }
    }
}
