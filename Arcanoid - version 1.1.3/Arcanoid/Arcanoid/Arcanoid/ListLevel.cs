using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid
{
    class ListLevel : Menu
    {
        Vector2 position = new Vector2(100, 100);

        public Texture2D background;
        public Texture2D Ok  {get; set;}
        public Vector2[] OkPosition = new Vector2[10];
        public Vector2[] TimePosition = new Vector2[10];
        public Vector2[] ScorePosition = new Vector2[10];

        public Vector2 positionTime = new Vector2(260, -200);
        public Vector2 positionScor = new Vector2(600, -200);

        float pauseTime = 0.0f;
        public bool mainLevelNone = false;

        float pauseT = 0.3f;
       new public float  Pause
        {
            get { return pauseT; }
            set { pauseT = value; }
        }

       public ListLevel()
       {
           Vector2 pos = new Vector2 (170,105);
           for (int i = 0; i < 10; i++)
           {
               OkPosition[i] = pos;
               pos.Y += 50;
           }

           pos = new Vector2(240, 100);
           Vector2 ScorePos = new Vector2(600, 100);

           for (int i = 0; i < 10; i++)
           {
               TimePosition[i] = pos;
               ScorePosition[i] = ScorePos;

               ScorePos.Y += 50;
               pos.Y += 50;

           }

           StartPosition();
       }

       public override void Update(GameTime gameTime)
       {
           KeyboardState state = Keyboard.GetState();

           float elap = (float)gameTime.ElapsedGameTime.TotalSeconds;
           pauseT -= elap;

           ///Підписи "Рахунок" та "Час "  у списку рівнів  ---------------
         
           #region Time_Score

           if (positionTime.Y <= 30)
           {
               positionTime.Y += 5f;
               positionScor.Y += 5f;
           }
           #endregion

           if (state.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter) && pauseT < 0)
           {
               Items[currentItem].OnClick();
               pauseT = 0.3f;
           }

           int delta = 0;

           if (state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
               delta = -1;

           if (state.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
               delta = 1;

           currentItem += delta;
           bool ok = false;
           while (!ok)
           {
               if (currentItem < 0)
                   currentItem = Items.Count - 1;
               else if (currentItem > Items.Count - 1)
                   currentItem = 0;
               else if (Items[currentItem].Active == false)
                   currentItem += delta;
               else ok = true;

           }

           oldState = state;

           pulse = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3) + 1);

               foreach (MenuItem item in Items)
               {

                   float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                   pauseTime -= elapsed;

                   if (pauseTime <= 0)
                   {
                       if (item.position.X >= 430)
                       {
                           item.position.X -= 60;
                           pauseTime = 0.50f;
                       }
                   }

               }
       }

        public void StartPosition()
        {
          
                positionTime = new Vector2(260, -200);  // стартові позиції для підписів
                
            positionScor = new Vector2(600, -200);

                foreach (MenuItem item in Items)
                {
                    if (item.position.X == 370)
                    {
                        startPosition = true;
                       // item.position.X = 910;
                    }
                    else 
                        startPosition = false;
                }
                if (startPosition)
                {
                    foreach (MenuItem item in Items)
                    {
                        item.position.X = 910;
                    }
                }
               
        }

        public override void DrawBackground(SpriteBatch spriteBatch, Texture2D background)
        {
            base.DrawBackground(spriteBatch, this.background);
        }

        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Game-Font/ListLevelFont");
            background = Content.Load<Texture2D>("Texture/Backgrounds/BlocBackground");
        }


        public void MainUpdate(GameTime gameTime)
        {
            base.Update(gameTime);
        }

       
    }
}
