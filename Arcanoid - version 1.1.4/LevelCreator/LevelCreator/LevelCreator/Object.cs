using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LevelCreator
{
    abstract class Object
    {
        public KeyboardState state;
        public Rectangle position;

        public void Right()
        {
            position.X += 70;
        }

        public void Left()
        {
            position.X -= 70;
        }

        public void Up()
        {
            position.Y -= 40;
        }

        public void Down()
        {
            position.Y += 40;
        }

        public void ScreenBounds()
        {
            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
            }

            if (position.X + 70 > 910)
            {
                position.X = 910 - 70 ;
            }

            if (position.Y + 40 > 650)
            {
                position.Y = 650 - 40 - 10;
            }
        }
    }
}
