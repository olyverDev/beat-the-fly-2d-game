using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlySWAT
{
    class Input
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        Color color = new Color(255, 255, 255, 255);
        public Vector2 size;
        bool down;
        public bool isClicked;

        public Input(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;
            size = new Vector2(graphics.Viewport.Width/4 , graphics.Viewport.Height/8 );
        }
       
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouseRectangle.Intersects(rectangle))
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 1; else color.A -= 1;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
                
            }
            else if (color.A < 255)
            {
                color.A += 3;
                isClicked = false;
            }

        }
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
        
    }
}
