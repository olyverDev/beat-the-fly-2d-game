using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace FlySWAT
{
    class Background
    {
        private Texture2D texture;
        private Vector2 position;
        public Background(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
