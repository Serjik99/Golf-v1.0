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
    public class ButtonWithRectangle
    {
        public Button Button { get; set; }
        public bool IsHovered;
        public Rectangle Rectangle { get; set; }
        MouseState mouse = new MouseState();
        Texture2D texture;
        SpriteFont font;
        string text;
        public ButtonWithRectangle(Rectangle buttonRectangle)
        {
            Rectangle = buttonRectangle;
            Button = new Button(buttonRectangle);
            font = null;
            text = "";
        }
        public void LoadContent(ContentManager content, string buttonTexture)
        {
            texture = content.Load<Texture2D>(buttonTexture);
            font = content.Load<SpriteFont>("Font");
        }
        public void LoadContent(Texture2D newTexture)
        {
            texture = newTexture;
        }
        public void LoadContent(Texture2D newTexture, SpriteFont font)
        {
            texture = newTexture;
            this.font = font;
        }
        public void Rename(string newString)
        {
            text = newString;
        }
        public void Update()
        {
            Button.Update();
            if (Button.CheckLeftMouse()&&Rectangle.Intersects(new Rectangle(mouse.X,mouse.Y,10,10)))
            {
                IsHovered = true;
            }
            else
            {
                IsHovered = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Rectangle, Color.White);
            }
            if (font != null)
            {
                spriteBatch.DrawString(font, text, new Vector2(Rectangle.X + Rectangle.Width / 2 - font.MeasureString(text).X / 2, Rectangle.Y + Rectangle.Height / 2 - font.MeasureString(text).Y / 2), Color.White);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color textureColor, Color fontColor)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Rectangle, textureColor);
            }
            if (font != null)
            {
                spriteBatch.DrawString(font, text, new Vector2(Rectangle.X + Rectangle.Width / 2 - font.MeasureString(text).X / 2, Rectangle.Y + Rectangle.Height / 2 - font.MeasureString(text).Y / 2), fontColor);
            }
        }
    }
}
