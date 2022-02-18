using System;
using System.Runtime;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
namespace Game0
{
    public class TheDesert : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private Man man;
        Random rnd = new Random();
        List<Cactus> cactus;
        private World world;
        public TheDesert()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "The Desert";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            world = new World();
            world.Gravity = Vector2.Zero;
            var edges = new Body[] {
            world.CreateEdge(new Vector2(0, 0), new Vector2(0, GraphicsDevice.Viewport.Width)),
            world.CreateEdge(new Vector2(0, 0), new Vector2(0, GraphicsDevice.Viewport.Height)),
            world.CreateEdge(new Vector2(GraphicsDevice.Viewport.Width, 0), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)),
            world.CreateEdge(new Vector2(GraphicsDevice.Viewport.Height, 0), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)),
            };
            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1.0f);
            }

            cactus = new List<Cactus>();
            for (int i = 0; i < 4; i++)
            {
                var position = new Vector2(
                    rnd.Next(0, GraphicsDevice.Viewport.Width - 16),
                    rnd.Next(0, GraphicsDevice.Viewport.Height - 16)
                    );
                var body = world.CreateRectangle(16, 16, 1, position, 1, BodyType.Dynamic);
                body.Rotation = 0;
                body.LinearVelocity = new Vector2(0, 0);
                body.SetRestitution(1);
                body.AngularVelocity = 0;
                cactus.Add(new Cactus(this, body));
            }
            man = new Man(this, world.CreateRectangle(16, 16, 1));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("Text Font");
            man.LoadContent(Content);
            foreach (Cactus cac in cactus) cac.LoadContent();


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (var cac in cactus) cac.Update(gameTime);
            foreach(var cac in cactus)
            {
                if (man.Bounds.CollidesWith(cac.Bounds))
                {
                    man.Colliding = true;
                    cac.Colliding = true;
                    Vector2 collisionAxis = cac.Center - man.Center;
                    collisionAxis.Normalize();
                    float angle = (float)System.Math.Acos(Vector2.Dot(collisionAxis, Vector2.UnitX));

                }
            }
            man.Update(gameTime);
            // TODO: Add your update logic here
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            foreach (Cactus cac in cactus) cac.Draw(_spriteBatch);
            man.Draw(gameTime, _spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.DrawString(_spriteFont, "If you wanna leave the desert, all you gotta do is escape, partner.", new Vector2(16, 16), Color.Black);
            _spriteBatch.DrawString(_spriteFont, "Punch a cactus. You won't.", new Vector2(16, 32), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
