using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid.MenuSystem
{
    class MenuEsc : Menu
    {
        private Texture2D Escbackground;

        public override void Update(GameTime gameTime)
        {
              base.Update(gameTime);

              if (startPosition)
              {
                  foreach (MenuItem item in Items)
                  {
                      item.position.X = 910;
                  }
              }

              foreach(MenuItem item in Items)
              {
                  startPosition = false;

                  if (item.position.X >= 430)
                  {
                      item.position.X -= 10;
                  }
                 
              }

        }

        public override void DrawBackground(SpriteBatch spriteBatch, Texture2D background)
        {
            base.DrawBackground(spriteBatch, this.Escbackground);
        }

        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Game-Font/ListLevelFont");
            Escbackground = Content.Load<Texture2D>("Texture/Backgrounds/EscBackground");
        }

        

    }
}
