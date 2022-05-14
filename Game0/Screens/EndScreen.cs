using Game0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheDesert.StateManagement;

namespace TheDesert.Screens
{
    public class EndScreen : GameScreen
    {
        private ContentManager _content;
        TileMap tileMap;
        private Texture2D _backgroundTexture;
        private Game game;
        public EndScreen(Game game)
        {
            this.game = game;
        }

    }
}
