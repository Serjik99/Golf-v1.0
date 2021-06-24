using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Golf_v1._0
{
    class Ball
    {
        int force;
        Vector2 speed;
        int acceleration;
        Vector2 position;
        Texture2D texture;
        double timeTick;
        Rectangle boundingBox;
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
            
        }
    }   
}
