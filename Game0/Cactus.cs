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
        Vector2 origin;
        public Vector2 Center;
        private Body body;
        public bool Colliding { get; set; }
        

        public Cactus(Game game, Body body)
        {
            this.body = body;
            this.game = game;
            this.rnd = new Random();
            origin = new Vector2(49, 49);
            
            this.bounds = new BoundingRectangle(origin, 16, 16);
            Center = new Vector2(origin.X + 8, origin.Y + 8);
            this.body.OnCollision += CollisionHandler;
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("colored_packed");
        }

        public void Update(GameTime gametime)
        { 
            Colliding = false;
            if (origin.X < -16) origin.X = game.GraphicsDevice.Viewport.Width;
            if (origin.Y < -16) origin.Y = game.GraphicsDevice.Viewport.Height;
            if (origin.X > game.GraphicsDevice.Viewport.Width) origin.X = -16;
            if (origin.Y > game.GraphicsDevice.Viewport.Height) origin.Y = -16;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            var source = new Rectangle(96, 16, 16, 16);
            spriteBatch.Draw(texture, body.Position, source, color, body.Rotation, origin, 1, SpriteEffects.None, 0);
        }
        private bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;
            return true;
        }
    }
}
