using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheDesert.Collision;

namespace TheDesert
{
    enum Treasures
    {
        Bars = 7,
        Coins = 4,
        Goblet = 6,
        Crown = 9,
        Pile = 10,
    }
    public class Treasure
    {

        Game game;
        Texture2D texture;
        Color color = Color.White;
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        Vector2 position;
        public Vector2 Center;
        public int TreasureType;
        public bool Collected { get; set; } = false;

        public Treasure(Game game, Vector2 position, int treasureType)
        {
            this.game = game;
            this.TreasureType = treasureType;
            this.position = position;
            this.bounds = new BoundingRectangle(position, 16, 16);
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("sheet");
        }

        public void Update(GameTime gametime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Collected) return;
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            var source = new Rectangle(TreasureType / 3 * 16, TreasureType / 4 * 16, 16, 16);
            spriteBatch.Draw(texture, position, source, color);
        }
    }
}
