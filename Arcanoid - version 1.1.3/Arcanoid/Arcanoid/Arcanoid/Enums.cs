using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcanoid
{
    enum GameState  
    {
        Game,            //  Стан Гри 
        Menu,           //   Стан Меню
        EscMenu,       //    ESC Меню   
        Exit          //     Стан Виходу
    }

    enum MenuState  
    {
        LevelListBloc,
        LevelList,   // Список рівнів
        MainLevelList,
        Basic,      //  Базовий стан
        Settings,
        Help,
        Exit       //   Стан закінчення поточної гри
         
    }

   public enum BrickType
    {
        Hard,            // Твердий блок // Неможливо розбити ;)
        Mild,           //  М'який блок 
        AnimRed,       // Анімований блок
        BreaksBloc    // Блок що розламується на дрібні частинки
    }

}
