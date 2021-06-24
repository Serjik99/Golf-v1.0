using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Golf_v1._0;
using System.Windows.Forms;

namespace Golf_v1._0
{
    public class Global_Menu
    {
        KeyboardState keyboardState;
        KeyboardState prevState;
        SpriteFont spriteFont;
        
        int selected;
        public Global_Menu()
        {
            selected = 0;
            

        }
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("GameFont");
        }
        public void Update(GameTime gameTime,List<string> blist)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) && keyboardState!=prevState)
            {
                if (selected < blist.Count - 1)
                {
                    selected++;
                }
            }

            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W) && keyboardState!=prevState)
            {
                if (selected > 0)
                {
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
                            break;
                        case 1:             // Info
                            Game1.gameState = GameState.MultiplayerMenu;
                            break;
                        case 2:             // Exit
                            PlayMP3();
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
                            break;    
                        case 1:             
                            Game1.gameState = GameState.Exit;
                            break;
                    }
                }
                prevState = keyboardState;
            }
            else if (Game1.gameState == GameState.MultiplayerMenu && keyboardState!=prevState)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    switch (selected)
                    {
                      
                        case 0:
                            Game1.gameType = GameType.SinglePlayer;// Info
                            Game1.gameState = GameState.ChoseVect;
                            break;
                        case 1:             // Exit
                            Game1.gameState = GameState.Exit;
                            break;
                    }
                }
                prevState = keyboardState;
            }
           

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
                    new Vector2(100, 20 * i), color
                );
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
