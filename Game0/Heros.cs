using System;
using System.Runtime;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Game0
{
    public class Heros
    {

        private Texture2D texture;
        private double animationTimer;
        private int nextX;
        private int nextY;
        public Vector2 Position { get; set; }
        
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("chars");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            var source = new Rectangle(208, 48, 16, 16);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
    }
}
