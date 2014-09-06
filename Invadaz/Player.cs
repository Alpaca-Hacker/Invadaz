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

        private GameObjects _gameObjects;

        private const int _moveSpeed = 3;
        private int _lastFiredTicks = 0;

        public Player(GameObjects gameObjects) 
            :base (gameObjects.Content.PlayerTexture)
        {
            _gameObjects = gameObjects;
        }

        public new void Update(GameTime gameTime)
        {
            var location = this.Location;
            var screenEdge = _gameObjects.GameBounds.Width-(this.Width);
            if (_lastFiredTicks > 0)
            {
                _lastFiredTicks--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _lastFiredTicks == 0 
                && (_gameObjects.Entities.FindAll(x => x.GetType().Name == "Bullet").Count < 5))
            {
                var bullet = new Bullet(_gameObjects);
                _gameObjects.Entities.Add(bullet);
                _lastFiredTicks = 30;
            }

         
            if ((Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) 
                && location.X > 0)
            {
                location.X-= _moveSpeed;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                && location.X < screenEdge)
            {
                location.X+= _moveSpeed;
            }
            this.Location = location;
  
            base.Update(gameTime);

        }



    }
}
