using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid
{
    class Help
    {
        private SpriteFont font;
        public List<Texture2D> Help_ListBonus { get; set; }
        private Vector2[] Help_BonusPosition;
        private Vector2[] Help_BonusPosString;
        private KeyboardState state;
        private KeyboardState oldState;
        private List<string> Help_ListBonusString;
        Vector2 Startposition = new Vector2(-50, 10);
        Vector2 StartpositionString = new Vector2(300, 15);
        public int indexUpdateDraw = 1;

        public List<Texture2D> Help_ListBloc { get; set; }
        private Vector2[] Help_BlocPosition;
        private List<string> Help_ListBlocString;
        private Vector2[] Help_BlocPosString;

        private List<string> Credits;
        private Vector2[] positionCredits;

        private Texture2D Backgrounds;
        private Texture2D DevBackground;
        private Texture2D KeyboardTexture;
        private Texture2D KeyboardTextureFon;

        Color color;
        Color colorText;
        Color colorBloc;
        Color colorTextBloc;

        public Help()
        {
            Credits = new List<string>(2);

            Help_ListBonus = new List<Texture2D>();    // список усіх бонусів
            Help_BonusPosition = new Vector2[14];      // масив векторів / початкових позицій

            Help_ListBonusString = new List<string>(14); // список пояснень бонусів
            Help_BonusPosString = new Vector2[14];       //  початкові позиції/вектори

            Help_BlocPosition = new Vector2[8];            // список позицій /векторів
            Help_ListBloc = new List<Texture2D>(8);         // список усіх блоків / текстур
            Help_ListBlocString = new List<string>(8);     // список пояснень блоків
            Help_BlocPosString = new Vector2[8];
            positionCredits = new Vector2[2];

            Initialize_4();
            Initialize();
            Initialize_2();


        }

        public void Initialize_2()
        {

            Startposition = new Vector2(-50, 10);
            StartpositionString = new Vector2(300, 10);

            for (int index = 0; index < 8; index++)
            {
                Help_BlocPosition[index] = Startposition;
                Help_BlocPosString[index] = StartpositionString;

                Startposition.Y += 58f;
                StartpositionString.Y += 57f;
                Help_ListBlocString.Add("Блок розбиваэться за один удар м'ча , може мiстити бонус.\nЗа нього нараховуэться 20 очок!");
                Help_ListBlocString.Add("Блок розбиваэться за один удар м'ча , може мiстити бонус.\nЗа нього нараховуэться 10 очок!");
                Help_ListBlocString.Add("Блок не розбиваэться  :( , \nцей блок можуть розбити тiльки кулi якими стрiляэ ракетка! ");
                Help_ListBlocString.Add("Блок розбиваэться за 5 ударiв м'ча , iнодi мiстить бонус.\nЗа нього нараховуэться 40 очок!");
                Help_ListBlocString.Add("Блок розбиваэться за 1 удар м'ча , мiстить бонус. \nЗа нього нараховуэться 40 очок!");
                Help_ListBlocString.Add("Блок розбиваэться за 2 удари м'ча , iнодi мiстить бонус.\nЗа нього нараховуэться 40 очок!");
                Help_ListBlocString.Add("Блок розбиваэться за 3 удари м'ча , iнодi мiстить бонус. \nЗа нього нараховуэться 40 очок!");
                Help_ListBlocString.Add("Блок розбиваэться за 4 удари м'ча , iнодi мiстить бонус. \nЗа нього нараховуэться 40 очок!");

            }

            colorBloc = new Color(0, 0, 0, 0);
            colorTextBloc = new Color(0, 0, 0, 0);
        }

        private void Initialize_4()
        {
            // Initialize_2();
            positionCredits[0] = new Vector2(350, 660);
            positionCredits[1] = new Vector2(385, 700);
            Credits.Add(" Програмiст та дизайнер ");
            Credits.Add(" Iльчук Михайло ");
        }

        private void Initialize()
        {
            Startposition = new Vector2(-50, 10);
            StartpositionString = new Vector2(300, 15);

            for (int index = 0; index < 14; index++)
            {
                Help_BonusPosition[index] = Startposition;
                Help_BonusPosString[index] = StartpositionString;

                Startposition.Y += 46f;
                StartpositionString.Y += 46f;
            }

            Help_ListBonusString.Add("Бонус збiльшуэ ракетку  у два рази! Наступний раз на 10 пiкс.");
            Help_ListBonusString.Add("Бонус зменшуэ ракетку  у два рази! Наступний раз  на 10 пiкс.");
            Help_ListBonusString.Add("Бонус додаэ 1000 очок . ");
            Help_ListBonusString.Add("Бонус вiднiмаэ 1000 очок .  :( ");
            Help_ListBonusString.Add("Бонус збiльшуэ швидкiсть м'яча.Наступний раз швидкiсть м'яча збiльшиться. ");
            Help_ListBonusString.Add("Бонус зменшуэ швидкiсть м'яча.Наступний раз швидкiсть м'яча зменшиться. ");
            Help_ListBonusString.Add("Бонус збiльшуэ швидкiсть ракетки.Наступний раз швидкiсть ,збiльшиться.");
            Help_ListBonusString.Add("Бонус зменшуэ швидкiсть ракетки.Наступний раз швидкiсть ,зменшиться.");
            Help_ListBonusString.Add("Бонус додаэ життя , якщо життiв ' 3 ' бонус недодаэ ще одне .\n");
            Help_ListBonusString.Add("Бонус дозволяэ ракетцi стрiляти кулями , якi розбивають будь-якi блоки.");
            Help_ListBonusString.Add("Бонус не даэ м'ячу впасти , вiн активний 10 с.");
            Help_ListBonusString.Add("Бонус збiльшуэ м'яч.При наступному разi, м'яч не збiльшуэться.");
            Help_ListBonusString.Add("Бонус зменшуэ м'яч.При наступному разi, м'яч не зменшуэться.");
            Help_ListBonusString.Add("М'яч стаэ вогняний ,розбиваэ всi блоки крiм зелених! 5> Активний >10 с.");

            color = new Color(0, 0, 0, 0);
            colorText = new Color(0, 0, 0, 0);
        }

        public void HelpUpdate()
        {
            state = Keyboard.GetState();

            if (indexUpdateDraw == 2)
                Update_1();

            if (indexUpdateDraw == 1)
                Update_2();

            if (indexUpdateDraw == 4)
                Update_4();

            if (state.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                if (indexUpdateDraw == 2 || indexUpdateDraw == 1 || indexUpdateDraw == 3)
                    indexUpdateDraw++;
            }

            if (state.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                if (indexUpdateDraw == 2 || indexUpdateDraw == 3 || indexUpdateDraw == 4)
                    indexUpdateDraw--;
            }

            oldState = state;
        }

        private void Update_1()
        {

            Initialize_2();
            Initialize_4();

            for (int index = 0; index < 14; index++)
            {
                if (Help_BonusPosition[index].X <= 50)
                    Help_BonusPosition[index].X += 1.5f;

                if (Help_BonusPosString[index].X >= 150)
                    Help_BonusPosString[index].X -= 2f;
            }

            if (Help_BonusPosition[0].X >= 10 && color.R <= 250)
            {
                color.R += 3;
                color.G += 3;
                color.B += 3;
                color.A += 3;
            }

            if (colorText.A < 255)
                colorText.A += 3;
        }

        private void Update_2()
        {
            Initialize();
            Initialize_4();

            for (int index = 0; index < 8; index++)
            {
                if (Help_BlocPosition[index].X <= 50)
                    Help_BlocPosition[index].X += 1.5f;

                if (Help_BlocPosString[index].X >= 150)
                    Help_BlocPosString[index].X -= 2f;
            }

            if (Help_BlocPosition[0].X >= 10 && colorBloc.R <= 250)
            {
                colorBloc.R += 3;
                colorBloc.G += 3;
                colorBloc.B += 3;
                colorBloc.A += 3;
            }

            if (colorTextBloc.A < 255)
                colorTextBloc.A += 3;

        }

        private void Update_4()
        {
            Initialize();

            for (int index = 0; index < 2; index++)
            {
                positionCredits[index].Y -= 2;
            }

            if (positionCredits[1].Y < 0)
                Initialize_4();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (indexUpdateDraw == 2)
                Draw_1(spriteBatch);

            if (indexUpdateDraw == 1)
                Draw_2(spriteBatch);

            if (indexUpdateDraw == 3)
                Draw_3(spriteBatch);

            if (indexUpdateDraw == 4)
                Draw_4(spriteBatch);

            spriteBatch.End();
        }

        private void Draw_1(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgrounds, Vector2.Zero, Color.White);

            for (int index = 0; index < 14; index++)
            {
                spriteBatch.Draw(Help_ListBonus[index], Help_BonusPosition[index], color);
                spriteBatch.DrawString(font, Help_ListBonusString[index], Help_BonusPosString[index], color);
            }
        }

        private void Draw_2(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgrounds, Vector2.Zero, Color.White);

            for (int index = 0; index < 8; index++)
            {
                spriteBatch.Draw(Help_ListBloc[index], Help_BlocPosition[index], colorBloc);
                spriteBatch.DrawString(font, Help_ListBlocString[index], Help_BlocPosString[index], colorBloc);
            }
            spriteBatch.DrawString(font, "Рухайтесь 'вправо' i 'влiво' , щоб мiняти сторiнки :)   Esc - Вихiд", new Vector2(10, 620), colorBloc);
        }

        private void Draw_3(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(KeyboardTextureFon, new Vector2(0, 100), Color.White);
            spriteBatch.Draw(KeyboardTexture, new Vector2(0,100), Color.White);
            spriteBatch.DrawString(font, "Рухайтесь 'вправо' i 'влiво' , щоб мiняти сторiнки :)   Esc - Вихiд", new Vector2(10, 620), Color.White);
        }

        private void Draw_4(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DevBackground, Vector2.Zero, Color.White);

            for (int index = 0; index < 2; index++)
            {
                spriteBatch.DrawString(font, Credits[index], positionCredits[index], Color.DarkRed);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Game-Font/HelpFont");

            Backgrounds = Content.Load<Texture2D>("Texture/Backgrounds/HelpBackground");
            DevBackground = Content.Load<Texture2D>("Texture/Backgrounds/DevBackground");

            Help_ListBloc.Add(Content.Load<Texture2D>("Texture/Help/type1"));
            Help_ListBloc.Add(Content.Load<Texture2D>("Texture/Help/type2"));
            Help_ListBloc.Add(Content.Load<Texture2D>("Texture/Help/type3"));
            Help_ListBloc.Add(Content.Load<Texture2D>("Texture/Help/type4"));

            KeyboardTexture = Content.Load<Texture2D>("Texture/Help/keyboards");
            KeyboardTextureFon = Content.Load<Texture2D>("Texture/Help/FonKeys");
        }

    }
}
