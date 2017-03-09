using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace FlySWAT
{
    class FlyAnimation 
    {
        private Texture2D texture;
        private Vector2 position;  
        private Vector2 origin; 
        private Vector2 velocity;
        private Rectangle rectangle;
        private float rotation;
        private Random randomCoo;
        private int currentFrame;
        private int frameHeight;
        private int frameWidth;
        private float timer;
        private float interval = 25;
        private int valueForAnimation = 1;
        private   float counter = 0;
        private Parameters parameters = new Parameters(1024,600);
        private bool checkAlive = true;
        private MouseState presentClick;
        private MouseState pastClick = Mouse.GetState();
        private GameTime gametime = new GameTime();

        public Texture2D Texture
        {
            set { texture = value; }
            get { return texture; }
        }
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }            
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }                
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }            
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public bool CheckAlive
        {
            get { return checkAlive; }
            set { checkAlive = CheckAlive; }
        }
       
        public FlyAnimation(Texture2D newTexture, Vector2 newPosition,  int newFrameWidth, int newFrameHeight )
        {
            texture = newTexture;
            randomCoo = new Random();
            position = newPosition;
            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;
        }

        public void Update(GameTime gameTime, Song song, SoundEffect soundKilled)
        {
            KillFly(ref soundKilled);

            if (checkAlive)
            {
               
                position += velocity;
                rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
                origin = new Vector2(frameWidth / 2, frameHeight / 2);
                Animate(gameTime);
                Borders();
                counter += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (counter > 2)
                {
                  AI((int)position.X, (int)position.Y);
                    counter = 0;
                }
            }
           
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void Animate(GameTime gameTime)
        {
            timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame+=valueForAnimation;
                timer = 0;
                if (currentFrame ==3 || currentFrame == 0)
                {
                    valueForAnimation =  -valueForAnimation;
                   // currentFrame = 0;
                }
            }
        }
        public int DivideVel(int r)
        {
            for (int i = 5; i < 11; i++)
            {
                while (r / 10 != 0)
                {
                    r /= 10;
                }
            }
                return r;
        }
        public void AI (int  x , int y)
        {
            int rX, rY;
          
                rX = GetRandomX();
                rY = GetRandomY();
           
           if (x != rX && y != rY )
           {
               velocity.X = (float)DivideVel(rX - x);
                velocity.Y = (float)DivideVel(rY - y) ;

                rotation = (float)Math.Asin( (rX - x) / Math.Sqrt((rX - x) * (rX - x) + (rY - y) * (rY - y)));
            }
         
        }

        public void Borders()
        {
            if (position.X <= 50)
            {
                position.X = 50;
                velocity = Vector2.Zero;
            }

            if (position.X + texture.Width / 7 >= parameters.ScreenWidth)
            {
                position.X = parameters.ScreenWidth - texture.Width / 7;
                velocity = Vector2.Zero;
            }
            if (position.Y <= 50)
            {
                position.Y = 50;
                velocity = Vector2.Zero;
            }

            if (position.Y + texture.Height >= parameters.ScreenHeight)
            {
                position.Y = parameters.ScreenHeight - texture.Height;
                velocity = Vector2.Zero;
            }
        }
        public void Killed()
        {
         
            velocity = Vector2.Zero ;
            checkAlive = false;
           
          
        }
        public int GetRandomX()
        {

            int X = randomCoo.Next(1, 1200);
            return X;
        }
        public int GetRandomY()
        {
            int Y = randomCoo.Next(1, 550);
            return Y;
        }
        public void KillFly(ref SoundEffect soundKilled)
        {
            presentClick = Mouse.GetState();
            if (presentClick.RightButton == ButtonState.Pressed  && pastClick.RightButton == ButtonState.Released)
            {
                
              MouseState mouseState = Mouse.GetState();
              double res = ( Math.Sqrt((mouseState.X - position.X) * (mouseState.X - position.X) + (mouseState.X - position.X) * (mouseState.Y - position.Y)));
              if (res >= 0 && res < 5)
              {
                  Killed();
                       soundKilled.Play();
                      
                  pastClick = presentClick;
              }
            }
        
        }
    }
}

