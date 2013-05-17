using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcanoid
{
    class Bullets
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 Speed;
        public int ActiveTime;
        public int TotalActiveTime;
        public bool IsVisible;
        public bool IsVisibleBul;

        Texture2D bulTexture;
        public Vector2 BulpositionLeft ;
        public Vector2 BulpositionRight;

        public Bullets(Texture2D texture, Vector2 speed, int activeTime)
        {
            this.texture = texture;
            IsVisible = false;
            this.Speed = speed;
            this.ActiveTime = activeTime;
            this.TotalActiveTime = 0;
        }

        public Bullets(Texture2D bulTexture)
        {
            this.bulTexture = bulTexture;
            BulpositionLeft = new Vector2(-200, 100);
            BulpositionRight = new Vector2(-200, 100);
         
        }

        public void UpdateBulletBul(Rectangle paddleRect)
        {
            BulpositionLeft.X = paddleRect.X -15;
            BulpositionLeft.Y = paddleRect.Y - 30;

            BulpositionRight.X = paddleRect.X + paddleRect.Width -25;
            BulpositionRight.Y = paddleRect.Y - 30;
        }


        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(texture, position, Color.White);   
            }
           
        }

        public void DrawBul(SpriteBatch spriteBatch)
        {
            if (IsVisibleBul)
            {
                spriteBatch.Draw(bulTexture, BulpositionLeft, Color.White);
                spriteBatch.Draw(bulTexture, BulpositionRight, Color.White);
            }
        }

    }
}
