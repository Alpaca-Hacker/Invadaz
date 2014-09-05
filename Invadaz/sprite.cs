using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    public class Sprite
    {
        protected Texture2D _texture;
        private int _rows;
        private int _columns;
        private int _timing;
        private int _currentFrame;
        private int _totalFrames;
        private int _anim;

        public Vector2 Location { get; set; } 

        public Sprite(Texture2D texture, int rows, int columns, int timing = 2)
        {
            _texture = texture;
            _currentFrame = 0;
            _rows = rows;
            _columns = columns;
            _totalFrames = _rows*_columns;
            _anim = 1;
            _timing = timing;
            Location = Vector2.Zero;
        }


        public virtual int Update(GameTime gameTime)
        {
           
            if (gameTime.TotalGameTime.Ticks % _timing == 0 && _totalFrames >1)
            { 
                _currentFrame += _anim;
                if (_currentFrame < 0)
                {
                    _anim = 1;
                    _currentFrame = 1;
                }
                if (_currentFrame >= _totalFrames)
                {
                    _anim = -1;
                    _currentFrame = _totalFrames - 2;
                }
            }
            return 0;
        }

        public void Draw(SpriteBatch spriteBatch, float size=1.0f)
        {
            int width = _texture.Width / _columns;
            int height = _texture.Height / _rows;
            int row = (int)((float)_currentFrame / (float)_columns);
            int column = _currentFrame % _columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, (int)(width*size), (int)(height*size));
            
            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
            
        }
    }
}
