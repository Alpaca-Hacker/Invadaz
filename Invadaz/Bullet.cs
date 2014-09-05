using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Invadaz
{
    class Bullet  :  Sprite
    {
        EnemyController _enemyController;
        Rectangle _gameBounds;

        public Bullet(Texture2D texture,Vector2 location, EnemyController enemyController, Rectangle gameBounds) :base (texture,1,1,1)
        {
            this.Location = location;
            _enemyController = enemyController;
            _gameBounds = gameBounds;
        }

        public override int Update(GameTime gameTime)
        {
            var location = this.Location;
            location.Y-= 3;
            if (location.Y <0)
            { 
                return 1; 
            }
            this.Location = location;
            base.Update(gameTime);
            return 0;
        }
    }
}
