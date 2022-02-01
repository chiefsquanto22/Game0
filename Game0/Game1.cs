using System;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private Texture2D texture;
        Random rnd = new Random();
        Vector2[] cactus = new Vector2[4];
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            for (int i = 0; i < cactus.Length; i++)
            {
                cactus[i] = new Vector2(rnd.Next(0, GraphicsDevice.Viewport.Width) - 16, rnd.Next(0, GraphicsDevice.Viewport.Height) - 16);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>("colored_packed");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("Text Font");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
            // TODO: Add your drawing code here
            _spriteBatch.DrawString(_spriteFont, "If you wanna leave the desert, all you gotta do is escape, partner.", new Vector2(16, 16), Color.Black);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
