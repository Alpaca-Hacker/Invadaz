using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Invadaz
{
    public class Bomb :Sprite
    {
        GameObjects _gameObjects;
        Player _player;

        public Bomb (GameObjects gameObjects):base (gameObjects.Content.BombTexture)
        {
            _gameObjects = gameObjects;
            _player = gameObjects.Player;
        }

        public override int Update(GameTime gameTime)
        {
            var location = this.Location;
            location.Y += 3;
            if (location.Y > _gameObjects.GameBounds.Height)
            {
                return 1;
            }
            this.Location = location;
            base.Update(gameTime);
            return 0;
        }
    }
}
