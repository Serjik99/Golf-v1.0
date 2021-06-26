using Microsoft.Xna.Framework;
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
            "MainMenu","Continue","Exit"
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
        public Global_Menu gmenu = new Global_Menu();
        private Player player;
        public static bool IsMusPlaying;
        private BackGround back;
        Menu menu = new Menu();
        Hole hole = new Hole(new Vector2(10,10));


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
            back = new BackGround(Width,Height);
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
            back.LoadContent(Content);
            
                // TODO: use this.Content to load your game content here
    }
        public static void PlayMus(string path)
        {
           MediaPlayer.Play(Song.FromUri(Path.GetFileNameWithoutExtension(path), new Uri(path)));
            IsMusPlaying = true;
        }
        public static void PauseMus()
        {
            MediaPlayer.Stop();
            IsMusPlaying = false;
        }
        
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //  Exit();
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

                    gmenu.Update(gameTime,gmenuList);
                    break;
                case GameState.SinglePlayerMenu:
                    gmenu.Update(gameTime,singlePlList);
                    
                    break;
                case GameState.MultiplayerMenu:
                    gmenu.Update(gameTime,multiPlList);
                  
                    break;
                case GameState.ChoseVect:
                    hud.Update(gameTime);
                    player.UpdateAngle(gameTime);
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        Game1.gameState = GameState.ChosePower;
                    }
                    if (keyboardState.IsKeyDown(Keys.Escape) && keyboardState != prevState)
                    {

                        Game1.gameState = GameState.Pause;
                        Game1.prevGState = GameState.ChoseVect;
                        
                    }
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
                    player.rect.Width = player.texture.Width/ 2;
                    ball.Update(Content,hole);
                    player.rect.Width = player.texture.Width;
                    break;
                case GameState.GameOver:
                    gmenu.Update(gameTime, GEnd);
                    break;
                case GameState.Win:
                    gmenu.Update(gameTime, GEnd);
                    break;
                case GameState.Exit:
                    this.Exit();
                    
                    break;

            }
            prevState = keyboardState;
            base.Update(gameTime);
        }

       
       
      
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            
            _spriteBatch.Begin();
            {
               
                switch (gameState)
                {
                    case GameState.Menu:
                        gmenu.Draw(_spriteBatch,gmenuList);
                        break;
                    case GameState.SinglePlayerMenu:
                        gmenu.Draw(_spriteBatch, singlePlList);
                        break;
                    case GameState.MultiplayerMenu:
                        gmenu.Draw(_spriteBatch, multiPlList);
                        break;
                    case GameState.ChoseVect:
                        back.Draw(_spriteBatch);
                        player.SetPosition((int)ball.position.X + ball.boundingBox.Height / 2, (int)ball.position.Y + ball.boundingBox.Height / 2, 100, 50);
                        player.DrawAngle(_spriteBatch);
                        hud.Draw(_spriteBatch);
                        break;
                    case GameState.ChosePower:

                        back.Draw(_spriteBatch);

                        player.DrawAngle(_spriteBatch);
                        hud.Draw(_spriteBatch);
                        break;
                    case GameState.Pause:
                        back.Draw(_spriteBatch);
                        gmenu.Draw(_spriteBatch, Gpause);
                        
                        break;
                    case GameState.Rolling:
                        back.Draw(_spriteBatch);
                        break;
                    case GameState.GameOver:
                        gmenu.Draw(_spriteBatch, GEnd);
                        break;
                    case GameState.Win:
                        gmenu.Draw(_spriteBatch, GEnd);
                        break;
                }
            }
            
            ball.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
      
     
        
        
    }
}
