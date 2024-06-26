﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Golf_v1_0
{
    public enum GameState
    {
        Menu, MultiplayerMenu, SinglePlayerMenu,ChoseVect,ChosePower,Rolling,Pause,GameOver,Win, Exit,
        
    }
    public enum GameType
    {
        None,Multiplayer, SinglePlayer
    }
    public enum Turn
    {
        Player1, Player2
    }
    public enum Animation
    {
        None, Mario, lunk , Dino
    }
    public class Game1 : Game
    {
        

        private List<string> gmenuList = new List<string>() {
                "SinglePlayer","Multiplayer","CustomizeMusic","Exit"
            };
        private List<string> singlePlList = new List<string>() {
                "Play","Back","Exit"
            };
        private List<string> multiPlList = new List<string>() {
                "Connect","Back","Exit"
            };
        private List<string> Gpause = new List<string>()
        {
            "Continue","Back","Exit"
        };
        private List<string> GEnd = new List<string>()
        {
            "Continue","Exit"
        };
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hud hud = new Hud();
        public static string  path;
        GraphicsDeviceManager graphics;
        KeyboardState keyboardState;
        KeyboardState prevState;
        Ball ball;
        public static GameState gameState = GameState.Menu;
        public static GameState prevGState = GameState.Menu;
        public static GameType gameType = GameType.None;
        public static int Width;
        public static int Height;
        public static Turn turn = Turn.Player1;
        public bool IsWin = false;
        public Global_Menu gmenu = new Global_Menu();
        private Player player;
        public static bool IsMusPlaying;
        public static int score1 = 0;
        public static int score2 = 0;
        BackGround back;
        Menu menu = new Menu();
        public static Hole hole;
        public static Animation animation = Animation.None;
        public static AnimationClass animator = new AnimationClass(0,0);
        
        public string whoWin;


        public int Score(Turn turn)
        {
            if(turn == Turn.Player1)
            {
                return score1;
            }
            else
            {
                return score2;
            }
            
        }
       

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Width = 1000;
            Height = 1000;
            graphics.PreferredBackBufferHeight = Height;
            graphics.PreferredBackBufferWidth = Width;
            ball = new Ball(new Vector2(250, 800), Width , Height );
            hole = new Hole(Width, Height);
            back = new BackGround(Width, Height);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ball.LoadContent(Content);
            gmenu.LoadContent(Content);
            player = new Player() ;
            player.LoadContent(Content);
            hud.LoadContent(Content);
            hole.LoadContent(Content);
            back.LoadContent(Content);
            animator.LoadContentMario(Content);

            
                // TODO: use this.Content to load your game content here
    }
        public static void PlayMus(string path)
        {
           //MediaPlayer.Play(Song.FromUri(Path.GetFileNameWithoutExtension(path), new Uri(path)));
            IsMusPlaying = true;
        }
        public static void PauseMus()
        {
            MediaPlayer.Stop();
            IsMusPlaying = false;
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameState = GameState.Pause;
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.RightAlt)&&IsMusPlaying==true&&keyboardState!=prevState)
            {
                
                PauseMus();

            }
            else if (keyboardState.IsKeyDown(Keys.RightAlt) && IsMusPlaying ==false && keyboardState != prevState)
            {
                PlayMus(path);
            }
            
            switch (gameState)
            {
                
                case GameState.Menu:
                    UpdateMenu(gameTime,gmenuList);
                    score1 = 0;
                    score2 = 0;
                    break;
                case GameState.SinglePlayerMenu:
                    gmenu.Update(gameTime,singlePlList);
                    
                    
                    break;
                case GameState.MultiplayerMenu:
                    gmenu.Update(gameTime,multiPlList);
                  
                    break;
                case GameState.ChoseVect:
                    PlayMus(path);

                    if(animation == Animation.None)
                    {
                        UpdateAngArrow(gameTime);
                        if (keyboardState.IsKeyDown(Keys.Space))
                        {
                            Game1.gameState = GameState.ChosePower;
                        }
                        if (keyboardState.IsKeyDown(Keys.Escape) && keyboardState != prevState)
                        {

                            Game1.gameState = GameState.Pause;
                            Game1.prevGState = GameState.ChoseVect;

                        }
                    }
                    else
                    {
                        animator.Update(Content);
                    }
                    
                    hud.Update(gameTime);
                    break;
                case GameState.Pause:
                    if (keyboardState.IsKeyDown(Keys.Escape) && keyboardState != prevState)
                    {

                        Game1.gameState = prevGState;
                    }
                    gmenu.Update(gameTime, multiPlList);
                    break;
                case GameState.ChosePower:
                    hud.Update(gameTime);

                    
                    

                    player.Update(gameTime);

                    if (keyboardState.IsKeyDown(Keys.Space) && keyboardState!= prevState)
                    {
                        Game1.gameState = GameState.Rolling;
                        ball.SetSpeed((float)(Math.PI -player.angle),player.force);
                    }
                    if (keyboardState.IsKeyDown(Keys.Escape) && keyboardState != prevState)
                    {

                        Game1.gameState = GameState.Pause;
                        Game1.prevGState = GameState.ChoseVect;

                    }
                    break;
                case GameState.Rolling:
                    hud.Update(gameTime);

                    PlayMus(path);
                    player.rect.Width = player.texture.Width/ 2;
                    ball.Update(Content,hole);
                    player.rect.Width = player.texture.Width;
                    break;
                case GameState.GameOver:
                    gmenu.Update(gameTime, GEnd);
                    break;
                case GameState.Win:
                    gmenu.Update(gameTime, GEnd);
                    hud.Update(gameTime);
                    score1 = 0;
                    score2 = 0;
                    animator.LoadContentMario(Content);
                    break;
                case GameState.Exit:
                    this.Exit();
                    break;
               

            }
            prevState = keyboardState;
            base.Update(gameTime);
        }
        private void UpdateAngArrow(GameTime gameTime)
        {
            player.UpdateAngle(gameTime);
        }
        private void UpdateForcing(GameTime gameTime)
        {
            player.Update(gameTime);
        }
        private void UpdateMenu(GameTime gameTime, List<string> blist)
        {
            gmenu.Update(gameTime, blist);
        }
        private void UpdateHUD(GameTime gameTime)
        {
            hud.Update(gameTime);
        }




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            
            _spriteBatch.Begin();
            {
               
                switch (gameState)
                {
                    case GameState.Menu:
                        back.Draw(_spriteBatch );
                        DrawGlobalMenu(_spriteBatch,gmenuList);
                        break;
                    case GameState.SinglePlayerMenu:
                        DrawBack(_spriteBatch);
                        gmenu.Draw(_spriteBatch, singlePlList);
                        break;
                    case GameState.MultiplayerMenu:
                        DrawBack(_spriteBatch);
                        gmenu.Draw(_spriteBatch, multiPlList);
                        break;
                    case GameState.ChoseVect:
                        DrawBack(_spriteBatch);
                        if(animation == Animation.Mario || animation == Animation.Dino)
                        {
                            
                            animator.Draw(_spriteBatch);
                        }
                        else if(animation == Animation.lunk)
                        {
                            hole.Draw(_spriteBatch);
                        }
                        else
                        {
                            hole.SetTexture(Content, "hole(0)");
                            player.SetPosition((int)ball.position.X + ball.boundingBox.Height / 2, (int)ball.position.Y + ball.boundingBox.Height / 2, 100, 50);
                            hole.Draw(_spriteBatch);
                            DrawAngling(_spriteBatch);
                            ball.Draw(_spriteBatch);

                        }
                        DrawHud(_spriteBatch);
                    
                        break;
                    case GameState.ChosePower:
                        
                        DrawBack(_spriteBatch);
                        hole.Draw(_spriteBatch);
                        DrawAngling(_spriteBatch);
                     
                        ball.Draw(_spriteBatch);
                        DrawHud(_spriteBatch);
                        //hole.Draw(_spriteBatch);

                        //player.DrawAngle(_spriteBatch);
                        hud.Draw(_spriteBatch);
                        break;
                    
                    case GameState.Pause:
                        DrawBack(_spriteBatch);
                        hole.Draw(_spriteBatch);
                        ball.Draw(_spriteBatch);
                        DrawGlobalMenu(_spriteBatch, Gpause);
                        
                        break;
                    case GameState.Rolling:
                        DrawBack(_spriteBatch);
                        hole.Draw(_spriteBatch);
                        ball.Draw(_spriteBatch);
                        break;
                    case GameState.GameOver:
                        gmenu.Draw(_spriteBatch, GEnd);
                        break;
                    case GameState.Win:
                        gmenu.Draw(_spriteBatch, GEnd);
                        break;
                }
            }
            
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawGlobalMenu(SpriteBatch spriteBatch, List<string> blist)
        {

            gmenu.Draw(spriteBatch, blist);
        }
        private void DrawAngling(SpriteBatch spriteBatch)
        {

            player.DrawAngle(spriteBatch);
        }
        private void DrawHud(SpriteBatch spriteBatch)
        {
            hud.Draw(spriteBatch);
        }
        private void DrawBack(SpriteBatch spriteBatch)
        {
            back.Draw(spriteBatch);
        }



    }
}
