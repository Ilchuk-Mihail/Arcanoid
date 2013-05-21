using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcanoid
{
    class Points_Anim
    {
        public Texture2D Texture;
        public Vector2 position;
        public Vector2 VelocityPoints = new Vector2(0, 1);
        public bool alive;
        public bool deletePoints;
        public bool run;
        public Vector2 positionStop;
        public Color clr;

    
        public Points_Anim(Texture2D pointsTexture, Vector2 position , Vector2 positionStop, bool alive , bool run)
        {
            this.Texture = pointsTexture;
            this.position = position;
            this.positionStop = positionStop;
            this.alive = alive;
            this.run = run;
            clr = new Color(255, 255, 255, 255);

        }

         public Vector2 Position
         {
             get { return position; }
             set { } 
         }

        public Points_Anim()
        { 
 
        }

        public bool Alive
        {
            get { return alive; }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                Texture.Width,
                Texture.Height);
        }

        public void RunPoints()
        {

            if (alive && run)
            {
               clr.R -= 5;
               clr.G -= 5;
               clr.B -= 5;
               clr.A -= 5;

               if (position.Y >= positionStop.Y - 20)
               {
                   clr = new Color(255, 255, 255, 255);
               }

                if (position.Y <= positionStop.Y - 71)
                {
                    alive = false;
                    deletePoints = true;
                }

                position -= VelocityPoints;
           }
        }

        public void Draw_Points_Animation(SpriteBatch spriteBatch)
        {
            
            if (alive)
            spriteBatch.Draw(Texture, position, clr);
        }

    }
}
