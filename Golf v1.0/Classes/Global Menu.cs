using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Golf_v1._0;


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

            if (keyboardState.IsKeyDown(Keys.S) && keyboardState!=prevState)
            {
                if (selected < blist.Count - 1)
                {
                    selected++;
                }
            }

            if (keyboardState.IsKeyDown(Keys.W) && keyboardState!=prevState)
            {
                if (selected > 0)
                {
                    selected--;
                }
            }
            if (Game1.gameState == GameState.Menu)
            {
                if (keyboardState.IsKeyDown(Keys.Enter) && keyboardState != prevState)
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
                            Game1.gameState = GameState.Exit;
                            break;
                    }
                }
                prevState = keyboardState;
            }
            else if (Game1.gameState == GameState.SinglePlayerMenu && keyboardState != prevState)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    switch (selected)
                    {
                        case 0:             
                            Game1.gameState = GameState.SinglePlayer;
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
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    switch (selected)
                    {
                      
                        case 0:             // Info
                            Game1.gameState = GameState.Multiplayer;
                            break;
                        case 1:             // Exit
                            Game1.gameState = GameState.Exit;
                            break;
                    }
                }
                prevState = keyboardState;
            }
            // Активация выбора в меню


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
      
    }
}
