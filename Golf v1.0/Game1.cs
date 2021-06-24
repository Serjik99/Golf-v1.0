using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Golf_v1._0
{
    public enum GameState
    {
        Menu,Multiplayer,SinglePlayer,ChoseVect,ChosePower,Rolling, Info, GameOver, Exit
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        GraphicsDeviceManager graphics;
        Ball ball = new Ball(new Vector2(250, 800));
        public static GameState gameState = GameState.Menu;
        public Global_Menu gmenu = new Global_Menu();
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
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.Menu:
                    UpdateMenu(gameTime);
                    break;
                case GameState.Info:
                    
                    break;
                
            }

            base.Update(gameTime);
        }
        private void UpdateMenu(GameTime gameTime)
        {
            gmenu.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

          
            _spriteBatch.Begin();
            {
               
                switch (gameState)
                {
                    case GameState.Menu:
                        DrawGlobalMenu(_spriteBatch);
                        break;
                    case GameState.Info:
                       
                        break;
                    case GameState.SinglePlayer:
                        
                        break;
                    case GameState.GameOver:
                        
                        break;
                }
            }

            ball.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawGlobalMenu(SpriteBatch spriteBatch)
        {
            gmenu.Draw(spriteBatch);
        }
    }
}
