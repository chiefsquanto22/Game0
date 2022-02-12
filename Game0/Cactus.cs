using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game0
{
    public class Cactus
    {
        Random rnd;
        Game game;
        Texture2D texture;
        Color color = Color.White;

        public Vector2 Position;

        public Cactus(Game game)
        {
            this.game = game;
            this.rnd = new Random();
            Position = new Vector2(rnd.Next(0, game.GraphicsDevice.Viewport.Width - 16), rnd.Next(32, game.GraphicsDevice.Viewport.Height - 16));
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("colored_packed");
        }

        public void Update(GameTime gameTime)
        {
            if (Position.X < 0) Position.X = game.GraphicsDevice.Viewport.Width;
            if (Position.Y < 0) Position.Y = game.GraphicsDevice.Viewport.Height;
        }
        public void Move(bool direction, bool moving)
        {
            if (direction && moving)
            {

                Position += new Vector2(1, 0);
            }
            if (!direction && moving)
            {
                Position += new Vector2(-1, 0);
            }
            
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            var source = new Rectangle(96, 16, 16, 16);
            spriteBatch.Draw(texture, Position, source, color);
        }
    }
}
