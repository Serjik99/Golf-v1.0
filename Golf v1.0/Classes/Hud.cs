using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Golf_v1_0;
namespace Golf_v1_0
{
    class Hud
    {
        private SpriteFont spriteFont;
        private int score;
        private Vector2 position;
        public Hud()
        {
            //Score
            score = 0;
            position = new Vector2(10, 10);
            //Health
          
        }
        public void Update(GameTime gameTime)
        {
            if(Game1.turn == Turn.Player1)
            {
                score = Game1.score1; 
            }else if(Game1.turn == Turn.Player2)
            {
                score = Game1.score2;
            }

        }
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("GameFont");
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color =  Color.Aqua;
            // Score
            
            spriteBatch.DrawString
                (
                    spriteFont, "Score:"+score.ToString()+Game1.turn,
                    position, color
                );

        }
        
    }
}
