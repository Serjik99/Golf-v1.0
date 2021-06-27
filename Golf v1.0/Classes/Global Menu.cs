using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Golf_v1_0;
using System.Windows.Forms;

using MyLibrary;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace Golf_v1_0

{
    public class Global_Menu
    {
        KeyboardState keyboardState;
        KeyboardState prevState;
        SpriteFont spriteFont;
        Texture2D menuTexture;
        Texture2D singlePlTexture;
        Texture2D mpTExture;
        Texture2D PauseTexture;
        Texture2D WinTexture;
        Texture2D LoseTexture;
        private Song changeSound;
        private Song selectSound;
        int selected;
        public Global_Menu()
        {
            selected = 0;
            

        }
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("GameFont");
            menuTexture = content.Load<Texture2D>(@"MenuContent\MainMeny");
            singlePlTexture = content.Load<Texture2D>(@"MenuContent\SinglePlayer");
            mpTExture = content.Load<Texture2D>(@"MenuContent\MultiPlayer");
            PauseTexture = content.Load<Texture2D>(@"MenuContent\Pause");
            LoseTexture = content.Load<Texture2D>("Lose1");
            WinTexture = content.Load<Texture2D>("WinPart1");
            changeSound = content.Load<Song>(@"MenuContent\button-37a");
            selectSound = content.Load<Song>(@"MenuContent\3a7ee48dc00822f");
            
        }
        public void Update(GameTime gameTime,List<string> blist)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) && keyboardState!=prevState)
            {
                if (selected < blist.Count - 1)
                {
                    MediaPlayer.Play(changeSound);
                    selected++;
                    
                }
            }

            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W) && keyboardState!=prevState)
            {
                if (selected > 0)
                {
                    MediaPlayer.Play(changeSound);
                    selected--;
                    
                }
            }
            if (Game1.gameState == GameState.Menu)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) && keyboardState != prevState)
                { 
                    switch (selected)
                    {
                        case 0:             // Start Play
                            Game1.gameState = GameState.SinglePlayerMenu;

                            MediaPlayer.Play(selectSound);
                            break;
                        case 1:             // Info
                            Game1.gameState = GameState.MultiplayerMenu;
                            MediaPlayer.Play(selectSound);
                            break;
                        case 2:             // Exit
                            
                            MediaPlayer.Play(selectSound);
                            SetMP3();
                            break;
                           
                        case 3:

                            Game1.gameState = GameState.Exit;
                            
                            break;
                    }
                }
                prevState = keyboardState;
            }
            else if (Game1.gameState == GameState.SinglePlayerMenu && keyboardState != prevState)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    switch (selected)
                    {
                        case 0:
                            Game1.gameType = GameType.SinglePlayer;
                            Game1.gameState = GameState.ChoseVect;
                            MediaPlayer.Play(selectSound);
                            break;    
                        case 1:             
                            Game1.gameState = GameState.Menu;
                            MediaPlayer.Play(selectSound);
                            break;
                        case 2:
                            MediaPlayer.Play(selectSound);
                            Game1.gameState = GameState.Exit;
                            
                            break;
                    }
                }
                
            }
            else if (Game1.gameState == GameState.MultiplayerMenu && keyboardState!=prevState)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    switch (selected)
                    {
                      
                        case 0:
                            Game1.gameType = GameType.Multiplayer;// Info
                            Game1.gameState = GameState.ChoseVect;
                            MediaPlayer.Play(selectSound);
                            break;
                        case 1:
                            Game1.gameState = GameState.Menu;
                            MediaPlayer.Play(selectSound);
                            break;
                        case 2:
                            MediaPlayer.Play(selectSound);
                            Game1.gameState = GameState.Exit;
                            
                            break;
                    }
                }
                
            }
            else if (Game1.gameState == GameState.Pause && keyboardState != prevState)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    switch (selected)
                    {

                        case 0:
                            
                            Game1.gameState = Game1.prevGState;
                            MediaPlayer.Play(selectSound);
                            break;
                        case 1:
                            if (Game1.gameType == GameType.SinglePlayer)
                            {
                                MediaPlayer.Play(selectSound);
                                Game1.gameState = GameState.SinglePlayerMenu;
                                
                            }
                            else if (Game1.gameType == GameType.Multiplayer)
                            {
                                MediaPlayer.Play(selectSound);
                                Game1.gameState = GameState.MultiplayerMenu;

                            }
                            break;
                        case 2:
                            MediaPlayer.Play(selectSound);
                            Game1.gameState = GameState.Exit;
                            
                            break;
                      
                    }
                }

            }
            else if ((Game1.gameState == GameState.Win || Game1.gameState == GameState.GameOver) && keyboardState != prevState)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    switch (selected)
                    {

                        case 0:

                            if (Game1.gameType == GameType.SinglePlayer)
                            {
                                MediaPlayer.Play(selectSound);
                                Game1.gameState = GameState.SinglePlayerMenu;

                            }
                            else if (Game1.gameType == GameType.Multiplayer)
                            {
                                MediaPlayer.Play(selectSound);
                                Game1.gameState = GameState.MultiplayerMenu;
                            }
                                break;
                        case 1:
                            MediaPlayer.Play(selectSound);
                            Game1.gameState = GameState.Exit;

                            break;
                        

                    }
                }

            }
            prevState = keyboardState;
           
        }



        public void Draw(SpriteBatch spriteBatch,List<string> blist)
        {
            
            Color color;

            for (int i = 0; i < blist.Count; i++)
            {
                if (selected == i)
                {
                    color = Color.Yellow;
                }
                else
                {
                    color = Color.White;
                }

                spriteBatch.DrawString
                (
                    spriteFont, blist[i],
                    new Vector2(Game1.Width/2- blist.Count * 30, Game1.Height / 2 - blist.Count*30 +(30 * (i))), color
                );
            }
            if (Game1.gameState == GameState.Menu)
            {
                spriteBatch.Draw(menuTexture,new Rectangle(Game1.Width/2-menuTexture.Width,Game1.Height/2-menuTexture.Height*6,menuTexture.Width*2,menuTexture.Height*2),Color.White);
            }
            else if (Game1.gameState == GameState.SinglePlayerMenu)
            {
                spriteBatch.Draw(singlePlTexture, new Rectangle(Game1.Width / 2 - singlePlTexture.Width, Game1.Height / 2 - singlePlTexture.Height * 6, singlePlTexture.Width * 2, singlePlTexture.Height * 2), Color.White);
            }
            else if (Game1.gameState == GameState.MultiplayerMenu)
            {
                spriteBatch.Draw(mpTExture, new Rectangle(Game1.Width / 2 - mpTExture.Width, Game1.Height / 2 - mpTExture.Height * 6, mpTExture.Width * 2, mpTExture.Height * 2), Color.White);
            }
            else if (Game1.gameState == GameState.Pause)
            {
                spriteBatch.Draw(PauseTexture, new Rectangle(Game1.Width / 2 - PauseTexture.Width, Game1.Height / 2 - PauseTexture.Height * 6, PauseTexture.Width * 2, PauseTexture.Height * 2), Color.White);
            }
            else if (Game1.gameState == GameState.GameOver)
            {
                spriteBatch.Draw(LoseTexture, new Rectangle(Game1.Width / 2 - LoseTexture.Width, Game1.Height / 2 - LoseTexture.Height * 6, LoseTexture.Width * 2, LoseTexture.Height * 2), Color.White);
            }
            else if (Game1.gameState == GameState.Win)
            {
                spriteBatch.Draw(WinTexture, new Rectangle(Game1.Width / 2 - WinTexture.Width, Game1.Height / 2 - WinTexture.Height * 6, WinTexture.Width * 2, WinTexture.Height * 2), Color.White);
            }
        }
        private void SetMP3()
        {
            
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;


                    Game1.path = filePath;
                   

                }
            }
        }
      
    }
}
