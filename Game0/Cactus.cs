﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheDesert.Collision;

namespace Game0
{
    public class Cactus
    {
        
        Game game;
        Texture2D texture;
        Color color = Color.White;
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        Vector2 position;
        public Vector2 Center;

        public Cactus(Game game, Vector2 position)
        {
            this.game = game;
            
            this.position = position;
            this.bounds = new BoundingRectangle(position, 16, 16);
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("colored_packed");
        }

        public void Update(GameTime gametime)
        { 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            var source = new Rectangle(96, 16, 16, 16);
            spriteBatch.Draw(texture, position, source, color);
        }
    }
}
