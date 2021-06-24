using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;


namespace Golf_v1._0
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
        Rectangle f = new Rectangle(100, 100, 20, 20);
        public Player()
        {

        }
        public void LoadContent(ContentManager content)
        {

            texture = content.Load<Texture2D>("arrow_outline_red_left_1");
        }
        public void DrawAngle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,f , null, Color.White, angle, new Vector2(texture.Width,texture.Height/2), SpriteEffects.None, 1f);
        }
       
        public void UpdateAngle(GameTime gameTime)
        {         
                angle += 0.01f;
        }
        double ticks = 0;
        public void Update(GameTime gametime)
        {
            double c = (Math.Sin(ticks / 10)+1) * 10;
            f.Width = 20 +(int)c;
            ticks++;
        }
    }
}
