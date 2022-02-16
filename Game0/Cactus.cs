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
        public Vector2 Position;
        private Body body;
        public bool Colliding { get; protected set; }

        public Cactus(Game game, Body body)
        {
            this.body = body;
            this.game = game;
            this.rnd = new Random();
            Position = new Vector2(rnd.Next(0, game.GraphicsDevice.Viewport.Width - 16), rnd.Next(32, game.GraphicsDevice.Viewport.Height - 16));
            this.bounds = new BoundingRectangle(Position, 16, 16);
            this.body.OnCollision += CollisionHandler;
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("colored_packed");
        }

        public void Update(Vector2 direction)
        {
            Position += direction;
            Colliding = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            var source = new Rectangle(96, 16, 16, 16);
            spriteBatch.Draw(texture, Position, source, color);
        }
        private bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;
            return true;
        }
    }
}
