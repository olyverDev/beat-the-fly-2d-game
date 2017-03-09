using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace FlySWAT
{
    class Text
    {
         SpriteFont font;
         Vector2 Position
         { 
             get;
             set; 
         }
         public Text(SpriteFont Font)
         {
             font = Font;
             Position = Position;
         }
         public void DrawString(SpriteBatch spriteBatch, string Text, Vector2 Position)
         {
             spriteBatch.DrawString(font, Text, Position, Color.White);
         }

    }
}
