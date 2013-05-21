using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid
{
    public class OptionLevel
    {
        public bool died = false;
        public bool lose = false;
        public bool win = false;

        Texture2D YouDied;
        Texture2D YouLose;
        Texture2D YouWin;
        Rectangle position;
        KeyboardState keyboardstate;
        Color clr = new Color(0, 0, 0, 0);
        Texture2D fon;
        Vector2 fonPosition;

        Game1 game ;

        public OptionLevel(Game1 game)
        {
            this.game = game;
            fonPosition = new Vector2(game.Width, game.Height);
        }

        public OptionLevel(Rectangle screenBounds, ContentManager Content)
        {
            YouDied = Content.Load<Texture2D>("Texture/OptionLevel/you_died");
            YouLose = Content.Load<Texture2D>("Texture/OptionLevel/you_lose");
            YouWin =  Content.Load<Texture2D>("Texture/OptionLevel/you_win");
            fon    =  Content.Load<Texture2D>("Texture/OptionLevel/fon");
            
            position = new Rectangle(screenBounds.Width / 2  - 180, screenBounds.Height / 2 -100, YouDied.Width, YouDied.Height);
  
        }
    
        public void Died()
        {
            clr.R += 2;
            clr.G += 2;
            clr.B += 2;
            clr.A += 2;

             keyboardstate = Keyboard.GetState();
             if (keyboardstate.IsKeyDown(Keys.Space))
             {
                 died = false;
             }

        }

        public void Lose()
        {
            clr.R += 2;
            clr.G += 2;
            clr.B += 2;
            clr.A += 2;

            keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.Enter))
            {
                lose = false;
            }
        }

        public void Win()
        {
            clr.R += 2;
            clr.G += 2;
            clr.B += 2;
            clr.A += 2;

            keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.Enter))
            {
                win = false;
            }
        }

        public void DrawDied(SpriteBatch spriteBatch)
        {
           spriteBatch.Draw(fon, fonPosition, Color.White);
           spriteBatch.Draw(YouDied,position,clr);
        }

        public void DrawLose(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fon, fonPosition, Color.White);
            spriteBatch.Draw(YouLose, position, clr);
        }

        public void DrawWin(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fon, fonPosition, Color.White);
            spriteBatch.Draw(YouWin, position, clr);
        }


    }
}
