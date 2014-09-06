using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    class GameObjects
    {
        public Player Player { get; set; }
        public List<Sprite> Entities { get; set; }
        public GameContent Content { get; set; }
        public Rectangle GameBounds { get; set; }
        public ScoreController Score { get; set; }
        public EnemyController EnemyController { get; set; }
    }
}
