using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Arcanoid
{
    static class FilesConfig
    {
        static string folderName = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static string pathDirerectory = Path.Combine(folderName, "Arcanoid");
        static string pathSubDirectory = Path.Combine(pathDirerectory, "Save");
        static string pathFile = Path.Combine(pathSubDirectory, "SaveGame.sav");

        public static string pathSDirect;
        static string[] file = null;

        public static void CheckAndCreate()
        {
            if (!(Directory.Exists(pathDirerectory)))
            {
                Directory.CreateDirectory(pathDirerectory);
            }

            if (Directory.Exists(pathDirerectory) && !(Directory.Exists(pathSubDirectory)))
            {
                Directory.CreateDirectory(pathSubDirectory);
            }

        }

        public static int MainLevelsLoad()
        {
            pathSDirect = Path.Combine(pathDirerectory, "UserLevel");

            if (Directory.Exists(pathSDirect))
            {
                file = Directory.GetFiles(pathSDirect);
            }
            else
            {
                Directory.CreateDirectory(pathSDirect);
                MainLevelsLoad();
            }

           return file.Length; 
        }

        public static void Reset()
        {
            File.Delete(pathFile);
        }

        public static void ResetMainLevel()
        {
            for (int numberLevel = 0; numberLevel < MainLevelsLoad(); numberLevel++)
            {
                File.Delete(file[numberLevel]);
            }
        }

        public static void CheckAndCreateFile()
        {
            CheckAndCreate();

            if (!File.Exists(pathFile))
            {
                string[] timeGame = new string[10];
                int[] Score = new int[10];

                for (int j = 0; j < 10; j++)
                {
                    timeGame[j] = "00 : 00";
                    Score[j] = 0;
                }

                int[] lv = new int[10];
               
                lv[0] = 1;

                GameData gameData = new GameData(1, lv, 0, timeGame,Score);
                FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
               
                bf.Serialize(fs, gameData);
                fs.Close();
            }

        }

       public static void Serialize(GameData gameSave)
        { 
            FileStream fs = new FileStream(pathFile , FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, gameSave);
            fs.Close();
        }

        public static GameData Deserialize()
        {
            GameData gameSave;

            FileStream fs = new FileStream(pathFile , FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            BinaryFormatter bf = new BinaryFormatter();
            gameSave = (GameData)bf.Deserialize(fs);       
            fs.Close();
            
            return gameSave;
        }
 
    }
}
