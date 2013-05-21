using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace LevelCreator
{
    class Pointer : Object
    {
        Texture2D pointerTexture;
        public static bool delete { get; set; }

        public Pointer()
        {
            position = new Rectangle(0, 0, 70, 40);
            delete = false;
        }     

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pointerTexture, position, Color.White);
        }

        public void ClearBloc()
        {
            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Delete))
            {
                delete = true;
            }
        }

        public void Load(ContentManager Content)
        {
            pointerTexture = Content.Load<Texture2D>("TextureLevelCreator/Pointer");
        }
    }
}
