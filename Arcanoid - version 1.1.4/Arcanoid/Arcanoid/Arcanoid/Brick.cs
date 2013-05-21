using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcanoid
{
    class Brick : AnimatedSprite
    {
        public int value; 
        public bool bonus = false;

        private Texture2D brickImage;
        private Texture2D[] imageArray;
        private int hitsToKill;

        public Brick(Texture2D[] img, int hits, Rectangle position , BrickType type)
        {
            hitsToKill = hits;
            imageArray = img;
            brickImage = imageArray[hitsToKill - 1];
            value = hitsToKill;
            dead = false;
            this.type = type;
            this.position = position;
        }

        public Brick(int frameCount, int framesPerSec, Rectangle position, Texture2D spriteTexture, BrickType type)
        {
            frameСount = frameCount;
            timeFrame = (float)1 / framesPerSec;
            frame = 0;
            totalElapsed = 0;
            this.position = position;
            this.spriteTexture = spriteTexture;
            this.type = type;
            dead = false;
        }

        public int hits
        {
            get { return hitsToKill; }
        }

        public bool TypeBloc()
        {
            if (type == BrickType.AnimRed || type == BrickType.BreaksBloc)
                return true;
            else return false;
        }

        public Texture2D getTexture()
        {
            return brickImage;
        }

        public int getWidth()
        {
            return brickImage.Width;
        }

        public int getHeight()
        {
            return brickImage.Height;
        }

        public bool isDead
        {
            get { return dead; }
            set { dead = value; }
        }

        public bool setHit()
        {
            hitsToKill -= 1;

            if (hitsToKill <= 0)
            {
                dead = true;
                return true;
            }
            else
            {

                brickImage = imageArray[hitsToKill - 1];
                return false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            Color clr = new Color(0, 250, 127, 255);

            if (type == BrickType.Hard)
                spriteBatch.Draw(brickImage, position, clr);
            else
            {
                clr = new Color(65, 105, 225, 255);

                spriteBatch.Draw(brickImage, position, clr);
            }
        }
    }
}
