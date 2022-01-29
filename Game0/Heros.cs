using System;
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
        
        
        public Rectangle Sprite { get; set; }
        public Vector2 Position { get; set; }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            spriteBatch.Draw(texture, Position, Sprite, Color.White);
        }
    }
}
