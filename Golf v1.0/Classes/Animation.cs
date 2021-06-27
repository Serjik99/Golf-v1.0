using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Golf_v1_0
{
    public class AnimationClass
    {
        Random random = new Random();
        Vector2 position = new Vector2();
        int sprite = 0;
        int iter = 0;
        Texture2D texture;
        Rectangle rectangle = new Rectangle(336, 296, 368, 408);

        public AnimationClass(int x , int y)
        {
            position.X = x;
            position.Y = y;
        }
        public void SetPosition(int x, int y , int width , int height)
        {

            rectangle = new Rectangle(x, y, width, height);
        }
        public void Update(ContentManager content)
        {
            if(Game1.animation == Animation.Mario)
            {
                iter += 1;
                if(iter % 10 == 0)
                {
                    sprite++;
                    texture = content.Load<Texture2D>("Animations/mario" + sprite);
                }
                if (sprite == 8)
                {
                    Game1.animation = Animation.None;
                    iter = 0;
                    sprite = 0;
                }
            }
            else if(Game1.animation == Animation.lunk)
            {
                iter += 1;
                if (iter % 10 == 0)
                {

                    sprite++;
                    if (sprite == 6)
                    {
                        if(Game1.turn == Turn.Player1)
                        {
                            Game1.animation = Animation.Mario;
                        }
                        else
                        {
                            Game1.animation = Animation.Dino;
                        }
                        iter = 0;
                        sprite = 0;
                        Game1.hole.SetPosition(new Vector2(random.Next(50, 850), random.Next(76, 300)));
                        
                    }
                    else
                    {
                        Game1.hole.SetRectangle(100, 100);
                        Game1.hole.SetTexture(content, "BallInHole/ballinhole(" + sprite + ")");
                    }
                    
                }
            }
            else if(Game1.animation == Animation.Dino)
            {
                iter += 1;
                if (iter % 10 == 0)
                {
                    sprite++;
                    texture = content.Load<Texture2D>("Animations/dino" + sprite);
                }
                if (sprite == 8)
                {
                    Game1.animation = Animation.None;
                    iter = 0;
                    sprite = 0;
                }
            }
            

        }

        public void LoadContentMario(ContentManager content)
        {
            texture = content.Load<Texture2D>("Animations/mario0");
        }
        public void LoadContentDino(ContentManager content)
        {
            texture = content.Load<Texture2D>("Animations/dino0");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
