using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;


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
        public SpriteTexture BigExplosionTexure { get; set; }

        public SpriteFont GameFont { get; set; }
        public SpriteFont TitleFont { get; set; }

        public SoundEffect ExplosionBigSound { get; set; }
        public SoundEffect ExplosionSmallSound { get; set; }
        public SoundEffect ShootSound { get; set; }
    }

}
