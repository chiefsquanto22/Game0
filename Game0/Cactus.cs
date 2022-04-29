using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheDesert.Collision;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
namespace Game0
{
    public class Cactus
    {
        Random rnd;
        Game game;
        Texture2D texture;
        Color color = Color.White;
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        Vector2 position;
        public Vector2 Center;
        
        public bool Colliding { get; set; }
        

        public Cactus(Game game, Vector2 position)
        {
            this.game = game;
            this.rnd = new Random();
            this.position = position;
            this.bounds = new BoundingRectangle(position, 16, 16);
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("colored_packed");
        }

        public void Update(GameTime gametime)
        { 
            Colliding = false;
            if (position.X < -16) position.X = game.GraphicsDevice.Viewport.Width;
            if (position.Y < -16) position.Y = game.GraphicsDevice.Viewport.Height;
            if (position.X > game.GraphicsDevice.Viewport.Width) position.X = -16;
            if (position.Y > game.GraphicsDevice.Viewport.Height) position.Y = -16;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            var source = new Rectangle(96, 16, 16, 16);
            spriteBatch.Draw(texture, position, source, color);
        }
        private bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;
            return true;
        }
    }
}
