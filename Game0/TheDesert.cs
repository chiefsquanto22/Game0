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
        Cactus[] cactus;
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
            cactus = new Cactus[]{
                new Cactus(this),
                new Cactus(this),
                new Cactus(this),
                new Cactus(this)
            };

            man = new Man(this);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            man.Update(gameTime);
            // TODO: Add your update logic here
            foreach(Cactus cac in cactus)
            {
                cac.Update(man.Position);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            foreach (Cactus cac in cactus)
            {
                cac.Draw(_spriteBatch);
            }
            man.Draw(gameTime, _spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.DrawString(_spriteFont, "If you wanna leave the desert, all you gotta do is escape, partner.", new Vector2(16, 16), Color.Black);
            _spriteBatch.DrawString(_spriteFont, "Be careful if you come back though. These cacti like to move.", new Vector2(16, 32), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
