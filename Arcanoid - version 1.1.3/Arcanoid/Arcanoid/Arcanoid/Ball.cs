using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid
{
    class Ball
    {
        public Vector2 motion;
        public Rectangle position;

        float ballSpeed = 6.7f;
        public bool alive;         // Чи живий об'єкт
        Texture2D TextureBall;
        Texture2D StandTexture;
        Rectangle screenBounds;
        bool ballFire;
       
        public Ball(Texture2D TextureBall, Rectangle screenBounds)
        {
            this.TextureBall = TextureBall;
            StandTexture = TextureBall;
            this.screenBounds = screenBounds;
            ballFire = false;
        }

        public int Width
        {
            get { return TextureBall.Width; }
        }

        public int Height
        {
            get { return TextureBall.Height; }
        }

        public bool BallFire
        {
            get {return ballFire; }
        }

        public Texture2D Texture
        {
            get { return TextureBall;  }
            set {
                TextureBall = value;
                ballFire = true ;
                }
        }
        public void Standart()
        {
            this.TextureBall = StandTexture;
            ballFire = false;
        }

        public void Update()
        {
            position.X += (int) (motion.X * ballSpeed);
            position.Y += (int) (motion.Y * ballSpeed);

            CheckWallCollision();
        }

        private void CheckWallCollision()
        {
            if (position.X < 0)
            {
                position.X = 0;
                motion.X *= -1;
            }
            if (position.X + GetBounds.Width > screenBounds.Width)
            {
                position.X = screenBounds.Width - GetBounds.Width;
                motion.X *= -1;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                motion.Y *= -1;
            }
        }

        public void SetInStartPosition(Rectangle paddleLocation)
        {
            Standart();
            motion = new Vector2(1, -1);
          //  position.Y = paddleLocation.Y - TextureBall.Height;
         //   position.X = paddleLocation.X + (paddleLocation.Width - TextureBall.Width) / 2;
           position = new Rectangle(paddleLocation.X + (paddleLocation.Width - TextureBall.Width) / 2,
                                     paddleLocation.Y - TextureBall.Height,
                                     TextureBall.Width, TextureBall.Height);

   //   position = new Rectangle(210, 0, TextureBall.Width, TextureBall.Height); motion = new Vector2(1, -1);
   //   position = new Rectangle(300, 150, TextureBall.Width, TextureBall.Height); motion = new Vector2(1, -1);
   //  position = new Rectangle(400, 185, TextureBall.Width, TextureBall.Height);  motion = new Vector2(-1, -1);

     //  position = new Rectangle(402, 0, TextureBall.Width, TextureBall.Height); motion = new Vector2(-1, -1);
        //  position = new Rectangle(260 , 0, TextureBall.Width, TextureBall.Height); motion = new Vector2(1, -1);
     //   position = new Rectangle(395, 0, TextureBall.Width, TextureBall.Height); motion = new Vector2(-1, 1);

    // position = new Rectangle(345, 0, TextureBall.Width, TextureBall.Height); motion = new Vector2(1, -1);
    //  position = new Rectangle(490, 0, TextureBall.Width, TextureBall.Height); motion = new Vector2(-1, 1);
      // position = new Rectangle(505, 150, TextureBall.Width, TextureBall.Height); motion = new Vector2(-1, -1);
             
         //  position = new Rectangle(490, 180, TextureBall.Width, TextureBall.Height); motion = new Vector2(-1, -1);
             // position = new Rectangle(500, 30, TextureBall.Width, TextureBall.Height); motion = new Vector2(-1, 1);
            //  position = new Rectangle(330,200, TextureBall.Width, TextureBall.Height); motion = new Vector2(1, -1);

            ballFire = false;
            ballSpeed = 6.7f;
            alive = false;
        }

        public float SpeedBall
        {
            get { return ballSpeed; }
            set { ballSpeed = value; }
        }

        public bool OffBottom()
        {
            if (position.Y > screenBounds.Height)
            {

                return true;
            }
            return false;
        }

        public void PaddleCollision(Rectangle paddleLocation)
        {
            Rectangle ballLocation = new Rectangle(
                (int)position.X,
                (int)position.Y,
                GetBounds.Width,
                GetBounds.Height);

            if (paddleLocation.Intersects(ballLocation))
            {
                position.Y = paddleLocation.Y - GetBounds.Height;
             //  motion.Y *= -1;
               float x = position.X - paddleLocation.X;
               if (x > 85 && x < 95)
               {
                   motion.Y *= -1;
               }
               else if (x > 0 && x < 10)
               {
                   motion.Y *= -1;
               }
               else if (x > 95 && x < 180)
               {
                   motion.Y *= -1;
               }

               float l = paddleLocation.Width;                       // розмыр платформи
               float angle = (float)Math.Acos((x / l));
              // motion.X = (float)Math.Cos(angle)   ;
               motion.Y = -(float)Math.Sin(angle)  ; 
            
            }

             motion.Normalize();
        }

        public Rectangle GetBounds
        {
            get { return position; }
            set {

                if (value.Width > Width && value.Height > Height)
                {
                    position = value;
                }

                if (Width > value.Width && Height > value.Height)
                {
                    position = value;
                }

                } 
        }

        public Rectangle GetBoundsNext()
        {
            return new Rectangle(
                (int)(position.X + motion.X * ballSpeed ),
                (int)(position.Y + motion.Y * ballSpeed),
                GetBounds.Width,
                GetBounds.Height);
        }

        public Rectangle BoundsNext()
        {
            return new Rectangle(
                (int)(position.X + motion.X * ballSpeed + 15),
                (int)(position.Y + motion.Y * ballSpeed + 15 ),
                GetBounds.Width + 10 ,
                GetBounds.Height+ 10);
        }

        public bool IfAlive_Ball()
        {
            KeyboardState keyboardstate = Keyboard.GetState();

            if (keyboardstate.IsKeyDown(Keys.Space))
            {
                this.alive = true;
                return true;
            }

            else return false;
        }

        public void DrawBallLives(SpriteBatch spriteBatch, int Lives)
        {
            if (Lives == 3)
            {
                spriteBatch.Draw(TextureBall, new Vector2(800, 5), Color.White);
                spriteBatch.Draw(TextureBall, new Vector2(830, 5), Color.White);
                spriteBatch.Draw(TextureBall, new Vector2(860, 5), Color.White);
            }

            if (Lives == 2)
            {
                spriteBatch.Draw(TextureBall, new Vector2(800, 5), Color.White);
                spriteBatch.Draw(TextureBall, new Vector2(830, 5), Color.White);
            }

            if (Lives == 1)
            {
                spriteBatch.Draw(TextureBall, new Vector2(800, 5), Color.White);
            }
        }

        public void DrawBall(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(TextureBall, position, Color.White);

        }
    }
}
