using System;
using System.Runtime;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using TheDesert.StateManagement;
using TheDesert.Screens;
using TheDesert;

namespace Game0
{
    public class TheDesert : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;
        private SpriteFont _spriteFont;
        private Man man;
        private Treasure[] treasure;
        private Song backgroundNoise;
        Random rnd = new Random();
        List<Cactus> cactus;
        private SoundEffect treasurePickup;
        private TileMap _tileMap;
        public TheDesert()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "The Desert";
            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);
            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);
            AddInitialScreens();

        }
        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new BackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            treasure = new Treasure[]
            {
                new Treasure(this, new Vector2((float)rnd.NextDouble()*1600,(float)rnd.NextDouble()*1600),(int)Treasures.Bars),
                new Treasure(this, new Vector2((float)rnd.NextDouble()*1600,(float)rnd.NextDouble()*1600),(int)Treasures.Coins),
                new Treasure(this, new Vector2((float)rnd.NextDouble()*1600,(float)rnd.NextDouble()*1600),(int)Treasures.Crown),
                new Treasure(this, new Vector2((float)rnd.NextDouble()*1600,(float)rnd.NextDouble()*1600),(int)Treasures.Goblet),
                new Treasure(this, new Vector2((float)rnd.NextDouble()*1600,(float)rnd.NextDouble()*1600),(int)Treasures.Pile),
            };

            cactus = new List<Cactus>();
            for (int i = 0; i < 100; i++)
            {
                var position = new Vector2(
                    rnd.Next(0, 16 * 99),
                    rnd.Next(0, 16 * 99)
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
            treasurePickup = Content.Load<SoundEffect>("Pickup_Coin14");
            backgroundNoise = Content.Load<Song>("wind1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundNoise);

            man.LoadContent(Content);
            foreach (Treasure t in treasure) t.LoadContent();
            foreach (Cactus cac in cactus) cac.LoadContent();
            _tileMap.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _screenManager.AddScreen(new PauseMenuScreen(), null);
                man.Paused = true;
            }
            man.Paused = false;
            foreach (var t in treasure)
            {
                if (!t.Collected && t.Bounds.CollidesWith(man.Bounds))
                {
                    t.Collected = true;
                    treasurePickup.Play();
                }
            }
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
            float playerX = MathHelper.Clamp(man.Position.X, GraphicsDevice.Viewport.Width / 2, 16 * 100 - GraphicsDevice.Viewport.Width / 2);
            float playerY = MathHelper.Clamp(man.Position.Y, GraphicsDevice.Viewport.Height / 2, 16 * 100 - GraphicsDevice.Viewport.Height / 2);
            float offsetX = GraphicsDevice.Viewport.Width / 2 - playerX;
            float offsetY = GraphicsDevice.Viewport.Height / 2 - playerY;
            Matrix transform;
            transform = Matrix.CreateTranslation(offsetX, offsetY, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _tileMap.Draw(gameTime, _spriteBatch);
            foreach (Treasure t in treasure) t.Draw(_spriteBatch);
            foreach (Cactus cac in cactus) cac.Draw(_spriteBatch);
            man.Draw(gameTime, _spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.DrawString(_spriteFont, "Health: " + man.Health, new Vector2(man.HealthPosition.X, man.HealthPosition.Y), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
