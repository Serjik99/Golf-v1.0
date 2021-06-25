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
        Menu, MultiplayerMenu, SinglePlayerMenu,ChoseVect,ChosePower,Rolling,Pause,GameOver, Exit,
        
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
        private List<string> pause = new List<string>()
        {
            "Continue","Exit"
        };
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hud hud = new Hud();
        public static string  path;
        GraphicsDeviceManager graphics;
        Ball ball = new Ball(new Vector2(250, 800));
        public static GameState gameState = GameState.Menu;
        public static GameState prevGState = GameState.Menu;
        public static GameType gameType = GameType.None;
        public Global_Menu gmenu = new Global_Menu();
        private Player player;
        public static bool IsMusPlaying;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 500;
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
            player = new Player((int)ball.position.X+ball.texture.Width, (int)ball.position.Y + ball.texture.Height,ball.texture.Width*3,ball.texture.Height*2);
            player.LoadContent(Content);
            hud.LoadContent(Content);

            
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
            KeyboardState keyboardState = Keyboard.GetState();
            KeyboardState prevState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.RightAlt)&&IsMusPlaying==true)
            {
                
                PauseMus();

            }
            else if (keyboardState.IsKeyDown(Keys.RightShift) && IsMusPlaying ==false)
            {
                PlayMus(path);
            }
            
            switch (gameState)
            {
                
                case GameState.Menu:
                    UpdateMenu(gameTime,gmenuList);
                    break;
                case GameState.SinglePlayerMenu:
                    UpdateMenu(gameTime,singlePlList);
                    break;
                case GameState.MultiplayerMenu:
                    UpdateMenu(gameTime,multiPlList);
                    break;
                case GameState.ChoseVect:
                    UpdateAngArrow(gameTime);
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        Game1.gameState = GameState.ChosePower;
                    }
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        prevState = keyboardState;
                        if (keyboardState.IsKeyUp(Keys.Escape)&&keyboardState!=prevState)
                        {
                            Game1.gameState = GameState.Pause;
                            Game1.prevGState = GameState.ChoseVect;
                        }
                    
                    }
                    break;
                case GameState.Pause:
                    UpdateMenu(gameTime,pause);
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                       
                        if (keyboardState.IsKeyUp(Keys.Escape))
                        {
                            Game1.gameState = prevGState;
                        }
                    }
                    
                    break;
                case GameState.ChosePower:
                    UpdateForcing(gameTime);
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        if (keyboardState.IsKeyUp(Keys.Escape))
                        {
                            Game1.gameState = GameState.Pause;
                            Game1.prevGState = GameState.ChosePower;
                        }
                       
                    }
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
        private void UpdateMenu(GameTime gameTime,List<string> blist)
        {
            gmenu.Update(gameTime,blist);
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
                        DrawGlobalMenu(_spriteBatch,gmenuList);
                        break;
                    case GameState.SinglePlayerMenu:
                        DrawGlobalMenu(_spriteBatch, singlePlList);
                        break;
                    case GameState.MultiplayerMenu:
                        DrawGlobalMenu(_spriteBatch, multiPlList);
                        break;
                    case GameState.ChoseVect:
                        DrawAngling(_spriteBatch);
                        DrawHud(_spriteBatch);
                        break;
                    case GameState.ChosePower:
                        DrawAngling(_spriteBatch);
                        DrawHud(_spriteBatch);
                        break;
                    case GameState.GameOver:
                        
                        break;
                }
            }
            
            ball.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawGlobalMenu(SpriteBatch spriteBatch,List<string> blist)
        {
            
            gmenu.Draw(spriteBatch,blist);
        }
        private void DrawAngling(SpriteBatch spriteBatch)
        {

            player.DrawAngle(spriteBatch);
        }
        private void DrawHud(SpriteBatch spriteBatch)
        {
            hud.Draw(spriteBatch);
        }
    }
}
