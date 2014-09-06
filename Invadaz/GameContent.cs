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
        public SpriteTexture PlayerTexture { get; set; }
        public SpriteTexture BulletTexture { get; set; }
        public SpriteTexture UfoTexture { get; set; }
        public SpriteTexture ExplosionTexture { get; set; }
        public SpriteTexture[] EnemyTextures { get; set; }
        public SpriteTexture BombTexture { get; set; }

        public SpriteFont GameFont { get; set; }

    }

}
