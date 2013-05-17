﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LevelCreator
{
    class Bloc : Object
    {
      
        public Texture2D bloc1 { get; set; }
        public Texture2D bloc2 { get; set; }
        public Texture2D bloc3 { get; set; }
        public Texture2D bloc4 { get; set; }
        public Texture2D bloc0 { get; set; }
        Texture2D texture;
        public int type;

        public Bloc[,] lsBloc { get ; set ; }

        public int Activ { get; set; }

        public Bloc()
        {
            Activ = 0;
            lsBloc = new Bloc[10, 13];
            position = new Rectangle(0, 0, 70, 40);
        }

        public Bloc(Rectangle position, Texture2D texture, int type)
        {
            Activ = 0;
            this.position = position;
            this.texture = texture;
            this.type = type;
        }

        public void Update()
        {
          state = Keyboard.GetState();

          if (state.IsKeyDown(Keys.D1))
          {
              Activ = 1;
              Pointer.delete = false;
          }

          if (state.IsKeyDown(Keys.D2))
          {
              Activ = 2;
              Pointer.delete = false;
          }

          if (state.IsKeyDown(Keys.D3))
          {
              Activ = 3;
              Pointer.delete = false;
          }

          if (state.IsKeyDown(Keys.D4))
          {
              Activ = 4;
              Pointer.delete = false;
          }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Activ == 1)
                spriteBatch.Draw(bloc1, position, Color.White);

            if (Activ == 2)
                spriteBatch.Draw(bloc2, position, Color.White);

            if (Activ == 3)
                spriteBatch.Draw(bloc3, position, Color.White);
                  
            if (Activ == 4)
                spriteBatch.Draw(bloc4, position, Color.White);
        
        }

        public void DrawList(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Load(ContentManager Content)
        {
            bloc1 = Content.Load<Texture2D>("TextureLevelCreator/bloc1");
            bloc2 = Content.Load<Texture2D>("TextureLevelCreator/bloc2");
            bloc3 = Content.Load<Texture2D>("TextureLevelCreator/bloc3");
            bloc4 = Content.Load<Texture2D>("TextureLevelCreator/bloc4");
            bloc0 = Content.Load<Texture2D>("TextureLevelCreator/bloc0");   
        }


    }
    
}
