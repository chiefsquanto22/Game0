using System;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game0
{
    public class TheDesert : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private Texture2D texture;
        private Man man;
        Random rnd = new Random();
        Vector2[] cactus = new Vector2[4];
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
            for (int i = 0; i < cactus.Length; i++)
            {
                cactus[i] = new Vector2(rnd.Next(0, GraphicsDevice.Viewport.Width) - 16, rnd.Next(0, GraphicsDevice.Viewport.Height) - 16);
            }
            man = new Man() { Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2) };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>("colored_packed");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("Text Font");
            man.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            man.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, cactus[0], new Rectangle(96, 16, 16, 16), Color.White);
            _spriteBatch.Draw(texture, cactus[1], new Rectangle(96, 16, 16, 16), Color.White);
            _spriteBatch.Draw(texture, cactus[2], new Rectangle(96, 16, 16, 16), Color.White);
            _spriteBatch.Draw(texture, cactus[3], new Rectangle(96, 16, 16, 16), Color.White);
            man.Draw(gameTime, _spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.DrawString(_spriteFont, "If you wanna leave the desert, all you gotta do is escape, partner.", new Vector2(16, 16), Color.Black);
            _spriteBatch.DrawString(_spriteFont, "Be careful if you come back though. These cacti like to move.", new Vector2(16, 32), Color.Black);
            _spriteBatch.DrawString(_spriteFont, "'The more I run, the less I seem to move...'", new Vector2(man.Position.X, man.Position.Y - 20), Color.Black);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
