using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    public class Explosion : Sprite
    {
        private int _currentFrame, _maxFrames;
 
        public Explosion (Texture2D texture,int rows, int columns, int timing):base (texture,rows,columns, timing)
        {
            _currentFrame = 0;
            _maxFrames = (rows * columns)*timing;
        }

        public override int Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _currentFrame ++;
            if (_currentFrame == _maxFrames)
            {
                return 1;
            }
            return 0;
        }
    }
}
