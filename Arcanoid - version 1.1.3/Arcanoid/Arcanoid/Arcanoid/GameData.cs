using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcanoid
{
    [Serializable]
    class GameData
    {
        public int[] level = new int[10];
        public  int bloc;
        public  int indexLevel;
        public string[] timeGame = new string[10];
        public int[] Score = new int[10];

        public GameData(int bloc, int[] level,int indexLevel, string[] timeGame, int[] Score)
        {    
            this.bloc = bloc;
            this.level = level;
            this.indexLevel = indexLevel;
            this.timeGame = timeGame;
            this.Score = Score;
        }

        public GameData(string[] timeGame)
        {
            this.timeGame = timeGame;
            for(int i = 0 ; i < 10 ; i++)
            {
                if (timeGame[i] == null)
                    timeGame[i] = "00 : 00";
            }
        }
    }
}
