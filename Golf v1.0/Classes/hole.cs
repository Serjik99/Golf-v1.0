using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;


namespace Golf_v1_0
{
    class hole
    {
        Texture2D texture;
        Vector2 position;
        Rectangle holeRectangle;

        public hole(Vector2 position)
        {
            this.position = position;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("hole(0)");
            holeRectangle = new Rectangle((int)position.X, (int)position.Y + texture.Height - 25, texture.Width, 25);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
