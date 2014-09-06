using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Invadaz
{
    public class GameContent
    {
        public Texture2D PlayerTexture { get; set; }
        public Texture2D BulletTexture { get; set; }
        public Texture2D UfoTexture { get; set; }
        public Texture2D ExplosionTexture { get; set; }
        public Texture2D[] EnemyTextures { get; set; }

        public SpriteFont GameFont { get; set; }

    }

}
