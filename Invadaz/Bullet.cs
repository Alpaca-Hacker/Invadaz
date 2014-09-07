using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Invadaz
{
    class Bullet  :  Sprite
    {
        GameObjects _gameObjects;

        public Bullet(GameObjects gameObjects) :base (gameObjects.Content.BulletTexture)
        {
            _gameObjects = gameObjects;
            Location = gameObjects.Player.Location + new Vector2((float)((gameObjects.Content.PlayerTexture.Width / 8)
                - gameObjects.Content.BulletTexture.Width / 2), 
                (float)-gameObjects.Content.BulletTexture.Height);
            gameObjects.Content.ShootSound.Play();
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
            enemies = _gameObjects.Entities.FindAll(x => x.GetType().Name == "Enemy" || x.GetType().Name == "Ufo");
            foreach (var enemy in enemies)
            {
                if (BoundingBox.Intersects(enemy.BoundingBox))
                {
                    _gameObjects.Entities.Remove(enemy);
                    var explosion = new Explosion(_gameObjects.Content.ExplosionTexture);
                    _gameObjects.Entities.Add(explosion);
                    explosion.Location = enemy.Location+new Vector2((enemy.Width/2)-explosion.Width/2,(enemy.Height/2)-explosion.Height/2);
                    _gameObjects.Score.Score += enemy.MyScore;
                   // _gameObjects.Content.ExplosionSmallSound.Play();
                    return 1;
                }
            }
            base.Update(gameTime);
            return 0;
        }
    }
}
