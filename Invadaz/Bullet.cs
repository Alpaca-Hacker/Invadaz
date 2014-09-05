using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Invadaz
{
    class Bullet  :  Sprite
    {
        List<Sprite> _gameObjects;
        Rectangle _gameBounds;
        Texture2D _explosionTexture;

        public Bullet(Texture2D texture,Vector2 location, List <Sprite> gameObjects, Rectangle gameBounds, Texture2D explosionTexture) :base (texture,1,1,1)
        {
            this.Location = location;
            _gameObjects = gameObjects;
            _gameBounds = gameBounds;
            _explosionTexture = explosionTexture;
        }

        public override int Update(GameTime gameTime)
        {
            var location = this.Location;
            location.Y-= 3;
            if (location.Y <0)
            { 
                return 1; 
            }
            this.Location = location;
            var enemies = new List<Sprite>();
            enemies = _gameObjects.FindAll(x => x.GetType().Name == "Enemy" || x.GetType().Name == "Ufo");
            foreach (var enemy in enemies)
            {
                if (BoundingBox.Intersects(enemy.BoundingBox))
                {
                    _gameObjects.Remove(enemy);
                    var explosion = new Explosion(_explosionTexture, 1, 7, 4);
                    _gameObjects.Add(explosion);
                    explosion.Location = this.Location-new Vector2(0,enemy.Height/2);
                    return 1;
                }
            }
            base.Update(gameTime);
            return 0;
        }
    }
}
