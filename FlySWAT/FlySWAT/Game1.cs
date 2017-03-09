using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlySWAT 
{
    public class Game1 : Microsoft.Xna.Framework.Game 
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
     FlyAnimation fly;
     Background bg, cake;
     Text font;
     Parameters parameters = new Parameters(1024,600);
     swatterAnimation swatter;
     enum GameState
     {
         MainMenu,
         Options,
         Play
     }
     GameState CurrentGameState = GameState.MainMenu;
     SoundEffect soundClap, soundKilled;
     Song buzzing;
     Input btnPlay, btnExit;
     float timer = 0;
     float timer1 = 0;
     float gameTimer = 40;
     FlyAnimation fly1, fly2, fly3,fly4,fly5,fly6;
  
    
        public Game1()
     {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = parameters.ScreenWidth;
            graphics.PreferredBackBufferHeight = parameters.ScreenHeight;
            graphics.ApplyChanges();
            btnPlay = new Input(Content.Load<Texture2D>("PlayButton"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(400, 230));
            btnExit = new Input(Content.Load<Texture2D>("ButtonExit"), graphics.GraphicsDevice);
            btnExit.setPosition(new Vector2(400,330));
            fly = new FlyAnimation(Content.Load<Texture2D>("flies"), new Vector2(600, 300), (int) 85, 44);
            fly1 = new FlyAnimation(Content.Load<Texture2D>("flies"), new Vector2(-10, -10), (int)85, 44);
            fly2 = new FlyAnimation(Content.Load<Texture2D>("flies"), new Vector2(1034, 610), (int)85, 44);
            fly3 = new FlyAnimation(Content.Load<Texture2D>("flies"), new Vector2(-10, 610), (int)85, 44);
            fly4 = new FlyAnimation(Content.Load<Texture2D>("flies"), new Vector2(1034, -10), (int)85, 44);
            fly5 = new FlyAnimation(Content.Load<Texture2D>("flies"), new Vector2(-10, 620), (int)85, 44);
            fly6 = new FlyAnimation(Content.Load<Texture2D>("flies"), new Vector2(1029, 601), (int)85, 44);
            swatter = new swatterAnimation(Content.Load<Texture2D>("swatter"), new Vector2(1, 1), 342, 299);
            bg = new Background(Content.Load<Texture2D>("Background"), new Vector2(0, 0));
            cake = new Background(Content.Load<Texture2D>("cake"), new Vector2(780, 50));
            font = new Text(Content.Load<SpriteFont>("SpriteFont1"));
            soundClap = Content.Load<SoundEffect>("clap");
            soundKilled = Content.Load<SoundEffect>("killed");
            buzzing = Content.Load<Song>("Buzzing");
         MediaPlayer.Play(buzzing);
            MediaPlayer.IsRepeating = true;
        }
        protected override void UnloadContent()
        {
           
        }
  
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) 
                this.Exit();
           
            IsMouseVisible = parameters.IsMouseVisible;
            MouseState mouse = Mouse.GetState();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Play;
                    btnPlay.Update(mouse);
                    if (btnExit.isClicked == true) this.Exit();
                    btnExit.Update(mouse);

                    break;
                case GameState.Play:
                    timer1 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    fly.Update(gameTime, buzzing, soundKilled);
                    if(!fly.CheckAlive)
                    fly1.Update(gameTime, buzzing, soundKilled);
                    if ( timer1 > 10 )
                    fly2.Update(gameTime, buzzing, soundKilled);
                    if (!fly2.CheckAlive )
                        fly3.Update(gameTime, buzzing, soundKilled);
                    if ( timer1 > 15)
                        fly4.Update(gameTime, buzzing, soundKilled);

                    if (timer1 > 25)
                        fly4.Update(gameTime, buzzing, soundKilled);
                    if (!fly4.CheckAlive)
                    {
                        fly5.Update(gameTime, buzzing, soundKilled);
                        fly6.Update(gameTime, buzzing, soundKilled);
                    }

           swatter.Update(gameTime, soundClap);
          
                    break;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // прорисовка переключения экранов
            switch (CurrentGameState)
            {
                  case GameState.MainMenu:
                  spriteBatch.Draw(Content.Load<Texture2D>("Menu"), new Rectangle(0, 0, parameters.ScreenWidth, parameters.ScreenHeight), Color.White);
                   btnPlay.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                   parameters.IsMouseVisible = true;
                    break;

                case GameState.Play:
                    parameters.IsMouseVisible = false;
                      bg.Draw(spriteBatch);
            cake.Draw(spriteBatch);
            fly.Draw(spriteBatch);
            fly1.Draw(spriteBatch);
            fly2.Draw(spriteBatch);
            fly3.Draw(spriteBatch);
            fly4.Draw(spriteBatch);
            fly5.Draw(spriteBatch);
            fly6.Draw(spriteBatch);
         swatter.Draw(spriteBatch);
         gameTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
       //  String s = //String.Format( );
         string s = "Timer: " + gameTimer.ToString("0");
         font.DrawString(spriteBatch, s, new Vector2(20, 20));
         
         timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
         if (timer > 40)
         {
             if (fly.CheckAlive || fly1.CheckAlive || fly2.CheckAlive || fly3.CheckAlive || fly4.CheckAlive || fly5.CheckAlive || fly6.CheckAlive)
             {
                 font.DrawString(spriteBatch, "YOU LOOSE \n (push escape to exit)", new Vector2(370, 220));
                 fly.Velocity = Vector2.Zero;
                 fly1.Velocity = Vector2.Zero;
                 fly2.Velocity = Vector2.Zero;
                 fly3.Velocity = Vector2.Zero;
                 fly4.Velocity = Vector2.Zero;
                 fly5.Velocity = Vector2.Zero;
                 fly6.Velocity = Vector2.Zero;

                 gameTimer = 0;
             }
         }
         else if (!fly.CheckAlive && !fly1.CheckAlive && !fly2.CheckAlive && !fly3.CheckAlive && !fly4.CheckAlive && !fly5.CheckAlive && !fly6.CheckAlive)
         {
             MediaPlayer.Pause();
             font.DrawString(spriteBatch, "YOU WIN! \n (push escape to exit)\n " , new Vector2(370, 220));
            
         }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
