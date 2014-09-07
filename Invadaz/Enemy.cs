using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
     public class Enemy : Sprite
    {
         private Rectangle _gameBounds;


         public Enemy(SpriteTexture texture, Rectangle gameBounds ) 
            :base (texture)
         {
             _gameBounds = gameBounds;
         }

        

      public override int Walk(GameTime gameTime,int direction, int timing=1)
         {

          if (gameTime.TotalGameTime.Ticks%timing !=0)
          {
              return 0;
          }
          var location = this.Location;
          if (direction == 0)
          {
              location.Y += 20;
              if (location.Y>_gameBounds.Height-120)
              {
                  return -1;
              }
              this.Location = location;
              return 0;
          }
          location.X += direction;
          int returnValue = 0;
          if (location.X >=_gameBounds.Width-this._texture.Width || location.X <=0)
          {
              returnValue = 1;
          }
          this.Location = location;
          return returnValue;
         }
    }
}
