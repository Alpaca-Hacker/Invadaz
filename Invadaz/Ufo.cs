using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Invadaz
{
    public class Ufo : Sprite
    {
        Rectangle _gameBounds;

        public Ufo (GameObjects gameObjects) : base (gameObjects.Content.UfoTexture)
        {
            _gameBounds = gameObjects.GameBounds;
        }

        public  override int MyScore
        {
            get
            {
                var rnd = new Random();

                return (rnd.Next(1,5) * 100);
            }
        }

        public override int Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var location = this.Location;
            location.X+= 2;
            if (location.X > _gameBounds.Width)
            {
                return 1;
            }
            this.Location = location;
            return 0;
        }
    }
}
