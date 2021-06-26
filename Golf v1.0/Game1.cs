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
    //Random random = new Random();
    public enum GameState
    {
        Menu, MultiplayerMenu, SinglePlayerMenu, ChoseVect, ChosePower, Rolling, GameOver, Exit, Win, Lose, Draw

    }
    public enum GameType
    {
        None, Multiplayer, SinglePlayer
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

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hud hud = new Hud();
        public static string path;
        GraphicsDeviceManager graphics;
        Ball ball;
        public static GameState gameState = GameState.Menu;
        public static GameType gameType = GameType.None;
        public Global_Menu gmenu = new Global_Menu();
        private Player player;
        public static bool IsMusPlaying;
        Hole lunk = new Hole(new Vector2(100, 100));
        BackGround backGround;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;

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
            ball = new Ball(new Vector2(100, 800), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            lunk.LoadContent(Content);
            ball.SetSpeed((float)Math.PI / 4, 30);
            ball.LoadContent(Content);
            gmenu.LoadContent(Content);
            player = new Player((int)ball.position.X + ball.texture.Width, (int)ball.position.Y + ball.texture.Height);
            player.LoadContent(Content);
            hud.LoadContent(Content);
            backGround = new BackGround(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            backGround.LoadContent(Content);


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            KeyboardState prevState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.RightAlt) && IsMusPlaying == true)
            {
                PauseMus();

                switch (gameState)
                {
                    case GameState.Menu:
                        UpdateMenu(gameTime, gmenuList);
                        break;
                    case GameState.SinglePlayerMenu:
                        UpdateMenu(gameTime, singlePlList);
                        break;
                    case GameState.MultiplayerMenu:
                        UpdateMenu(gameTime, multiPlList);
                        break;
                    case GameState.ChoseVect:
                        UpdateAngArrow(gameTime);
                        break;
                    case GameState.ChosePower:
                        break;
                    case GameState.Exit:
                        this.Exit();
                        break;
                    case GameState.Rolling:
                        ball.Update(Content, lunk);
                        break;

                }

                base.Update(gameTime);
            }
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
                        DrawGlobalMenu(_spriteBatch, gmenuList);
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
            backGround.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);
            lunk.Draw(_spriteBatch);
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
    }
}
