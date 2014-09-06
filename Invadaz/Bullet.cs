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
        ScoreController _score;

        public Bullet(Texture2D texture,ScoreController score, List <Sprite> gameObjects, Rectangle gameBounds, Texture2D explosionTexture) :base (texture,1,1,1)
        {
            _score = score;
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
                    explosion.Location = this.Location-new Vector2(enemy.Width/2,enemy.Height/2);
                    _score.Score += enemy.MyScore;
                    return 1;
                }
            }
            base.Update(gameTime);
            return 0;
        }
    }
}
