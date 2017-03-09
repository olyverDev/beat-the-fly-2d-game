using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace FlySWAT
{
    class swatterAnimation
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 origin;
        private Vector2 velocity;
        private Rectangle rectangle;
        private int currentFrame;
        private int frameHeight;
        private int frameWidth;
        private float timer;
        private float interval = 25;
        private  int valueForAnimation = 1;
       
        private bool enabled = true;
        MouseState presentClick;
        MouseState pastClick = Mouse.GetState();

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        public swatterAnimation(Texture2D newTexture, Vector2 newPosition, int newFrameWidth, int newFrameHeight)
        {
            texture = newTexture;
            position = newPosition;
            frameWidth = newFrameWidth;
            frameHeight = newFrameHeight;
        }
        
        public void Update(GameTime gameTime , SoundEffect soundClap )
        {
            position += velocity;
            MouseState mouseState = Mouse.GetState();
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
              origin = new Vector2(rectangle.Width/2, rectangle.Height/2);
            position.X = mouseState.X + 80;
            position.Y = mouseState.Y ;
            presentClick = Mouse.GetState();
               if (presentClick.RightButton == ButtonState.Pressed)
               {
                   if (pastClick.RightButton == ButtonState.Released)
                   {
                       soundClap.Play();
                       enabled = true;
                   }
                   Animate(gameTime);
                 
               }
             pastClick = presentClick;
           }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0);
        }
        public void Animate(GameTime gameTime)
        {
            if (enabled)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds*5;
            if (timer > interval)
            {
                currentFrame += valueForAnimation;
                timer = 0;
                if (currentFrame == 6)
                {
                    valueForAnimation = -valueForAnimation ;
                
                }
                if ( currentFrame == 0)
                    {
                       currentFrame = 0;
                       enabled = false;
                       valueForAnimation = 1;
                    }
            }
            }

        }


       
            }

        }
       

