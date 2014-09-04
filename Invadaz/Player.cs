using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invadaz
{
    public class Player : Sprite
    {

        private Rectangle _gameBounds;
        private const int _moveSpeed = 3;

        public Player(Texture2D texture, int rows, int columns, Rectangle gameBounds,int timing = 1 ) 
            :base (texture,rows,columns,timing)
        {
            _gameBounds = gameBounds;
        }

        public new void Update(GameTime gameTime)
        {
            var location = this.Location;
         
            if ((Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) && location.X > 0)
            {
                location.X-= _moveSpeed;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                && location.X < (_gameBounds.Width - 72))
            {
                location.X+= _moveSpeed;
            }
            this.Location = location;
  

            base.Update(gameTime);

        }



    }
}
