using System;
using System.Runtime;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Game0
{
    public class TheDesert : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private Man man;
        
        private Song backgroundNoise;
        Random rnd = new Random();
        List<Cactus> cactus;

        private TileMap _tileMap;
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


            cactus = new List<Cactus>();
            for (int i = 0; i < 4; i++)
            {
                var position = new Vector2(
                    rnd.Next(0, GraphicsDevice.Viewport.Width - 16),
                    rnd.Next(0, GraphicsDevice.Viewport.Height - 16)
                    );

                cactus.Add(new Cactus(this, position));
            }
            man = new Man(this);
            _tileMap = new TileMap("map.txt");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("Text Font");
            
            backgroundNoise = Content.Load<Song>("wind1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundNoise);

            man.LoadContent(Content);
            foreach (Cactus cac in cactus) cac.LoadContent();
            _tileMap.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (var cac in cactus) cac.Update(gameTime);
            foreach (var cac in cactus)
            {
                if (man.Bounds.CollidesWith(cac.Bounds))
                {
                    man.Colliding = true;
                }
            }
            man.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _tileMap.Draw(gameTime, _spriteBatch);
            foreach (Cactus cac in cactus) cac.Draw(_spriteBatch);
            man.Draw(gameTime, _spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.DrawString(_spriteFont, "If you wanna leave the desert, all you gotta do is escape, partner.", new Vector2(16, 16), Color.Black);
            _spriteBatch.DrawString(_spriteFont, "Punch a cactus. You won't.", new Vector2(16, 32), Color.Black);
            _spriteBatch.DrawString(_spriteFont, "Health: " + man.Health, new Vector2(16, 46), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
