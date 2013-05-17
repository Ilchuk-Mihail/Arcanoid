using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Arcanoid
{
    class Settings : Menu
    {

        private Texture2D SettingBackground;

        public override void DrawBackground(SpriteBatch spriteBatch, Texture2D background)
        {
            base.DrawBackground(spriteBatch, this.SettingBackground);
        }

        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Game-Font/ListLevelFont");
            SettingBackground = Content.Load<Texture2D>("Texture/Backgrounds/SettingBackground");
        }
    }
}
