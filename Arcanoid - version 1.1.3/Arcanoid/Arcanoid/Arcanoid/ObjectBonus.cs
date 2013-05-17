using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcanoid
{
    class ObjectBonus: Points_Anim
    {
        Vector2 VelocityBonus = new Vector2(0, 3.5f);
        public bool deleteBonus = false;
        int randomBonus;

        public ObjectBonus(Texture2D BonusTexture, Vector2 position ,Vector2 positionStop, int randomBonus)
        {
            this.Texture = BonusTexture;
            this.position = position;
            alive = false;
            this.positionStop = positionStop;
            clr = new Color(0, 0, 0, 0);
            this.randomBonus = randomBonus;
        }

        public int BonusValue
        {
            get { return randomBonus; }
        }

        public void RunObject()
        {

            if (alive)
            {
                if (position.Y <= positionStop.Y + 50)
                {
                    clr.R += 5;
                    clr.G += 5;
                    clr.B += 5;
                    clr.A += 5;
                }

                else 
                {
                    clr = new Color(255, 255, 255, 255);
                }

                if (position.Y >= positionStop.Y + 700)
                {
                    alive = false;
                    deleteBonus = true;
                   
                }

                position += VelocityBonus;
            }
        
        }

        public void DrawBonus(SpriteBatch spriteBatch)
        {
            if(alive)
            spriteBatch.Draw(Texture, position, clr);
        }

    }
}
