using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;


namespace Golf_v1_0
{
    
    class Hole
    {
        Random random = new Random();
        Texture2D texture;
        Vector2 position;
        Rectangle holeRectangle;
        Rectangle colision;

        public Hole(int x , int y)
        {
            position = new Vector2(random.Next(0, x - 100), random.Next(0 , y /3));

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("hole(0)");
            holeRectangle = new Rectangle((int)position.X, (int)position.Y , texture.Width, texture.Height);
            colision = new Rectangle((int)position.X + 5, (int)position.Y + 5, texture.Width - 10, texture.Height - 10);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, holeRectangle, Color.White);
            spriteBatch.Draw(texture, colision, Color.Red);
        }
        public void SetPosition(Vector2 pos)
        {
            position = pos;
        }
        public void SetTexture(ContentManager content, string texture_name)
        {
            texture = content.Load<Texture2D>(texture_name);
        }

        public Rectangle GetColision()
        {
            return colision;
        }

        
    }
}
