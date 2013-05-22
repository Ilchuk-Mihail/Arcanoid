using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LevelCreator
{
    class Help
    {
        public bool Visible = false;
        public bool Close = false;
        public bool Save = false;
        public SpriteFont font;
        Vector2 position;
        Vector2 positionSave;
        Color clr;
        float pauseTime = 3;


        public Help()
        {
            position = new Vector2(-360, 450);
            positionSave = new Vector2(930, 490);
            clr = new Color(0, 0, 0, 0);
        }


        public void Update()
        {
           if(position.X < 10)
              position.X += 5f;

           if (Close)
           {
               position.X -= 10f;
               if (position.X <= -360)
               {
                   Close = false;
                   Visible = false;
               }
           }   
             
        }

        public void SaveUpdate(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            pauseTime -= elapsed;

            if (pauseTime < 0)
            {
                if (positionSave.X < 930)
                    positionSave.X += 5f;
                else 
                {
                    pauseTime = 3;
                    Save = false;
                    clr = new Color(0, 0, 0, 0);
                }

                   return;
            }

            if (positionSave.X > 700)
                positionSave.X -= 5f;

            clr.R += 5;
            clr.G += 5;
            clr.B += 5;
            clr.A += 5;
        }

        public void SaveDraw(SpriteBatch spriteBatch)
        {
            if (Save)
            {
                spriteBatch.DrawString(font, "Рiвень збережено", positionSave, clr);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, @"Змiнити тип вказiвника - 1 - 8", position, Color.Black);
            spriteBatch.DrawString(font, @"Зберегти рiвень - Space ", (position + new Vector2(0,20)), Color.Black);
          
            spriteBatch.DrawString(font, @"Закрити допомогу - F2 ", (position + new Vector2(0, 60)), Color.Black);
            spriteBatch.DrawString(font, @"Поставити блок - Enter ", (position + new Vector2(0, 80)), Color.Black);
            spriteBatch.DrawString(font, @"Гумка - Delete ", (position + new Vector2(0, 100)), Color.Black);
            spriteBatch.DrawString(font, @"Очисити поле - Esc ", (position + new Vector2(0, 120)), Color.Black);
            spriteBatch.DrawString(font, @"Вихiд - Q ", (position + new Vector2(0, 140)), Color.Black);
        }

        public void Load(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("FontLevelCreator/font");
        }
    }
}
