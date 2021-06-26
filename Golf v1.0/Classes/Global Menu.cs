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
        Uri selectSound = new Uri(@"C:\Users\romah\OneDrive\Документы\GitHub\Golf-v1.0\Golf v1.0\Content\MenuContent\3a7ee48dc00822f.mp3");
        Uri changeSound = new Uri(@"C:\Users\romah\OneDrive\Документы\GitHub\Golf-v1.0\Golf v1.0\Content\MenuContent\button-37a.mp3");
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
            
        }
        public void Update(GameTime gameTime,List<string> blist)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) && keyboardState!=prevState)
            {
                if (selected < blist.Count - 1)
                {
                    selected++;
                    MediaPlayer.Play(Song.FromUri("",changeSound));
                }
            }

            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W) && keyboardState!=prevState)
            {
                if (selected > 0)
                {
                    selected--;
                    MediaPlayer.Play(Song.FromUri("", changeSound));
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

                            MediaPlayer.Play(Song.FromUri( " ",selectSound));
                            break;
                        case 1:             // Info
                            Game1.gameState = GameState.MultiplayerMenu;
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            break;
                        case 2:             // Exit
                            PlayMP3();
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            break;
                           
                        case 3:
                            Game1.gameState = GameState.Exit;
                            //MediaPlayer.Play(Song.FromUri(Path.GetFileNameWithoutExtension(@"C:\Users\romah\OneDrive\Документы\GitHub\Golf-v1.0\Golf v1.0\Content\MenuContent\3a7ee48dc00822f.mp3"), new Uri(@"C:\Users\romah\OneDrive\Документы\GitHub\Golf-v1.0\Golf v1.0\Content\MenuContent\3a7ee48dc00822f.mp3")));
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
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            break;    
                        case 1:             
                            Game1.gameState = GameState.Menu;
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            break;
                        case 2:
                            Game1.gameState = GameState.Exit;
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
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
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            break;
                        case 1:
                            Game1.gameState = GameState.Menu;
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            break;
                        case 2:
                            Game1.gameState = GameState.Exit;
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
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
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            break;
                        case 1:
                            if (Game1.gameType == GameType.SinglePlayer)
                            {
                                Game1.gameState = GameState.SinglePlayerMenu;
                                MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            }
                            else if (Game1.gameType == GameType.Multiplayer)
                            {
                                Game1.gameState = GameState.MultiplayerMenu;
                                MediaPlayer.Play(Song.FromUri(" ", selectSound));
                            }
                            break;
                        case 2:
                            Game1.gameState = GameState.Exit;
                            MediaPlayer.Play(Song.FromUri(" ", selectSound));
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
                    new Vector2(Game1.Width/2-50, Game1.Height / 2 - 50+(30 * (i))), color
                );
            }
            if (Game1.gameState == GameState.Menu)
            {
                spriteBatch.Draw(menuTexture,new Rectangle(Game1.Width/2-110,Game1.Height/2-200,menuTexture.Width*2,menuTexture.Height*2),Color.White);
            }
            else if (Game1.gameState == GameState.SinglePlayerMenu)
            {
                spriteBatch.Draw(singlePlTexture, new Rectangle(Game1.Width/ 2 - 185, Game1.Height / 2 - 200, singlePlTexture.Width * 2, singlePlTexture.Height * 2) , Color.White);
            }
            else if (Game1.gameState == GameState.MultiplayerMenu)
            {
                spriteBatch.Draw(mpTExture, new Rectangle(Game1.Width / 2 - 155, Game1.Height / 2 - 200, mpTExture.Width * 2, mpTExture.Height * 2), Color.White);
            }
            else if (Game1.gameState == GameState.Pause)
            {
                spriteBatch.Draw(PauseTexture, new Rectangle(Game1.Width / 2 - 120, Game1.Height / 2 - 200,PauseTexture.Width * 2, PauseTexture.Height * 2), Color.White);
            }
            else if (Game1.gameState == GameState.GameOver)
            {

            }
        }
        private void PlayMP3()
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
                    Game1.PlayMus(filePath);

                }
            }
        }
      
    }
}
