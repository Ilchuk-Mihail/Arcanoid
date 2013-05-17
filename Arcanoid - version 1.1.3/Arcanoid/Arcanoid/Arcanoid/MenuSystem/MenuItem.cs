using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Arcanoid
{
    class MenuItem
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public event EventHandler Click;

        public Vector2 position;
        public int NumberLevel;

        public MenuItem(string name,Vector2 position)
        {
            this.Name = name;
            Active = true;
            this.position = position;
        }

        public MenuItem(string name, Vector2 position, int NumberLevel)
        {
            this.Name = name;
            Active = true;
            this.position = position;
            this.NumberLevel = NumberLevel;
        }

        public void OnClick()
        {
            if (Click != null)
                Click(this, null);      
        }
    }
}
