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
        private PlayerHero playerHero;
        private Heros[] heros;
        private Texture2D texture;
        Random rnd = new Random();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerHero = new PlayerHero();
            heros = new Heros[]
            {
                new Heros(){Position = new Vector2(100,200)},
                new Heros(){Position = new Vector2(45, 300)},
                new Heros(){Position = new Vector2(300, 400)}
            };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>("chars");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerHero.LoadContent(Content);
            foreach (var hero in heros) hero.LoadContent(Content);
            _spriteFont = Content.Load<SpriteFont>("Text Font");
            texture = Content.Load<Texture2D>("chars");

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
            foreach (var hero in heros)
            {
                hero.Sprite = new Rectangle(GraphicsDevice.Viewport.Width - rnd.Next(2, 3) * 16, rnd.Next(2, 3) * 16, 16, 16);
                hero.Draw(_spriteBatch, texture);
            }
            // TODO: Add your drawing code here
            _spriteBatch.DrawString(_spriteFont, "Should you choose to leave this place, press the Escape button. May your God be with you.", new Vector2(16, 16), Color.Black);
            _spriteBatch.DrawString(_spriteFont, "Where am I again?", new Vector2(heros[1].Position.X + 16, heros[1].Position.Y), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
