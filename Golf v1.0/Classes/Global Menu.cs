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
        List<string> buttonList;
        int selected;
        public Global_Menu()
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

            if (keyboardState.IsKeyDown(Keys.S) && keyboardState!=prevState)
            {
                if (selected < buttonList.Count - 1)
                {
                    selected++;
                }
            }

            if (keyboardState.IsKeyDown(Keys.W) && keyboardState != prevState)
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
                        Game1.gameState = GameState.SinglePlayer ;
                        break;
                    case 1:             // Info
                        Game1.gameState = GameState.Multiplayer;
                        break;
                    case 2:             // Exit
                        Game1.gameState = GameState.Exit;
                        break;
                }
            }
            prevState = keyboardState;

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
