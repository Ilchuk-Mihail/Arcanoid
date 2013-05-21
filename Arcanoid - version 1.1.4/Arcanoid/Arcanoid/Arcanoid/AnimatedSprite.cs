using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcanoid
{
    public  abstract class  AnimatedSprite
    {
        public Rectangle position;
        public Texture2D spriteTexture;
        public int frame;
        public BrickType type;
        public bool stopAnim = false;

        protected int frameСount;
        protected double timeFrame;
        protected double totalElapsed;
        protected bool dead;
      
        public AnimatedSprite()
        { 
        
        }

        public void UpdateFrame(double elapsed)
        {
            totalElapsed += elapsed;
       
           if (totalElapsed > timeFrame)
           {    
                frame = frame % (frameСount - 1);
                frame++;
                totalElapsed -= timeFrame;
           }
        }
 
        public void DrawAnimationSprite(SpriteBatch spriteBatch)
        {
            int frameWidth = spriteTexture.Width / frameСount;
            Rectangle rectangle = new Rectangle(frameWidth * frame, 0, frameWidth, spriteTexture.Height);
            spriteBatch.Draw(spriteTexture, position, rectangle, Color.White);
        }

    }
}
