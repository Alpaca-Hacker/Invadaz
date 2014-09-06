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
 
        public Explosion (SpriteTexture texture):base (texture)
        {
            _currentFrame = 0;
            _maxFrames = (texture.Rows * texture.Columns)*texture.Timing;
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
