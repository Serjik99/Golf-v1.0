using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Golf_v1._0;

namespace Golf_v1_0
{
    class Menu
    {
        KeyboardState keyboardState;
        SpriteFont spriteFont;
        List<string> buttonList;
        int selected;
        public Menu()
        {
            buttonList = new List<string>();
            selected = 0;
            buttonList.Add("SinglePlayer");
            buttonList.Add("Multiplayer");
            buttonList.Add("Info");
            buttonList.Add("Exit");

        }
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("GameFont");
        }
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyDown(Keys.S))
            {
                if (selected < buttonList.Count - 1)
                {
                    selected++;
                }
            }

            if (keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyDown(Keys.W))
            {
                if (selected > 0)
                {
                    selected--;
                }
            }

            // Активация выбора в меню
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                switch (selected)
                {
                    case 0:             // Start Play
                        Game1.gameState = GameState.Playing;
                        break;
                    case 1:             // Info
                        Game1.gameState = GameState.Info;
                        break;
                    case 2:             // Exit
                        Game1.gameState = GameState.Exit;
                        break;
                }
            }

           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;

            for (int i = 0; i < buttonList.Count; i++)
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
                    spriteFont, buttonList[i],
                    new Vector2(100, 20 * i), color
                );
            }

        }
    }
}
