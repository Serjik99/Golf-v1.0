using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;


namespace Golf_v1_0
{
    class Ball
    {
        double force;
        Vector2 speed;
        double acceleration = 0.1;
        public Vector2 position;
        
        public Texture2D texture;

        double timeTick;
        Rectangle boundingBox;
        Rectangle[] colisions;
        int textureNumber = 0;
        public Vector2 Position
        {
            get { return position; }
        }
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }

        public Ball(Vector2 position)
        {
            this.position = position;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load <Texture2D>("Ball(0)");
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, Color.White);
        }
        public void Update()
        {
            if((position + speed).X <= 500 - boundingBox.Width && (position + speed).Y <= 1000 - boundingBox.Height && (position + speed).X >= 0 && (position + speed).Y >= 0)
            {
                position += speed;
                boundingBox.X = (int)position.X;
                boundingBox.Y = (int)position.Y;
            }
            if((position + speed).X > 500 - boundingBox.Width || (position + speed).X < 0)
            {
                speed.X = -speed.X;
            }
            if((position + speed).Y > 1000 - boundingBox.Height || (position + speed).Y < 0)
            {
                speed.Y = -speed.Y;
            }
            if(speed.Y < 0)
            {
                speed.Y += (float)acceleration;
            }
            else if(speed.Y > 0)
            {
                speed.Y -= (float)acceleration;
            }
            if(speed.X < 0)
            {
                speed.X += (float)acceleration;
            }
            else if(speed.X > 0)
            {
                speed.X -= (float)acceleration;
            }
            if(Math.Abs(speed.X) < 0.1 )
            {
                speed.X = 0;
            }
            if(Math.Abs(speed.Y) < 0.1)
            {
                speed.Y = 0;
            }
            //foreach()
            //{

            //}
            
        }
        public void SetSpeed(float angle , double force)
        {
            speed.X = (float)(Math.Sin(angle) * force);
            speed.Y = (float)(Math.Cos(angle) * force);
        }
        
    }   
}
