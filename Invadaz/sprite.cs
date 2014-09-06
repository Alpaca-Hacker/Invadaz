using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    public class Sprite : IEnumerable
    {
        protected Texture2D _texture;
        private int _rows;
        private int _columns;
        private int _timing;
        private int _currentFrame;
        private int _totalFrames;
        private int _anim;
        public int Width;
        public int Height;

        public Vector2 Location { get; set; } 

        public Sprite(SpriteTexture spriteTexture)
        {
            _texture = spriteTexture.Texture;
            _currentFrame = 0;
            _rows = spriteTexture.Rows;
            _columns = spriteTexture.Columns;
            _totalFrames = _rows*_columns;
            _anim = 1;
            _timing = spriteTexture.Timing;
            Location = Vector2.Zero;
            Width = _texture.Width / _columns;
            Height = _texture.Height / _rows;
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
            }
        }
        public virtual int MyScore { get; set; }

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

            int row = (int)((float)_currentFrame / (float)_columns);
            int column = _currentFrame % _columns;

            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, (int)(Width*size), (int)(Height*size));
            
            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
            
        }

        public virtual int Walk(GameTime gameTime, int _direction, int step)
        {
            throw new NotImplementedException();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
