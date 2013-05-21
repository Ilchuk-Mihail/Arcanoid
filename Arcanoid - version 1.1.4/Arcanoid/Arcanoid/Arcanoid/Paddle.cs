using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;


namespace Arcanoid
{
    public class Paddle 
    {
        public bool alive;         // Чи живий об'єкт
        Texture2D TexturePaddle;
        Rectangle screenBounds;    //  Прямокутник екрану
        Rectangle position;
        Vector2 paddleSpeed;

        public Paddle(Texture2D texturePaddle, Rectangle screenBounds)
        {
            this.TexturePaddle = texturePaddle;
            this.screenBounds = screenBounds;
            paddleSpeed = new Vector2(10.5f, 0);
            SetInStartPosition();
        }
  

        public void SetInStartPosition()
        {
            position.X = (screenBounds.Width - TexturePaddle.Width) / 2;
            position.Y = screenBounds.Height - TexturePaddle.Height - 5;
            alive = false;
            position = new Rectangle((screenBounds.Width - TexturePaddle.Width) / 2,
                                     (screenBounds.Height - TexturePaddle.Height - 5),
                                      TexturePaddle.Width,TexturePaddle.Height);
            paddleSpeed = new Vector2(10.5f, 0);
        }

        public void NewAlive_Paddle()
        {
            this.alive = false;
            SetInStartPosition();
        }

        public bool IfAlive_Paddle()
        {
            KeyboardState keyboardstate = Keyboard.GetState();

            if (keyboardstate.IsKeyDown(Keys.Space))
            {
                this.alive = true;
                return true;
            }

            else return false;
        }

        public void Move_Right()
        {
            position.X += (int) paddleSpeed.X;

            StopPaddle();
        }

        private void StopPaddle()
        {

            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.X > screenBounds.Width - GetBounds.Width)
            {
                position.X = screenBounds.Width - GetBounds.Width;
            }
        }

        public void Move_Left()
        {
            position.X -= (int)paddleSpeed.X;

            StopPaddle();
        }

        public int Width
        {
            get { return TexturePaddle.Width; }
        }

        public int Height
        {
            get { return TexturePaddle.Height; }
        }

        public Vector2 Velocity
        {
            get { return paddleSpeed; }
            set { paddleSpeed = value; }
        }

        public Rectangle GetBounds
        {
            get { return position;  }
            set
            {
                if (value.Width > Width + Width)
                {
                    position.Width += 10;
                }
                else

                if (value.Width == position.Width + position.Width)
                {
                    position.Width = value.Width;
                }

                if (position.Width > value.Width)
                {
                    position = value;
                }
                else if (Width == value.Width)
                {
                    position.Width -= 10 ;
                }
              
            }
        }

        public void DrawPaddle(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(TexturePaddle, position, Color.White);
        }
    }
}
