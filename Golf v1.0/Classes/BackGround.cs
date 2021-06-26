using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
namespace Golf_v1_0
{
    class BackGround
    {
        public Texture2D texture;
        int x;
        int y;

        public BackGround(int x,  int y)
        {
            this.x = x;
            this.y = y;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("backGround");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < (y / texture.Height) + 2; i++)
            {
                for (int j = 0; j < (x / texture.Width) + 2; j++)
                {
                    spriteBatch.Draw(texture, new Rectangle( j  * texture.Width, i * texture.Height, texture.Width, texture.Height), Color.White);
                }
            }
            
        }
    }
}
