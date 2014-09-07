using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Invadaz
{
    class Bullet  :  Sprite
    {
        List<Sprite> _entities;
        Rectangle _gameBounds;
        SpriteTexture _explosionTexture;
        ScoreController _score;

        public Bullet(GameObjects gameObjects) :base (gameObjects.Content.BulletTexture)
        {
            _score = gameObjects.Score;
            _entities = gameObjects.Entities;
            _gameBounds = gameObjects.GameBounds;
            _explosionTexture = gameObjects.Content.ExplosionTexture;
            Location = gameObjects.Player.Location + new Vector2((float)((gameObjects.Content.PlayerTexture.Width / 8)
                - gameObjects.Content.BulletTexture.Width / 2), 
                (float)-gameObjects.Content.BulletTexture.Height);
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
            enemies = _entities.FindAll(x => x.GetType().Name == "Enemy" || x.GetType().Name == "Ufo");
            foreach (var enemy in enemies)
            {
                if (BoundingBox.Intersects(enemy.BoundingBox))
                {
                    _entities.Remove(enemy);
                    var explosion = new Explosion(_explosionTexture);
                    _entities.Add(explosion);
                    explosion.Location = enemy.Location+new Vector2((enemy.Width/2)-explosion.Width/2,(enemy.Height/2)-explosion.Height/2);
                    _score.Score += enemy.MyScore;
                    return 1;
                }
            }
            base.Update(gameTime);
            return 0;
        }
    }
}
