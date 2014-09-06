using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    public class SpriteTexture
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Timing { get; set; }
        public int Width
        {
            get
            {
                return Texture.Width;
            }
        }
        public int Height
        {
            get
            {
                return Texture.Height;
            }
        }

        public SpriteTexture(Texture2D texture, int rows, int columns, int timing = 1)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            Timing = timing;
        }
    }
}
