using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using LevelCreator;

namespace LevelCreater
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int Width;
        public int Height;

        public bool Open = false;

        KeyboardState state;
        KeyboardState oldState;

        float pauseTime = 7f;

        Texture2D Grid;

        Bloc blok;

        Help help = new Help();

        Bloc bloc = new Bloc();
        Pointer pointer = new Pointer();

        int[,] level = new int[10, 13];
        Rectangle[,] rect = new Rectangle[10, 13];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Width = graphics.PreferredBackBufferWidth = 910;
            Height = graphics.PreferredBackBufferHeight = 650;
        }

        protected override void Initialize()
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    rect[i, j] = new Rectangle(x, y, 70, 40);

                    x += 70;
                }

                x = 0;
                y += 40;
            }

            base.Initialize();
        }

        private void Clear()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    level[i, j] = 0;
                    bloc.lsBloc[i, j] = null;
                }
            }

        }

     /*   private bool ValiLevel()
        {
            bool ok = false;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (level[i, j] == 0)
                        ok = true;
                }
            }

            return ok;
        }*/

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bloc.Load(Content);
            pointer.Load(Content);
            help.Load(Content);

            Grid = Content.Load<Texture2D>("TextureLevelCreator/LevelGrid");
         
            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///  Метод який викликається безпосередньо перед виходом
        ///  bool зміна говорить про те що поток закритий 
        ///  тобто в грі поле меню Створити рівень стає знову активним
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            Open = true;
        }

        protected override void Update(GameTime gameTime)
        {
            bloc.Update();

            state = Keyboard.GetState();

            bloc.ScreenBounds();
            pointer.ScreenBounds();

            pointer.ClearBloc();
            if (help.Save)
            {
                help.SaveUpdate(gameTime);
            }

            if (help.Visible)
            {
                help.Update();
            }

            if (state.IsKeyDown(Keys.Escape))
            {
                Clear();
            }

            if (state.IsKeyDown(Keys.Q))
            {
                Open = true;
                this.Exit();
            }

            if (state.IsKeyDown(Keys.F1))
            {
                help.Visible = true;
            }

            if (state.IsKeyDown(Keys.F2))
            {
                help.Close = true;
            }

            if (state.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                bloc.Right();
                pointer.Right();
            }

            if (state.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                bloc.Left();
                pointer.Left();
            }

            if (state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                bloc.Up();
                pointer.Up();
            }

            if (state.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                bloc.Down();
                pointer.Down();
            }

            if (state.IsKeyDown(Keys.Enter))
            {
                if (bloc.Activ == 1)
                {
                    blok = new Bloc(bloc.position, bloc.bloc1, 1);
                }

                if (bloc.Activ == 2)
                {
                    blok = new Bloc(bloc.position, bloc.bloc2, 2);
                }

                if (bloc.Activ == 3)
                {
                    blok = new Bloc(bloc.position, bloc.bloc3, 3);
                }

                if (bloc.Activ == 4)
                {
                    blok = new Bloc(bloc.position, bloc.bloc4, 4);
                }

                if (bloc.Activ == 5)
                {
                    blok = new Bloc(bloc.position, bloc.bloc5, 5);
                }

                if (bloc.Activ == 6)
                {
                    blok = new Bloc(bloc.position, bloc.bloc6, 6);
                }

                if (bloc.Activ == 7)
                {
                    blok = new Bloc(bloc.position, bloc.bloc7, 7);
                }

                if (bloc.Activ == 8)
                {
                    blok = new Bloc(bloc.position, bloc.bloc8, 8);
                }

                Rectangle rectan = new Rectangle(bloc.position.X, bloc.position.Y, 50, 30);

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        if ((rect[i, j].Intersects(rectan)))
                        {
                            bloc.lsBloc[i,j] = blok;
                        
                            if (bloc.Activ == 1)
                                level[i, j] = 1;

                            if (bloc.Activ == 2)
                                level[i, j] = 2;

                            if (bloc.Activ == 3)
                                level[i, j] = 3;

                            if (bloc.Activ == 4)
                                level[i, j] = 4;

                            if (bloc.Activ == 5)
                                level[i, j] = 5;

                            if (bloc.Activ == 6)
                                level[i, j] = 6;

                            if (bloc.Activ == 7)
                                level[i, j] = 7;

                            if (bloc.Activ == 8)
                                level[i, j] = 8;

                            if (Pointer.delete)
                            {
                                level[i, j] = 0;
                                bloc.lsBloc[i, j] = null;
                            }
                        }
                    }
                }
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            pauseTime -= elapsed;

            if (state.IsKeyDown(Keys.Space) && (oldState.IsKeyUp(Keys.Space)) && pauseTime < 0)
            {
                WriteLevel();
                pauseTime = 7f;
            }

            oldState = state;
           
            base.Update(gameTime);
  
        }

        private void WriteLevel()
        {
           int numberLevel = 1;
           string pathFile;
           string folderName = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
           string pathDirerectory = Path.Combine(folderName, "Arcanoid");
           string pathSubDirectory = Path.Combine(pathDirerectory, "UserLevel");

           pathFile = Path.Combine(pathSubDirectory, "Level" + numberLevel + ".txt");

     
            if (!(Directory.Exists(pathDirerectory)))
            {
                Directory.CreateDirectory(pathDirerectory);
            }

            if (Directory.Exists(pathDirerectory) && !(Directory.Exists(pathSubDirectory)))
            {
                Directory.CreateDirectory(pathSubDirectory);
            }

            Validator(ref numberLevel, ref pathFile, pathSubDirectory);
          

           FileStream fileStream = new FileStream(pathFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

            StreamWriter writer = new StreamWriter(fileStream);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    writer.Write(level[i, j]);
                }

                writer.WriteLine();
            }

            help.Save = true;

            writer.Close();
        }

        private static void Validator(ref int numberLevel, ref string pathFile, string pathSubDirectory)
        {
            if (!File.Exists(pathFile))
            {
                return;
            }

            if (File.Exists(pathFile))
            {
                numberLevel++;
                pathFile = Path.Combine(pathSubDirectory, "Level" + numberLevel + ".txt");
            }

             Validator(ref numberLevel, ref pathFile, pathSubDirectory);

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Grid, new Vector2(0, 0), Color.White);

            help.Draw(spriteBatch);
            help.SaveDraw(spriteBatch);

            spriteBatch.DrawString(help.font, @"Показати допомогу - F1 ",new Vector2(10,490), Color.Black);

        
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if(bloc.lsBloc[i,j] != null)
                    bloc.lsBloc[i,j].DrawList(spriteBatch);
                }
            }

            if (!Pointer.delete)
            {
                bloc.Draw(spriteBatch);
            }

            pointer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
