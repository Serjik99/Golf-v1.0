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
        Menu, MultiplayerMenu, SinglePlayerMenu,ChoseVect,ChosePower,Rolling,GameOver, Exit,
        
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
                "Play","Exit"
            };
        private List<string> multiPlList = new List<string>() {
                "Connect","Exit"
            };
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        public static string  path;
        GraphicsDeviceManager graphics;
        Ball ball = new Ball(new Vector2(250, 800));
        public static GameState gameState = GameState.Menu;
        public static GameType gameType = GameType.None;
        public Global_Menu gmenu = new Global_Menu();
        private Player player = new Player();
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
            gameState = GameState.Menu;
            //delete later
            ball.SetSpeed((float)Math.PI / 3, 20);
            ball.LoadContent(Content);
            gmenu.LoadContent(Content);
            player.LoadContent(Content);

          

            // TODO: use this.Content to load your game content here
        }
        public static void PlayMus(string path)
        {
           MediaPlayer.Play(Song.FromUri(Path.GetFileNameWithoutExtension(path), new Uri(path)));
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
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
                    break;
                case GameState.ChosePower:
                    break;
                case GameState.Exit:
                    this.Exit();
                    
                    break;

            }
            
            base.Update(gameTime);
        }
        private void UpdateAngArrow(GameTime gameTime)
        {
            player.Update(gameTime);
        }
        private void UpdateMenu(GameTime gameTime,List<string> blist)
        {
            gmenu.Update(gameTime,blist);
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
                        break;
                    case GameState.ChosePower:
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
    }
}
