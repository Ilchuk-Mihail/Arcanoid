using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Arcanoid
{
    class Menu
    {
            public List<MenuItem> Items { get; set; }
            public bool startPosition { get; set; }
            public SpriteFont font;

            private Texture2D background;
            protected int currentItem;
            protected  KeyboardState oldState;
            protected float pulse;
            protected KeyboardState state;
            float pauseTime = 0.5f;

            public SoundEffect click;

            public float Pause
            {
                get { return pauseTime; }
                set { pauseTime = value; }
            }

           public Texture2D Background
           {
               get { return background; }
           }
            
            public Menu()
            {
                Items = new List<MenuItem>();         
            }

            public int  CurrentItem
            {
                get { return currentItem;  }
                set { currentItem = value; }
            }
            
            public virtual void Update(GameTime gameTime)
            { 
                state = Keyboard.GetState();

                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                pauseTime -= elapsed;
                

                if (state.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter) && pauseTime < 0 )
                {   
                    Items[currentItem].OnClick();

                    click.Play();
                  
                    pauseTime = 0.5f;
                }

                int delta = 0;

                if(state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
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
            }

            public virtual void DrawBackground(SpriteBatch spriteBatch, Texture2D background)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            public virtual void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Begin();

                Vector2 origin =new Vector2(0,0);
               
                foreach(MenuItem item in Items)
                { 
                    float scale = 1;
                    Color color = Color.White;
                    if (item.Active == false)
                    {
                        color = Color.Gray;
                        scale = 1;
                    }

                    if (item == Items[currentItem])
                    {
                        color = Color.Brown;
                      //  scale = 1.3f;
                        scale = 1 + pulse * 0.10f;
                        
                    }


                    spriteBatch.DrawString(font, item.Name,item.position, color,0f,origin,scale,SpriteEffects.None,0);
                   
                }

                spriteBatch.End();
            }

            public virtual void LoadContent(ContentManager Content)
            {
                font = Content.Load<SpriteFont>("Game-Font/MenuFont");
                background = Content.Load<Texture2D>("Texture/Backgrounds/MenuBackground");

                click = Content.Load<SoundEffect>("Sounds/click");
            }
        }
   
}
