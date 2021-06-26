using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MyLibrary
{
    public class Label
    {
        public string Text { get { return text; } set { text = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        string text;
        SpriteFont font;
        Vector2 position;
        public Label(Vector2 pos)
        {
            text = "";
            position = pos;
        }
        public Label()
        {
            text = "";
            position = new Vector2();
        }
        public void LoadContent(ContentManager content)
        {
            font = null;
            font = content.Load<SpriteFont>("Font");
        }
        public void LoadContent(SpriteFont font)
        {
            this.font = font;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, Color fontColor)
        {
            spriteBatch.DrawString(font, text, position, fontColor);
        }
    }
}
