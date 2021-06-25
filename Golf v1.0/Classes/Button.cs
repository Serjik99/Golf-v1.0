using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace MyLibrary
{
    public delegate void OnClick();
    public class Button
    {
        public Rectangle BoundingBox { get { return boundingBox; } set { boundingBox = value; } }
        public OnClick OnLeftClick { get { return onLeftClick; } set { onLeftClick = value; } }
        public OnClick OnRightClick { get { return onRightClick; } set { onRightClick = value; } }
        public List<OnClick> MemoryCells { get { return memoryCells; } set { memoryCells = value; } }
        public Rectangle boundingBox, mouseRectangle;
        MouseState mouse, prevmouse;
        ButtonState leftButton, prevLeftButton, rightButton, prevRightButton;
        OnClick onLeftClick;
        OnClick onRightClick;
        List<OnClick> memoryCells;
        public bool isHover;
        public bool delegatemode;
        public Button()
        {
            delegatemode = false;
            onLeftClick = null;
            onRightClick = null;
            boundingBox = new Rectangle();
            memoryCells = new List<OnClick>();
        }
        public Button(Rectangle buttonRectangle)
        {
            delegatemode = false;
            onLeftClick = null;
            onRightClick = null;
            boundingBox = buttonRectangle;
            isHover = false;
        }
        public void Update()
        {
            mouse = Mouse.GetState();
            mouseRectangle = new Rectangle(mouse.Position.X, mouse.Position.Y, 0, 0);
            //leftButton
            if (boundingBox.Intersects(mouseRectangle) && mouse.LeftButton == ButtonState.Pressed && CheckLeftMouse() && onLeftClick != null)
            {
                if (delegatemode)
                {
                    onLeftClick();
                }
                else
                {
                    new Thread(new ThreadStart(onLeftClick)).Start();
                }
               
            }
            if (boundingBox.Intersects(mouseRectangle) && mouse.RightButton == ButtonState.Pressed && CheckRightMouse() && onRightClick!=null)
            {
                if (delegatemode)
                {
                    onLeftClick();
                }
                else
                {
                    onRightClick();
                }
            }
            prevmouse = mouse;
        }

        public bool CheckLeftMouse()
        {
            return mouse.LeftButton != prevmouse.LeftButton;

        }
        public bool CheckRightMouse()
        {
            return mouse.RightButton != prevmouse.RightButton;
        }


    }
}
