using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;


namespace Golf_v1_0
{

    class Player
    {
        double timeTick;
        int score;
        int speed;
        float angle = (float)Math.PI * 2;
        KeyboardState keyboardState;
        KeyboardState prevState;
        Vector2 direction;
        Vector2 position;
        Texture2D texture;
        

        public Player()
        {

        }
        public void LoadContent(ContentManager content)
        {

            texture = content.Load<Texture2D>("arrow_outline_red_left_1");
        }
        public void DrawAngle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(100, 100,20, 20), null, Color.White, angle, new Vector2(texture.Width,texture.Height/2), SpriteEffects.None, 1f);
        }
        public void
        public void Update(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.L))
            {
                Game1.gameState = GameState.ChosePower;
            }
            else
            {
                angle += 0.01f;
            }
            
            prevState = keyboardState;
        }
    }
}
