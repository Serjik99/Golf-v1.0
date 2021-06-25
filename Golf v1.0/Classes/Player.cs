﻿using Microsoft.Xna.Framework;
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
        static int x;
        static int y;
        int width;
        Rectangle rect;
        float angle = (float)Math.PI * 2;
        KeyboardState keyboardState;
        KeyboardState prevState;
        Vector2 direction;
        Vector2 position;
        Texture2D texture;
        
        public Player(int x,int y,int w,int h)
        {
            rect = new Rectangle(x,y, w, h);
            width = rect.Width;
        }
        public void LoadContent(ContentManager content)
        {

            texture = content.Load<Texture2D>("arrow_outline_red_left_1");
        }
        public void DrawAngle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,rect , null, Color.White, angle, new Vector2(texture.Width,texture.Height/2), SpriteEffects.None, 1f);
           
        }
       
        public void UpdateAngle(GameTime gameTime)
        {         
                angle += 0.01f;
        }
        double ticks = 0;
        
        public void Update(GameTime gametime)
        {
            
            double c = (Math.Sin(ticks / 10)+1) *(width/2);
            rect.Width = width +(int)c;
            ticks++;
        }
    }
}
