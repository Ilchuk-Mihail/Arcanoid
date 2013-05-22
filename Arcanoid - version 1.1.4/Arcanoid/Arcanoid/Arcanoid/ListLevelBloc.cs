using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcanoid.MenuSystem;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Arcanoid
{
    class ListLevelBloc : ListLevel
    {

        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Game-Font/ListLevelFont");
            click = Content.Load<SoundEffect>("Sounds/click");
        }
    }
}
