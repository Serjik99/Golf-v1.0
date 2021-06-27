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
        int y;
        int x;
        int score;
        
        public Texture2D texture;

        double timeTick;
        public Rectangle boundingBox;
        Rectangle colision;
        int textureNumber = 0;
        public Vector2 Position
        {
            get { return position; }
        }
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }

        public Ball(Vector2 position , int x , int y)
        {
            this.y = y;
            this.x = x;
            this.position = position;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("balltexture(0)");
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 64, 64);
            textureNumber = 0;
            colision = new Rectangle((int)position.X + 10, (int)position.Y + 10, 44, 44);



        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, Color.White);

            //spriteBatch.Draw(texture, colision, Color.Red);

        }
        public void Update(ContentManager content , Hole hole)
        {
            if ((position + speed).X <= x - boundingBox.Width && (position + speed).Y <= y - boundingBox.Height && (position + speed).X >= 0 && (position + speed).Y >= 0)
            {
                position += speed;
                boundingBox.X = (int)position.X;
                boundingBox.Y = (int)position.Y;


                colision.X = (int)position.X + 10;
                colision.Y = (int)position.Y + 10;



                if (speed.X != 0 || speed.Y != 0)
                {
                    if (textureNumber == 10)
                    {
                        texture = content.Load<Texture2D>("Ball(0)");
                    }
                    if (textureNumber >= 20)
                    {
                        texture = content.Load<Texture2D>("balltexture(0)");
                        textureNumber = 0;
                    }
                }
                textureNumber++;
            }
            if ((position + speed).X > x - boundingBox.Width || (position + speed).X < 0)
            {
                speed.X = -speed.X;
            }
            if ((position + speed).Y > y - boundingBox.Height || (position + speed).Y < 0)
            {
                speed.Y = -speed.Y;
            }
            if (speed.Y < 0)
            {
                speed.Y += (float)acceleration;
            }
            else if (speed.Y > 0)
            {
                speed.Y -= (float)acceleration;
            }
            if (speed.X < 0)
            {
                speed.X += (float)acceleration;
            }
            else if (speed.X > 0)
            {
                speed.X -= (float)acceleration;
            }
            if (Math.Abs(speed.X) < 0.1)
            {
                speed.X = 0;
            }
            if (Math.Abs(speed.Y) < 0.1)
            {
                speed.Y = 0;
            }
            for (int i = 0; i < 2; i++)
            {
                if (colision.Intersects(hole.GetColision()) )
                {

                    if(Game1.turn == Turn.Player1)
                    {
                        Game1.score1 += score + 100;
                        if(Game1.score1 >= 1000)
                        {
                            Game1.gameState = GameState.Win;
                        }
                        Game1.turn = Turn.Player2;
                        position = new Vector2(250, 800);
                        boundingBox.X = (int)position.X;
                        boundingBox.Y = (int)position.Y;
                        colision.X = (int)position.X + 10;
                        colision.Y = (int)position.Y + 10;

                    }
                    else if(Game1.turn == Turn.Player2)
                    {
                        Game1.score2 += score + 100;
                        if (Game1.score2 >= 1000)
                        {
                            Game1.gameState = GameState.Win;
                        }
                        Game1.turn = Turn.Player1;
                        position = new Vector2(250, 800);
                        boundingBox.X = (int)position.X;
                        boundingBox.Y = (int)position.Y;
                        colision.X = (int)position.X + 10;
                        colision.Y = (int)position.Y + 10;


                    }
                    Game1.animation = Animation.lunk;
                    //texture = content.Load<Texture2D>("golfBall(0)");
                    //hole.SetTexture(content, "hole_with_ball");
                    speed = new Vector2(0, 0);
                }
            }
            if(Game1.gameState == GameState.Rolling && speed.X == 0 && speed.Y == 0)
            {
                Game1.gameState = GameState.ChoseVect;
            }
                
        }
        public void SetSpeed(float angle, double force)
        {
            speed.X = (float)(Math.Sin(angle+ Math.PI/2) * force);
            speed.Y = (float)(Math.Cos(angle + Math.PI / 2) * force);
        }

    }
}
