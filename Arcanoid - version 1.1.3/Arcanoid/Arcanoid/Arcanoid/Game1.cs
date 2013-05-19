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
using Arcanoid.MenuSystem;
using Arcanoid.AudioManager;
using Microsoft.Xna.Framework.Storage;
using System.Threading;

namespace Arcanoid
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MenuItem levelCreate;  // Поле для сторення рівня ... щоб міняти Activ
        LevelCreater.Game1 Creator;  // Потік  створення рівня

        GameData gameData;
        public int[] Level = new int[10];
        public int BlocLevel = 1;
        int currentLevel = 0;
        string[] timeGame = new string[10];
        float timegameSeconds = 0;
        float timeMinutes = 0;

        Rectangle ScreenRectangle;

        public int Width;
        public int Height;

        Texture2D LoadingTexture;
        bool Loading = false;
        float loading =  0.3f;
        Color LoadColor = new Color(255, 255, 255, 255);

        Help help = new Help();

        bool pause = false;
        float pauseTime = 3;
        float scale = 2;

        ListLevel listLevel = new ListLevel();
        ListLevel MainlistLevel = new ListLevel();
        ListLevelBloc listLevBloc = new ListLevelBloc();

        bool mainLevel = false;

        Menu menu;
        Settings Settings;
        MenuEsc menuEsc;
        OptionLevel optionLevel;
        MenuState menuState = MenuState.Basic;
        GameState gameState = GameState.Menu;

        //----------Ракетка----------
        Texture2D paddleTexture;
        Paddle paddle;
        //---------/Ракетка----------

        //-------М'яч----------------
        Ball ball;
        Texture2D ballTexture;
        Texture2D ballFireTexture;
        //-------/М'яч---------------

        //-------Блоки---------------
        public Texture2D brick;
        List<Brick> bricks;
        Texture2D[] brickTextures;
        Texture2D[] brickTextures2;
        //-------/Блоки---------------

        SpriteFont font;
        int Score;
        int[] ScoreList = new int[10];
        public int Lives = 3;

        //---------Очки-------------
        Texture2D TexturePoints_20;
        Texture2D TexturePoints_10;
        Texture2D TexturePoints_40;
        Points_Anim point;
        List<Points_Anim> points;
        Vector2 Position_points;
        Vector2 Stop_Position_points;
        //---------/Очки-------------

        Texture2D Background;
        Texture2D MainBackground;

        //--------- Бонуси------------
        List<ObjectBonus> bonuses;
        ObjectBonus Bonus;
        Texture2D BonusTextureMax;
        Texture2D BonusTextureMin;
        Texture2D Bonus1000;
        Texture2D Bonus_1000;
        Texture2D ballSpeedPlus;
        Texture2D ballSpeedMinus;
        Texture2D paddleSpeedPlus;
        Texture2D paddleSpeedMinus;
        Texture2D LivesPlus;
        Texture2D TextrShoot;
        Texture2D BallBig;
        Texture2D BallSmall;

        Texture2D FireBallBonus;
        float TimeBonusFire = 5;
        bool ballFireStart;

        Texture2D BonusTimeSt;
        Texture2D TimeStopTexture;
        bool bonusVisible = false;
        float TimeBonusVisible = 10;
        Rectangle posBonusTime;
        bool start;
        //---------/Бонуси------------

        
        Texture2D textr;
        Texture2D textr2;
        double elapsed;
      
        int CountBricks;
        int CountBricks_hard;

        int test;


        //-------Bullets----------
        List<Bullets> bullets = new List<Bullets>();
        int timeBetweenShots = 300;
        int shotTimer = 0;
        bool IsShooting = false;
        int st = 0;
        Texture2D bul;
        Bullets buletsBul;
        //-------/Bullets----------
    
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Width = graphics.PreferredBackBufferWidth = 910;
            Height = graphics.PreferredBackBufferHeight = 650;
            ScreenRectangle = new Rectangle(0, 0, Width, Height);      
        }

        protected override void Initialize()
        {
            OptionLevel optionLevel = new OptionLevel(this);
            menu = new Menu();
            menuEsc = new MenuEsc();
            Settings = new Settings();

            ///Очистка всього із коду
            ///
       /*    Level[0] = 1;

            for(int j = 0; j< 10; j++ )
            {
                timeGame[j] = "00 : 00";
                ScoreList[j] = 0;
            }

            gameData = new GameData(BlocLevel, Level,currentLevel,timeGame,ScoreList);
            FilesConfig.Serialize(gameData);*/


            LoadGame();

            #region Menu

            MenuItem newGame = new MenuItem("Нова гра",new Vector2(100,100));
            MenuItem resumeGame = new MenuItem("Продовжити", new Vector2(100, 150));
            MenuItem levels = new MenuItem("Рiвнi", new Vector2(100, 200));
            MenuItem mainLevels = new MenuItem("Власнi рiвнi", new Vector2(100, 250));
            levelCreate = new MenuItem("Створити рiвень", new Vector2(100, 300));
            MenuItem settings = new MenuItem("Налаштування", new Vector2(100, 350));
            MenuItem help = new MenuItem("Допомога", new Vector2(100, 400));
            MenuItem exitGame = new MenuItem("Вихiд", new Vector2(100, 450));

            resumeGame.Active = false;
            newGame.Click += new EventHandler(newGame_Click);
            resumeGame.Click += new EventHandler(resumeGame_Click);
            levels.Click += new EventHandler(levels_Click);
            mainLevels.Click += new EventHandler(mainLevels_Click);
            levelCreate.Click += new EventHandler(levelCreate_Click);
            settings.Click += new EventHandler(settings_Click);
            help.Click += new EventHandler(help_Click);
            exitGame.Click += new EventHandler(exitGame_Click);

            menu.Items.Add(newGame);
            menu.Items.Add(resumeGame);
            menu.Items.Add(levels);
            menu.Items.Add(mainLevels);
            menu.Items.Add(levelCreate);
            menu.Items.Add(settings);
            menu.Items.Add(help);
            menu.Items.Add(exitGame);

#endregion

            #region ESC- Menu

            MenuItem resumeGameEsc = new MenuItem("Продовжити", new Vector2(910, 200));
            MenuItem exitGameEsc = new MenuItem("Завершити гру", new Vector2(910, 250));
            MenuItem GameEscMenu = new MenuItem("Вийти в головне меню", new Vector2(910, 300));

            resumeGameEsc.Click += new EventHandler(resumeGameEsc_Click);
            exitGameEsc.Click += new EventHandler(exitGameEsc_Click);
            GameEscMenu.Click += new EventHandler(GameEscMenu_Click);

            menuEsc.Items.Add(resumeGameEsc);
            menuEsc.Items.Add(exitGameEsc);
            menuEsc.Items.Add(GameEscMenu);

            #endregion

            #region Settings

            MenuItem resetSaving = new MenuItem("Скинути всi збереження", new Vector2(100, 300));
            MenuItem resetMainLevel = new MenuItem("Видалити власнi рiвнi", new Vector2(100, 350));
            resetSaving.Click += new EventHandler(resetSaving_Click);
            resetMainLevel.Click += new EventHandler(resetMainLevel_Click);

            Settings.Items.Add(resetSaving);
            Settings.Items.Add(resetMainLevel);

            #endregion

            #region Start-Position

            int pos = 100;

           for (int i = 0; i < 10; i++)
           {
                MenuItem level = new MenuItem("Рiвень " + (i + 1) , new Vector2(910, pos) , i+1 );
                level.Click += new EventHandler(level_Click);
                listLevel.Items.Add(level);
                pos += 50;

           }

            pos = 100;

           for (int i = 0; i < 10; i++)
           {
               MenuItem listBloc = new MenuItem("Блок " + (i + 1), new Vector2(910, pos), i + 1 );
               listBloc.Click += new EventHandler(listLevBloc_Click);
               listLevBloc.Items.Add(listBloc);
               pos += 50;

           }

            #endregion

           UpdateLevel(pos);

           UpdateLevel_and_BlocList();

         base.Initialize();

        }

        private void UpdateLevel(int pos)
        {
            pos = 100;
            MainlistLevel.Items.Clear();
            for (int i = 0; i < FilesConfig.MainLevelsLoad(); i++)
            {
                MenuItem Mainlevel = new MenuItem("Рiвень " + (i + 1), new Vector2(410, pos), i + 1);
                Mainlevel.Click += new EventHandler(Mainlevel_Click);
                MainlistLevel.Items.Add(Mainlevel);
                pos += 50;
            }
        }

        void level_Click(object sender, EventArgs e)
        {
            CurrentLevel(listLevel.CurrentItem + 1);
        }

        void Mainlevel_Click(object sender, EventArgs e)
        {
            mainLevel = true;

            CurrentLevel(MainlistLevel.CurrentItem + 1);

        }

        void levelCreate_Click(object sender, EventArgs e)
        {
            Thread threadCreator = new Thread(() =>
            {
                Creator = new LevelCreater.Game1();
                Creator.Run();
            });


            Thread threadGame = new Thread(() =>
            {
                Arcanoid.Game1 game = new Arcanoid.Game1();
                game.Run();
            });

            threadGame.IsBackground = true;

            threadCreator.IsBackground = true;
            threadCreator.Start();
            // thread.Join();

            menu.CurrentItem = 3;
            levelCreate.Active = false;
        }

        private void UpdateActiv()
        {
            if (Creator != null && Creator.Open)
             levelCreate.Active = true;
        }
        
        void exitGame_Click(object sender, EventArgs e)
        {
            AudioManager.AudioManager.StopMenuMusic();
            this.Exit();
        }

        void help_Click(object sender, EventArgs e)
        {
            menuState = MenuState.Help;
        }
        void listLevBloc_Click(object sender, EventArgs e)
        {
            listLevel.CurrentItem = 0;

          //  listLevel.startPosition = true;
          //  menu.startPosition = true;
            listLevel.Pause = 0.3f;

            menuState = MenuState.LevelList;

        }

        void levels_Click(object sender, EventArgs e) 
        {
           // listLevel.startPosition = true;
           // menu.startPosition = true;
            listLevel.Pause = 0.3f;

            mainLevel = false;
            menuState = MenuState.LevelListBloc;
        }

        void mainLevels_Click(object sender, EventArgs e)
        {
            if (MainlistLevel.Items.Count == 0)
            {
                MainlistLevel.mainLevelNone = true;
            }
            else
                MainlistLevel.mainLevelNone = false;


            listLevel.Pause = 0.3f;

            UpdateLevel(100);
            menuState = MenuState.MainLevelList;
        }

        void settings_Click(object sender, EventArgs e) 
        {
            menu.Pause = 0.5f;      
            menuState = MenuState.Settings;
        }

        void resetSaving_Click(object sender, EventArgs e) 
        {
            FilesConfig.Reset();
          /*  GameData gamedata = new GameData(1, 1);
            FilesConfig.Serialize(gamedata);*/
            LoadGame();

            menu.Items[1].Active = false;
            UpdateLevel_and_BlocList();
            gameState = GameState.Menu;
            menuState = MenuState.Basic;
        }

        void resetMainLevel_Click(object sender, EventArgs e)
        {
            FilesConfig.ResetMainLevel();

            menu.Items[1].Active = false;     
            gameState = GameState.Menu;
            menuState = MenuState.Basic;
        }

        void resumeGame_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
            pause = true;
        }


        void exitGameEsc_Click(object sender, EventArgs e)
        {
            menuEsc.startPosition = true;

            bricks.Clear();
            points.Clear();

            menu.CurrentItem = 0;
            menu.Items[1].Active = false;
            
            gameState = GameState.Menu;
            menuState = MenuState.Basic;

        }

        void GameEscMenu_Click(object sender, EventArgs e)
        {
            menuEsc.startPosition = true;
           
            menu.Items[1].Active = true;
            gameState = GameState.Menu;
            menuState = MenuState.Basic;
        }


        void resumeGameEsc_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
            pause = true;
        }

        void newGame_Click(object sender, EventArgs e)
        {

            menu.Items[1].Active = true;
            gameState = GameState.Game;
            mainLevel = false;

            StartGame(true);
            CreateLevel(gameData.level[gameData.indexLevel]);

            Score = 0;
        }


        private void CurrentLevel(int current)
        {
            menu.Items[1].Active = true;

            menuState = MenuState.Basic;
            gameState = GameState.Game;
            StartGame(true);
            CreateLevel(current);
            Score = 0;
          
        }

        private void UpdateLevel_and_BlocList()
        {

            for (int i = gameData.bloc; i < listLevBloc.Items.Count; i++)
            {
                listLevBloc.Items[i].Active = false;
            }

            for (int i = gameData.level[gameData.indexLevel]; i < listLevel.Items.Count; i++)
            {
                listLevel.Items[i].Active = false;
               
            }
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            menu.LoadContent(Content);
            menuEsc.LoadContent(Content);

            Settings.LoadContent(Content);

            listLevel.LoadContent(Content);
            MainlistLevel.LoadContent(Content);
            listLevBloc.LoadContent(Content);
            help.LoadContent(Content);

           // help.HelpTextureList.Add(paddleTexture);
          //  help.HelpTextureList.Add(paddleTexture);

            optionLevel = new OptionLevel(ScreenRectangle,Content);

            paddleTexture = Content.Load<Texture2D>("Texture/paddle");   
            paddle = new Paddle(paddleTexture, ScreenRectangle);

            bul = Content.Load<Texture2D>("Texture/bul");
            buletsBul = new Bullets(bul);

            ballTexture = Content.Load<Texture2D>("Texture/ball");
            ballFireTexture = Content.Load<Texture2D>("Texture/Fireball");
            ball = new Ball(ballTexture, ScreenRectangle);

           // help.HelpTextureList.Add(ballTexture);
           // help.HelpTextureList.Add(ballFireTexture);


            font = Content.Load<SpriteFont>("Game-Font/GameFont");

            brickTextures = new Texture2D[5];

            brickTextures[0] = Content.Load<Texture2D>("Texture/Bricks/brick_5");
            brickTextures[1] = Content.Load<Texture2D>("Texture/Bricks/brick_4");
            brickTextures[2] = Content.Load<Texture2D>("Texture/Bricks/brick_3");
            brickTextures[3] = Content.Load<Texture2D>("Texture/Bricks/brick_2");
            brickTextures[4] = Content.Load<Texture2D>("Texture/Bricks/brick");

            brickTextures2 = new Texture2D[4];

         
            brickTextures2[0] = Content.Load<Texture2D>("Texture/Bricks/brick_1_4");
            brickTextures2[1] = Content.Load<Texture2D>("Texture/Bricks/brick_1_3");
            brickTextures2[2] = Content.Load<Texture2D>("Texture/Bricks/brick_1_2");
            brickTextures2[3] = Content.Load<Texture2D>("Texture/Bricks/brick_1_1");
            
           
         
               listLevel.Ok = Content.Load<Texture2D>("Texture/Ok");
           

            textr = Content.Load<Texture2D>("Texture/Bricks/test3");
            textr2 = Content.Load<Texture2D>("Texture/Bricks/test2");


            TexturePoints_20 = Content.Load<Texture2D>("Texture/Points/20_points");
            TexturePoints_10 = Content.Load<Texture2D>("Texture/Points/10_points");
            TexturePoints_40 = Content.Load<Texture2D>("Texture/Points/40_points");

           // help.HelpTextureList.Add(TexturePoints_20);
           // help.HelpTextureList.Add(TexturePoints_10);
           // help.HelpTextureList.Add(TexturePoints_40);

            for(int i = 0 ; i < 4; i++)
            {
                help.Help_ListBloc.Add(brickTextures2[i]);
            }

            BonusTextureMax = Content.Load<Texture2D>("Texture/Bonuses/bonus-max");            help.Help_ListBonus.Add(BonusTextureMax);
            BonusTextureMin = Content.Load<Texture2D>("Texture/Bonuses/bonus-min");            help.Help_ListBonus.Add(BonusTextureMin);
            Bonus1000 = Content.Load<Texture2D>("Texture/Bonuses/+1000");                      help.Help_ListBonus.Add(Bonus1000);
            Bonus_1000 = Content.Load<Texture2D>("Texture/Bonuses/-1000");                     help.Help_ListBonus.Add(Bonus_1000);
            ballSpeedPlus = Content.Load<Texture2D>("Texture/Bonuses/ballSpeed++");            help.Help_ListBonus.Add(ballSpeedPlus);
            ballSpeedMinus = Content.Load<Texture2D>("Texture/Bonuses/ballSpeed--");           help.Help_ListBonus.Add(ballSpeedMinus);
            paddleSpeedPlus = Content.Load<Texture2D>("Texture/Bonuses/paddleSpeed++");        help.Help_ListBonus.Add(paddleSpeedPlus);
            paddleSpeedMinus = Content.Load<Texture2D>("Texture/Bonuses/paddleSpeed--");       help.Help_ListBonus.Add(paddleSpeedMinus);
            LivesPlus = Content.Load<Texture2D>("Texture/Bonuses/Lives++");                    help.Help_ListBonus.Add(LivesPlus);
            TextrShoot = Content.Load<Texture2D>("Texture/Bonuses/shoot");                     help.Help_ListBonus.Add(TextrShoot);
            TimeStopTexture = Content.Load<Texture2D>("Texture/timeStop");                    // help.Help_ListBonus.Add(TimeStopTexture);
            BonusTimeSt = Content.Load<Texture2D>("Texture/Bonuses/bonus-Timestop");           help.Help_ListBonus.Add(BonusTimeSt);
            posBonusTime = new Rectangle(0,(ScreenRectangle.Height - TimeStopTexture.Height - 5),ScreenRectangle.Width, TimeStopTexture.Height);
            BallBig = Content.Load<Texture2D>("Texture/Bonuses/ballBig");                      help.Help_ListBonus.Add(BallBig);
            BallSmall = Content.Load<Texture2D>("Texture/Bonuses/ballSmall");                  help.Help_ListBonus.Add(BallSmall);
            FireBallBonus = Content.Load<Texture2D>("Texture/Bonuses/FireBallBonus");          help.Help_ListBonus.Add(FireBallBonus);

            LoadingTexture = Content.Load<Texture2D>("Texture/Loading");
            Background = Content.Load<Texture2D>("Texture/Backgrounds/GameBackground");
            MainBackground = Content.Load<Texture2D>("Texture/Backgrounds/MainBackground");

            // TODO: use this.Content to load your game content here         
        }

        private void StartGame(bool parametr)
        {
            if (parametr)
            {
                paddle.SetInStartPosition();
                ball.SetInStartPosition(paddle.GetBounds);
                bonusVisible = false;
                ballFireStart = false;
                Lives = 3;
                timegameSeconds = 0;
                timeMinutes = 0;
                Score = 0;
                optionLevel.died = false;
                optionLevel.lose = false;
                LoadGame();
                this.Window.Title = "Гра завантаженна " + gameData.level[gameData.indexLevel].ToString() + " --- " + gameData.bloc;

            }
            else
            {
                paddle.SetInStartPosition();
                ball.SetInStartPosition(paddle.GetBounds);
                ballFireStart = false;
                bonusVisible = false;
               
            }

        }

        private void SaveGame()
        {

            gameData.bloc = BlocLevel;
            gameData.level = Level;
            gameData.indexLevel = currentLevel;
            gameData.timeGame = timeGame;
            gameData.Score = ScoreList;

            FilesConfig.Serialize(gameData);

            if (listLevel.CurrentItem == 9 && listLevel.Items[listLevel.CurrentItem].Active)
            {
                NewBlocGame();
                return;
            }
                if (listLevel.Items[listLevel.CurrentItem + 1].Active)
                    return;
           
            else
            {
                Level = gameData.level;
                if (listLevel.Items[Level[gameData.indexLevel]].Active == false)
                {
                    listLevel.Items[Level[gameData.indexLevel]].Active = true;
                    Level[gameData.indexLevel]++;
                }
            }
            if (Level[gameData.indexLevel] == 10)
            {
                listLevBloc.Items[gameData.indexLevel].Active = false;
                listLevBloc.Items[BlocLevel++].Active = true ;
                gameData.indexLevel = ++currentLevel;
                listLevBloc.CurrentItem = currentLevel;
            
                gameData.level[gameData.indexLevel] = 1;

            }

             gameData = new GameData(BlocLevel,Level,currentLevel,timeGame,ScoreList);
             FilesConfig.Serialize(gameData);
             UpdateLevel_and_BlocList();
             this.Window.Title = "Гра збережена " + gameData.level[gameData.indexLevel].ToString() + "  " + gameData.bloc;
        }

        private void NewBlocGame()
        {
            listLevBloc.Items[gameData.indexLevel].Active = false;
            listLevBloc.Items[BlocLevel++].Active = true;
            gameData.indexLevel = ++currentLevel;
            listLevBloc.CurrentItem = currentLevel;
            gameData.level[gameData.indexLevel] = 1;
            Level = gameData.level;
            gameData = new GameData(BlocLevel, Level, currentLevel,timeGame,ScoreList);
            FilesConfig.Serialize(gameData);
            UpdateLevel_and_BlocList();
            return;
        }

        private void LoadGame()
        {   
            FilesConfig.CheckAndCreateFile();
            gameData = FilesConfig.Deserialize();

            BlocLevel = gameData.bloc;
            Level = gameData.level;
            currentLevel  = gameData.indexLevel;
            timeGame = gameData.timeGame;
            ScoreList = gameData.Score;

            UpdateLevel_and_BlocList();
        }

        private void CreateLevel(int currentLevel)
        {
            string[] lines;

         /*   if (currentLevel > 3)
                currentLevel = 1;*/

            bricks = new List<Brick>();
            
            points = new List<Points_Anim>();
            int x = 0, y = 0;
            CountBricks = 0;
            CountBricks_hard = 0;

            if (mainLevel)
            {
               lines = File.ReadAllLines(FilesConfig.pathSDirect + "/Level" + currentLevel + ".txt");
            }
            else
                lines = File.ReadAllLines("Content//Level-Game/level" + currentLevel + ".txt");

            if (gameData.bloc == 2)
            {
                object ob = new object();
                ob = gameData.indexLevel + "" + gameData.level[gameData.indexLevel];

                lines = File.ReadAllLines("Content//Level-Game/level" + ob + ".txt");
            }

            else  if (gameData.bloc > 2)
            {
                object ob = new object();
                ob = gameData.bloc + "" + gameData.level[gameData.indexLevel];

                lines = File.ReadAllLines("Content//Level-Game/level" + ob + ".txt");
            }

            foreach (string line in lines)
            {
                foreach (char c in line)
                {

                    if (c == 'X' || c == '4' )
                    {
                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(brickTextures, 5, rect, BrickType.Mild);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_40, Position_points, Stop_Position_points, false, false);

                        points.Add(point);

                        bricks.Add(brick);
                        CountBricks++;

                    }

                    if (c == '5')
                    {
                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(brickTextures2, 4, rect, BrickType.Mild);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_40, Position_points, Stop_Position_points, false, false);

                        points.Add(point);

                        bricks.Add(brick);
                        CountBricks++;

                    }

                    if (c == '6')
                    {
                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(brickTextures2, 3, rect, BrickType.Mild);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_40, Position_points, Stop_Position_points, false, false);

                        points.Add(point);

                        bricks.Add(brick);
                        CountBricks++;

                    }

                    if (c == '7')
                    {
                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(brickTextures2, 2, rect, BrickType.Mild);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_40, Position_points, Stop_Position_points, false, false);

                        points.Add(point);

                        bricks.Add(brick);
                        CountBricks++;

                    }

                    if (c == '8')
                    {
                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(brickTextures2, 1, rect, BrickType.Mild);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_40, Position_points, Stop_Position_points, false, false);

                        points.Add(point);

                        bricks.Add(brick);
                        CountBricks++;

                    }

                    if (c == 'Y' || c == '3')
                    {

                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(brickTextures, 5, rect, BrickType.Hard);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_20, Position_points, Stop_Position_points, false, false);

                        points.Add(point);

                        bricks.Add(brick);
                        CountBricks_hard++;

                    }

                    if (c == 'Z' || c == '2')
                    {

                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(5, 10, rect, textr, BrickType.BreaksBloc);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_10, Position_points, Stop_Position_points, false, false);

                        points.Add(point);

                        bricks.Add(brick);
                        CountBricks++;
                    }

                    if (c == 'C' || c=='1')
                    {

                        Rectangle rect = new Rectangle(x, y, 70, 40);
                        Brick brick = new Brick(4, 10, rect, textr2, BrickType.AnimRed);

                        Position_points = new Vector2(rect.X + 10, rect.Y + 10);
                        Stop_Position_points = new Vector2(0, rect.Y + 10);
                        point = new Points_Anim(TexturePoints_20, Position_points, Stop_Position_points, false, false);

                        points.Add(point);


                        bricks.Add(brick);
                        CountBricks++;
                    }

                    x += 70;

                }

                x = 0;
                y += 40;
            }
           
            BonusPosition();
        }

        public void BonusPosition()
        {
            Random rand = new Random();
            int i = 0;
            int r = 0;
            int ok = 0;
            int okk = 0;
            bonuses = new List<ObjectBonus>(bricks.Count);

            while (i <= bricks.Count)
            {
                bonuses.Add(null);
                i++;
            }

            i = 0;
            while (i <= CountBricks)
            {
                if (r <= (int) (CountBricks/2) + 10)
                {
                    int k = rand.Next(0, CountBricks - 1);
                    int randBonus = rand.Next(1, 15);

                    Vector2 pos = new Vector2(bricks[k].position.X, bricks[k].position.Y);
                    Vector2 posStop = new Vector2(0, bricks[k].position.Y);

                    switch (randBonus) 
                    {

                        case 1: 
                            {
                                Bonus = new ObjectBonus(BonusTextureMax, pos, posStop, randBonus);
                                break;
                            }
                        case 2:
                            {
                                Bonus = new ObjectBonus(BonusTextureMin, pos, posStop, randBonus);
                                break;
                            }
                        case 3:
                            {
                                Bonus = new ObjectBonus(Bonus1000, pos, posStop, randBonus);
                                break;
                            }
                        case 4:
                            {
                                Bonus = new ObjectBonus(Bonus_1000, pos, posStop, randBonus);
                                break;
                            }
                        case 5:
                            {
                                Bonus = new ObjectBonus(ballSpeedPlus, pos, posStop, randBonus);
                                break;
                            }
                        case 6:
                            {
                                Bonus = new ObjectBonus(ballSpeedMinus, pos, posStop, randBonus);
                                break;
                            }
                        case 7:
                            {
                                Bonus = new ObjectBonus(paddleSpeedPlus, pos, posStop, randBonus);
                                break;
                            }
                        case 8:
                            {
                                Bonus = new ObjectBonus(paddleSpeedMinus, pos, posStop, randBonus);
                                break;
                            }
                        case 9:
                            {
                                Bonus = new ObjectBonus(LivesPlus, pos, posStop, randBonus);
                                break;
                            }
                        case 10:
                            {
                                if (ok < 3)
                                {
                                    Bonus = new ObjectBonus(TextrShoot, pos, posStop, randBonus);
                                    ok++;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                        case 11:
                            {
                                Bonus = new ObjectBonus(BonusTimeSt, pos, posStop, randBonus);
                                break;
                            }
                        case 12:
                            {
                                Bonus = new ObjectBonus(BallBig, pos, posStop, randBonus);
                                break;
                            }
                        case 13:
                            {
                                Bonus = new ObjectBonus(BallSmall, pos, posStop, randBonus);
                                break;
                            }
                        case 14:
                            {
                                if (okk < 2)
                                {
                                    Bonus = new ObjectBonus(FireBallBonus, pos, posStop, randBonus);
                                    okk++;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                    }

                    
                    bonuses[k] = Bonus;
                    r++;
                }

                i++;
            }


        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void Loadin(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            loading -= elapsed;

            LoadColor.R -= 5;
            LoadColor.G -= 5;
            LoadColor.B -= 5;
            LoadColor.A -= 5;

            if (loading < 0 && LoadColor.A < 10)
            {
                Loading = true;
            }

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardstate = Keyboard.GetState();

            Paused(gameTime);

            UpdateActiv(); // Перевірка чи поток LevelCreator відкритий , якщо так, поле Створити рівень не активне 
            
            if (!pause)
            {
                if (keyboardstate.IsKeyDown(Keys.Escape) && menuState == MenuState.LevelList)
                {
                    menuState = MenuState.LevelListBloc;
                    listLevel.StartPosition();
                    listLevBloc.StartPosition();

                    loading = 0.3f;
                    Loading = false;
                }

                else if (keyboardstate.IsKeyDown(Keys.Escape) && menuState == MenuState.LevelListBloc)
                {
                    gameState = GameState.Menu;
                    menuState = MenuState.Basic;

                    listLevel.StartPosition();
                    listLevBloc.StartPosition();

                    loading = 0.3f;
                    Loading = false;
                }

                else if (keyboardstate.IsKeyDown(Keys.Escape) && menuState == MenuState.Settings)
                {
                    gameState = GameState.Menu;
                    menuState = MenuState.Basic;
                    loading = 0.3f;
                    Loading = false;
                }

                else if (keyboardstate.IsKeyDown(Keys.Escape) && menuState == MenuState.Help)
                {
                    gameState = GameState.Menu;
                    menuState = MenuState.Basic;
                    help.indexUpdateDraw = 1;
                    loading = 0.3f;
                    Loading = false;
                    help.Initialize_2();
                }

                else if (keyboardstate.IsKeyDown(Keys.Escape) && menuState == MenuState.MainLevelList)
                {
                    gameState = GameState.Menu;
                    menuState = MenuState.Basic;
                    MainlistLevel.mainLevelNone = false;

                    loading = 0.3f;
                    Loading = false;
                }

                if (menuState == MenuState.MainLevelList && !(MainlistLevel.mainLevelNone))
                {
                    Loadin(gameTime);

                    if (Loading)
                         MainlistLevel.MainUpdate(gameTime);
                }

                if (menuState == MenuState.Settings)
                {
                    Loadin(gameTime);

                    if (Loading)
                        Settings.Update(gameTime);
                }

                if (menuState == MenuState.Help)
                {
                    Loadin(gameTime);

                    if(Loading)
                    help.HelpUpdate();
                }

                if (menuState == MenuState.LevelListBloc)
                {
                    Loadin(gameTime);

                    if (Loading)
                    listLevBloc.Update(gameTime);
                }

                 if (menuState == MenuState.LevelList)
                    listLevel.Update(gameTime);

                  if (gameState == GameState.EscMenu)
                      menuEsc.Update(gameTime);

                  if (gameState == GameState.Game)
                  {
                      if (!optionLevel.died && !optionLevel.lose && !optionLevel.win)
                      {
                          float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                          float elap = (float)gameTime.ElapsedGameTime.TotalMinutes;

                          timegameSeconds += elapsed;
                          timeMinutes += elap;

                          if (timegameSeconds >= 59)
                              timegameSeconds = 0;
                      }

                      UpdateGameLogic(gameTime);      
                  }

                  if (gameState == GameState.Menu /*&& menuState == MenuState.Basic*/)
                      AudioManager.AudioManager.StartMenuMusic();
                  else
                      AudioManager.AudioManager.StopMenuMusic();

                  if (gameState == GameState.Menu && menuState == MenuState.Basic)
                      menu.Update(gameTime);
            }
      
            AudioManager.AudioManager.Update();
            
            base.Update(gameTime);
        }

        private void Paused(GameTime gameTime)
        {
            if (pause)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                pauseTime -= elapsed;

                if (pauseTime >= 2.5 && pauseTime <= 3)
                    scale++;

                else if (pauseTime >= 1.5 && pauseTime <= 2)
                         scale++;

                else if (pauseTime >= 0.5 && pauseTime <= 1)
                         scale++;

                if (scale >= 32)
                    scale = 2;

                if (pauseTime <= 0)
                {
                    pause = false;
                    pauseTime = 3;
                }
            }
        }

        private void UpdateGameLogic(GameTime gameTime)
        {

            KeyboardState keyboardstate = Keyboard.GetState();

            if (keyboardstate.IsKeyDown(Keys.Escape))
                gameState = GameState.EscMenu; 

            if (keyboardstate.IsKeyDown(Keys.Enter) && (keyboardstate.IsKeyDown(Keys.LeftAlt) || (keyboardstate.IsKeyDown(Keys.RightAlt))))
                graphics.ToggleFullScreen();

            if (keyboardstate.IsKeyDown(Keys.Right) && paddle.alive)
            {
                paddle.Move_Right();
            }

            if (keyboardstate.IsKeyDown(Keys.Left) && paddle.alive)
            {
                paddle.Move_Left();
            }

            if (!optionLevel.lose)
            {
                ball.IfAlive_Ball();
                paddle.IfAlive_Paddle();
            }
            if (ball.alive)
            {

                ball.Update();

                ball.PaddleCollision(paddle.GetBounds);
            }
            for (int i = 0; i < bricks.Count; i++)
            {

                if (bricks[i].position.Intersects(ball.GetBoundsNext()) && bricks[i].type == BrickType.Hard)
                {
                    if (!ball.BallFire)
                        BrickCollision(i);
                }

                else if (bricks[i].position.Intersects(ball.GetBoundsNext()) && ((bricks[i].type == BrickType.BreaksBloc )||(bricks[i].type ==  BrickType.AnimRed)))
                {
                    elapsed = gameTime.ElapsedGameTime.TotalSeconds;
                    bricks[i].stopAnim = true;
                    bricks[i].isDead = true;

                    points[i].alive = true;
                    points[i].run = true;

                    Bonuses(i);

                    Points(i);

                    if (!ball.BallFire)
                        BrickCollision(i);

                }

                else if (bricks[i].stopAnim)
                {
                    bricks[i].UpdateFrame(elapsed);


                    if (bricks[i].frame >= 3 && bricks[i].isDead)
                    {

                        bricks[i].position = Rectangle.Empty;

                    }
                }

                else

                    if (bricks[i].position.Intersects(ball.GetBoundsNext()) && bricks[i].type != BrickType.Hard)
                    {
                        if (!ball.BallFire)

                            BrickCollision(i);

                        if (bricks[i].setHit())
                            bricks[i].isDead = true;



                        if (bricks[i].isDead)
                        {
                            bricks[i].position = Rectangle.Empty;

                            points[i].alive = true;
                            points[i].run = true;
                            Points(i);
                            Bonuses(i);
                        }

                        ball.Update();
                        break;
                    }

                BulletsStart(gameTime, i);

              
            }

            if (ball.alive )
            {
                for (int j = 0; j <= bonuses.Count - 1; j++)
                {
                    if (bonuses[j] != null)
                    {
                        if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 1)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            paddle.GetBounds = new Rectangle(paddle.GetBounds.X,
                                                             paddle.GetBounds.Y,
                                                             paddle.GetBounds.Width + paddle.GetBounds.Width,
                                                             paddle.GetBounds.Height);

                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 2)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            paddle.GetBounds = new Rectangle(paddle.GetBounds.X,
                                                             paddle.GetBounds.Y,
                                                             paddle.Width,
                                                             paddle.Height);

                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 3)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            Score += 1000;
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 4)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            Score -= 1000;
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 5)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            ball.SpeedBall = ball.SpeedBall + 1.5f;
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 6)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            ball.SpeedBall = ball.SpeedBall - 1.5f;
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 7)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            paddle.Velocity = new Vector2(paddle.Velocity.X + 3.5f, 0);
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 8)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            paddle.Velocity = new Vector2(paddle.Velocity.X - 2.5f, 0);
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 9)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            if (Lives < 3)
                            {
                                Lives++;
                            }
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 10)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            st = 0;
                            IsShooting = true;
                            buletsBul.IsVisibleBul = true;
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 11)
                        {
                            if (bonusVisible)
                            {
                                TimeBonusVisible = 10;
                            }

                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            bonusVisible = true;
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 12)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            ball.GetBounds = new Rectangle(ball.GetBounds.X,
                                                           ball.GetBounds.Y,
                                                           ball.Width + 10,
                                                           ball.Height + 10
                                                            );
                        }
                        else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 13)
                        {
                            test++;
                            bonuses[j].alive = false;
                            bonuses[j] = null;
                            ball.GetBounds = new Rectangle(ball.GetBounds.X,
                                                           ball.GetBounds.Y,
                                                           ball.Width - 8,
                                                           ball.Height - 8
                                                            );
                        }
                          else if ((bonuses[j].GetBounds().Intersects(paddle.GetBounds)) && bonuses[j].BonusValue == 14)
                          {
                              test++;
                              bonuses[j].alive = false;
                              bonuses[j] = null;
                              ball.Texture = ballFireTexture;
                              ballFireStart = true;
                          }
                    }
                }

            }

            if (ballFireStart)
            {
                Rectangle pos = new Rectangle(0, (ScreenRectangle.Height - TimeStopTexture.Height - 5), ScreenRectangle.Width, TimeStopTexture.Height + 50);


                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                TimeBonusFire -= elapsed;

                if (TimeBonusFire < 1  && ball.GetBoundsNext().Intersects(pos))
                {
                    ball.Standart();
                    TimeBonusFire = 5;
                    ballFireStart = false;
                }
            }

            if (bonusVisible)
            {
                start = true;

                if (ball.GetBoundsNext().Intersects(posBonusTime))
                {
                    ball.motion.Y *= -1;
                }
            }

            if (start)
            {

                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                TimeBonusVisible -= elapsed;

                if (TimeBonusVisible < 0)
                {
                    bonusVisible = false;
                    TimeBonusVisible = 10;
                    start = false;
                }

            }

            buletsBul.UpdateBulletBul(paddle.GetBounds);

            Shoot(gameTime);
            UpdateBullets();

            foreach (Points_Anim point in points)
            {
                point.RunPoints();
            }

            foreach (ObjectBonus bonus in bonuses)
            {
                if (bonus != null)
                    bonus.RunObject();
            }

            if (Lives >= 1 && !optionLevel.lose)
            {
                optionLevel.Died();
            }

            optionLevel.Win();

            if (Lives <= 0)
            {
                ball.alive = false;
                optionLevel.Lose();

                if (keyboardstate.IsKeyDown(Keys.Enter))
                {
                    Lives = 3;
                    StartGame(true);
                    CreateLevel(listLevel.CurrentItem + 1);
                    optionLevel.died = false; 
                }
            }

            if (ball.OffBottom())
            {
                Lives--;

                optionLevel.died = true; 

                if (Lives <= 0)
                { 
                    optionLevel.lose = true;
                   // Exit();
                }

                StartGame(false);
            }

            int del = 0;

            foreach (Brick brick in bricks)
            {
                if (brick.isDead)
                    del++;
            }

            if ((bricks.Count - (del + CountBricks_hard)) == 0)
            {
                optionLevel.win = true;
                StartGame(false);
                if (keyboardstate.IsKeyDown(Keys.Enter))
                {
                    if (Math.Round(timegameSeconds, 0) < 10)
                    {
                        gameData.timeGame[listLevel.CurrentItem] = timeGame[listLevel.CurrentItem] = "0" + (int)timeMinutes + " : 0" + Math.Round(timegameSeconds, 0).ToString();
                    }
                    else
                        gameData.timeGame[listLevel.CurrentItem] = timeGame[listLevel.CurrentItem] = "0" + (int)timeMinutes + " : " + Math.Round(timegameSeconds, 0).ToString();

                    ScoreList[listLevel.CurrentItem] = Score;

                    SaveGame();
                    StartGame(true);
                    CreateLevel(gameData.level[gameData.indexLevel]);
                   
                }
            }
            // CreateLevel();
        }

        private void BulletsStart(GameTime gameTime, int i)
        {
            for (int j = 0; j < bullets.Count; j++)
            {
                if (bricks[i].position.Intersects(bullets[j].GetBounds()) && bullets[j].IsVisible && bricks[i].type != BrickType.Hard)
                {
                        bricks[i].position = Rectangle.Empty;
                        bullets[j].IsVisible = false;
                        bricks[i].isDead = true;
                        Bonuses(i);
                        points[i].alive = true;
                        points[i].run = true;
                        Points(i);
                }

                else if (bricks[i].position.Intersects(bullets[j].GetBounds()) && bricks[i].type == BrickType.Hard)
                {
                    bricks[i].position = Rectangle.Empty;
                    bullets[j].IsVisible = false;
                    CountBricks_hard--;
                    bricks[i].isDead = true;
                    
                }

            }

           
        }

        private void Bonuses(int i)
        {

            if (bonuses[i] != null)
            {
                bonuses[i].alive = true;
            
            }

        }

        public void UpdateBullets()
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.position -= bullet.Speed;
                if (bullet.position.Y < 0)
                {
                    bullet.IsVisible = false;    
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].TotalActiveTime > bullets[i].ActiveTime)
                    bullets.RemoveAt(i);
            }

            if (st == 10)
            {
                buletsBul.IsVisibleBul = false;
            }
        }

        public void Shoot(GameTime gameTime)
        {
            if (IsShooting &&  !(st  == 10))
            {
                shotTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (shotTimer > timeBetweenShots)
                {
                    shotTimer = 0;
                    Bullets Bullet1 = new Bullets(Content.Load<Texture2D>("Texture/Bullet"), new Vector2(0, 2), 2000);
                    Bullets Bullet2 = new Bullets(Content.Load<Texture2D>("Texture/Bullet"), new Vector2(0, 2), 2000);
                    Vector2 vec1 = new Vector2(buletsBul.BulpositionLeft.X + 10, buletsBul.BulpositionLeft.Y);
                    Vector2 vec2 = new Vector2(buletsBul.BulpositionRight.X + 15, buletsBul.BulpositionLeft.Y);
                    Bullet1.position = vec1;
                    Bullet1.IsVisible = true;

                    Bullet2.position = vec2;
                    Bullet2.IsVisible = true;

                  //  if (bullets.Count() < 5)
                        bullets.Add(Bullet1);
                        bullets.Add(Bullet2);
                        st++;
                }
            }
        }

        private void Points(int i)
        {

            if (bricks[i].type == BrickType.BreaksBloc)
            {
               // points[i] = new Points_Anim(TexturePoints_10, Position_points, Stop_Position_points, true, true);
                Score += 10;
            }
            else
                if (bricks[i].type == BrickType.AnimRed)
                {
                  //  points[i] = new Points_Anim(TexturePoints_20, Position_points, Stop_Position_points, true, true);
                    Score += 20;
                }
                else
                    if (bricks[i].hits == 0)
                    {
                        //points[i] = new Points_Anim(TexturePoints_40, Position_points, Stop_Position_points, true, true);
                        Score += 40;
                    }
                    
                       
            if (points[i].deletePoints)
                points.RemoveAt(i);
        }



        private void BrickCollision(int i)
        {

            if (bricks[i].position.Left <= ball.GetBoundsNext().Center.X && ball.GetBoundsNext().Center.X <= bricks[i].position.Right)
                ball.motion.Y *= -1;

            else if (bricks[i].position.Top <= ball.GetBoundsNext().Center.Y && ball.GetBoundsNext().Center.Y <= bricks[i].position.Bottom)
                ball.motion.X *= -1;

            else
            {
                if (ball.motion.X > 0 && ball.motion.Y > 0  && ball.GetBoundsNext().Bottom >= bricks[i].position.Bottom
                                                            && ball.GetBoundsNext().Right >= bricks[i].position.Left)
                    ball.motion.X *= -1;

                else if (ball.motion.X > 0 && ball.motion.Y > 0 && ball.GetBoundsNext().Bottom <= bricks[i].position.Left
                                                                && ball.GetBoundsNext().Right >= bricks[i].position.Top)
                    ball.motion.Y *= -1;

                else
                    if (ball.motion.X > 0 && ball.motion.Y < 0)
                        ball.motion.Y *= -1;

                    else
                        if (ball.motion.X < 0 && ball.motion.Y < 0      && ball.GetBoundsNext().Top <= bricks[i].position.Bottom
                                                                        && ball.GetBoundsNext().Right >= bricks[i].position.Left
                                                                        && ball.GetBoundsNext().Bottom >= bricks[i].position.Bottom)
                            ball.motion.Y *= -1;

                        else
                            if (ball.motion.X < 0 && ball.motion.Y < 0)
                                ball.motion.X *= -1;

                            else
                                if (ball.motion.X < 0 && ball.motion.Y > 0  && ball.GetBoundsNext().Top <= bricks[i].position.Left
                                                                            && ball.GetBoundsNext().Left <= bricks[i].position.Right
                                                                            && ball.GetBoundsNext().Top <= bricks[i].position.Top)
                                    ball.motion.Y *= -1;

                                else
                                    if (ball.motion.X < 0 && ball.motion.Y > 0)
                                        ball.motion.X *= -1;

            }

            /*           
                    

                       else if (ball.GetBoundsNext().Top <= bricks[i].position.Bottom  && ball.GetBoundsNext().Right >= bricks[i].position.Bottom && ball.GetBoundsNext().Bottom >= bricks[i].position.Right)
                                   ball.motion.Y *= -1;

                        else if (ball.GetBoundsNext().Top <= bricks[i].position.Bottom && ball.GetBoundsNext().Right >= bricks[i].position.Bottom && ball.GetBoundsNext().Bottom <= bricks[i].position.Right)
                                   ball.motion.X *= -1;*/
         
           /*  if ((ball.GetBoundsNext().Bottom <= bricks[i].position.Center.X && ball.GetBoundsNext().Left >= bricks[i].position.Center.X))
                ball.motion.X *= -1;
             else
             if ((ball.GetBoundsNext().Bottom <= bricks[i].position.Center.X && ball.GetBoundsNext().Right >= bricks[i].position.Center.Y))
                 ball.motion.X *= -1;

            else  if ((ball.GetBoundsNext().Top <= bricks[i].position.Center.X && ball.GetBoundsNext().Left >= bricks[i].position.Center.X))
                     ball.motion.Y *= -1;

            else if ((ball.GetBoundsNext().Top <= bricks[i].position.Center.X && ball.GetBoundsNext().Right >= bricks[i].position.Center.Y))
                ball.motion.Y *= -1;
            }*/

            /*
                if ((ball.GetBoundsNext().Bottom <= bricks[i].position.Center.Y && ball.GetBoundsNext().Left >= bricks[i].position.Center.Y))
                    ball.motion.X *= -1;

                 if ((ball.GetBoundsNext().Bottom <= bricks[i].position.Center.X && ball.GetBoundsNext().Left >= bricks[i].position.Center.Y))
                    ball.motion.X *= -1;

                 if ((ball.GetBoundsNext().Bottom <= bricks[i].position.Center.X && ball.GetBoundsNext().Right <= bricks[i].position.Center.X))
                    ball.motion.X *= -1;
        

             if ((ball.GetBoundsNext().Top <= bricks[i].position.Center.X && ball.GetBoundsNext().Left >= bricks[i].position.Center.Y))
                ball.motion.Y *= -1;

             else   if ((ball.GetBoundsNext().Top >= bricks[i].position.Center.Y && ball.GetBoundsNext().Left >= bricks[i].position.Center.Y))
                ball.motion.Y *= -1;
            
            */
           /* else if (ball.GetBoundsNext().Right >= bricks[i].position.Center.X && ball.GetBoundsNext().Top <= bricks[i].position.Center.Y)
                ball.motion.Y *= -1;

            else if (ball.GetBoundsNext().Right >= bricks[i].position.Center.X && ball.GetBoundsNext().Top >= bricks[i].position.Center.X)
                ball.motion.Y *= -1;

            else if (ball.GetBoundsNext().Right >= bricks[i].position.Center.X && ball.GetBoundsNext().Top >= bricks[i].position.Center.X)
                ball.motion.X *= -1;

            else if (ball.GetBoundsNext().Right >= bricks[i].position.Center.X && ball.GetBoundsNext().Bottom >= bricks[i].position.Center.Y)
                ball.motion.X *= -1;

            else if (ball.GetBoundsNext().Right >= bricks[i].position.Center.Y && ball.GetBoundsNext().Bottom >= bricks[i].position.Center.Y)
                ball.motion.X *= -1;

            else if (ball.GetBoundsNext().Top <= bricks[i].position.Center.Y && ball.GetBoundsNext().Right >= bricks[i].position.Center.X)
                ball.motion.Y *= -1;

            else if (ball.GetBoundsNext().Top <= bricks[i].position.Center.Y && ball.GetBoundsNext().Right <= bricks[i].position.Center.X)
                ball.motion.X *= -1;

            else if ((ball.GetBoundsNext().Bottom <= bricks[i].position.Center.X && ball.GetBoundsNext().Right >= bricks[i].position.Center.X))
                ball.motion.X *= -1;

            else if ((ball.GetBoundsNext().Bottom <= bricks[i].position.Center.Y && ball.GetBoundsNext().Left >= bricks[i].position.Center.Y))
                ball.motion.X *= -1;

            else if (ball.GetBoundsNext().Bottom <= bricks[i].position.Center.X && ball.GetBoundsNext().Right <= bricks[i].position.Center.Y)
                ball.motion.Y *= -1;

            else if (ball.GetBoundsNext().Left <= bricks[i].position.Center.X && ball.GetBoundsNext().Top <= bricks[i].position.Center.Y)
                ball.motion.Y *= -1;

            else if (ball.GetBoundsNext().Left >= bricks[i].position.Center.X && ball.GetBoundsNext().Top <= bricks[i].position.Center.Y)
                ball.motion.Y *= -1;

            else if (ball.GetBoundsNext().Left >= bricks[i].position.Center.X && ball.GetBoundsNext().Top >= bricks[i].position.Center.X)
               ball.motion.Y *= -1;

            else if (ball.GetBoundsNext().Left >= bricks[i].position.Center.X && ball.GetBoundsNext().Top <= bricks[i].position.Center.X)
                ball.motion.X *= -1;*/
  
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0,0,0,0));

            if (menuState == MenuState.Settings)
            {
                if (!Loading)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(LoadingTexture, Vector2.Zero, LoadColor);
                    spriteBatch.End();
                }
                else
                {
                    Settings.DrawBackground(spriteBatch, Settings.Background);
                    Settings.Draw(spriteBatch);
                }
            }

            if (menuState == MenuState.Help)
            {
                if (!Loading)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(LoadingTexture, Vector2.Zero, LoadColor);
                    spriteBatch.End();
                }
                else
                   help.Draw(spriteBatch);
            }

            if (menuState == MenuState.MainLevelList && !(MainlistLevel.mainLevelNone))
            {

                if (!Loading)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(LoadingTexture, Vector2.Zero, LoadColor);
                    spriteBatch.End();
                }
                else
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(MainBackground, Vector2.Zero, Color.White);
                    spriteBatch.End();


                    MainlistLevel.Draw(spriteBatch);
                }
            }

            else
                if (MainlistLevel.mainLevelNone)
                {
                    spriteBatch.Begin();

                    spriteBatch.Draw(MainBackground, Vector2.Zero, Color.White);
                    spriteBatch.DrawString(menu.font, "У Вас '0' власних рiвнiв  ;) ", new Vector2(220, 200), Color.Black);

                    spriteBatch.End();
                }

            if (menuState == MenuState.LevelListBloc)
            {
                if (!Loading)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(LoadingTexture, Vector2.Zero, LoadColor);
                    spriteBatch.End();
                }
                else
                {
                    listLevel.DrawBackground(spriteBatch, listLevel.Background);

                    listLevBloc.Draw(spriteBatch);
                }
            }

             if (menuState == MenuState.LevelList)
             {
                 listLevel.DrawBackground(spriteBatch, listLevel.Background);

                 spriteBatch.Begin();

                 spriteBatch.DrawString(listLevel.font,"Час",listLevel.positionTime, Color.White);
                 spriteBatch.DrawString(listLevel.font, "Рахунок", listLevel.positionScor, Color.White);

                 for(int i= 0; i < gameData.level[gameData.indexLevel]; i++)
                 {                      
                     if (ScoreList[i] != 0)
                     {
                         spriteBatch.DrawString(listLevel.font, gameData.timeGame[i], listLevel.TimePosition[i], Color.White);
                         spriteBatch.DrawString(listLevel.font, ScoreList[i].ToString(), listLevel.ScorePosition[i], Color.White);
                         spriteBatch.Draw(listLevel.Ok, listLevel.OkPosition[i], Color.White);
                     }
                 }

                 spriteBatch.End();

                 listLevel.Draw(spriteBatch);
             }

             if (gameState == GameState.Game)
             {
                 spriteBatch.Begin();
                 spriteBatch.Draw(Background, Vector2.Zero, Color.White);
                 spriteBatch.End();

                 DrawGame(gameTime);
             }

             if (gameState == GameState.EscMenu)
             {
                 menuEsc.DrawBackground(spriteBatch, menuEsc.Background);
                 menuEsc.Draw(spriteBatch);
             }

           if (gameState == GameState.Menu && menuState == MenuState.Basic)
           {
               menu.DrawBackground(spriteBatch,menu.Background);
               menu.Draw(spriteBatch);
           }
            base.Draw(gameTime);
        }

        private void DrawGame(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch); 
            }    

            foreach (Brick brick in bricks)
            {

                if (!brick.isDead && !(brick.TypeBloc()))
                {
                    brick.Draw(spriteBatch);
                }

                else if (brick.TypeBloc())
                {
                    brick.DrawAnimationSprite(spriteBatch);
                }
            }

            foreach (Points_Anim point in points)
            {
                point.Draw_Points_Animation(spriteBatch);
            }

            foreach (ObjectBonus Bonus in bonuses)
            {
                if (Bonus != null)
                    Bonus.DrawBonus(spriteBatch);
            }

            if (bonusVisible)
                spriteBatch.Draw(TimeStopTexture, posBonusTime, Color.White);

            paddle.DrawPaddle(spriteBatch);

            buletsBul.DrawBul(spriteBatch);

            ball.DrawBall(spriteBatch);

            spriteBatch.DrawString(font, " Рахунок : " + Score, Vector2.Zero, Color.White);
            if (Math.Round(timegameSeconds, 0) < 10)
            {
                spriteBatch.DrawString(font, "Час  0" + (int)timeMinutes + " : 0" + Math.Round(timegameSeconds, 0).ToString(), new Vector2(400, 0), Color.White);
            }
            else
                spriteBatch.DrawString(font, "Час  0" + (int)timeMinutes + " : " + Math.Round(timegameSeconds, 0).ToString(), new Vector2(400, 0), Color.White);

            spriteBatch.DrawString(font, "Життя :  "/*+ Lives */, new Vector2(685, 0), Color.White);

            spriteBatch.DrawString(font, "Test-Colision : " + test, new Vector2(0, 350), Color.White);
            spriteBatch.DrawString(font, "Width-Paddle : " + paddle.GetBounds.Width, new Vector2(0, 380), Color.White);
            spriteBatch.DrawString(font, "Velocity-Paddle : " + paddle.Velocity.X, new Vector2(0, 410), Color.White);
            spriteBatch.DrawString(font, "Velocity-Ball : " + ball.SpeedBall, new Vector2(0, 440), Color.White);
            spriteBatch.DrawString(font, "Game-Time : " + gameTime.TotalGameTime.Seconds, new Vector2(0, 480), Color.White);

            if (bonusVisible)
            {
                spriteBatch.Draw(BonusTimeSt, new Vector2(1, 548), Color.White);
                spriteBatch.DrawString(font, Math.Round(TimeBonusVisible,2).ToString() , new Vector2(5, 550), Color.Black);
            }
             if (ball.BallFire)
             {
                spriteBatch.Draw(FireBallBonus, new Vector2(1, 530), Color.White);
                spriteBatch.DrawString(font, Math.Round(TimeBonusFire,2).ToString(), new Vector2(5, 530), Color.Black);
             }

            ball.DrawBallLives(spriteBatch, Lives);

            if (optionLevel.died && Lives > 0)
            {
                optionLevel.DrawDied(spriteBatch);
            }

            if (optionLevel.lose)
            {
                optionLevel.DrawLose(spriteBatch);
            }

            if (optionLevel.win)
            {
                optionLevel.DrawWin(spriteBatch);
            }


            if (pause)
            {
                string text = "" + Math.Round(pauseTime);
                if (pauseTime <= 0.5)
                {
                    text = "Go!!!";
                    scale = 3;
                }

                spriteBatch.DrawString(font, text, new Vector2(Width / 2 , Height / 2 - 130), Color.Red, 0, new Vector2(5, 5), scale, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }


        
    }
}
