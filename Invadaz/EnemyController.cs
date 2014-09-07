using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    public class EnemyController
    {
 
        int step = 1;

        private List<Sprite> _entities;
        private Rectangle _gameBounds;
        private int _direction;
        private bool _hasHitEdge;
        private GameObjects _gameObjects;

       public EnemyController(GameObjects gameObjects)
        {
            _entities = gameObjects.Entities;
            _gameBounds = gameObjects.GameBounds;

            _gameObjects = gameObjects;

        }

        public void Startup()
        {
            var textures = new SpriteTexture[_gameObjects.Content.EnemyTextures.Length];
            _gameObjects.Content.EnemyTextures.CopyTo(textures,0);            
           
            var location = new Vector2(0, 50);
            _direction = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    var enemy = new Enemy(textures[i], _gameBounds);
                    _entities.Add(enemy);
                    enemy.Location = location;
                    enemy.MyScore = (i * 25) + 50;
                    location.X += 50;
 
                }

                location.Y += 40;
                location.X = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            var enemies = new List<Sprite>();
            enemies = _entities.FindAll(x => x.GetType().Name == "Enemy");
            if (!_hasHitEdge)
            {
                foreach (var enemy in enemies)
                {
                    int check = enemy.Walk(gameTime, _direction, step);
                    if (check == 1)
                    {
                        _hasHitEdge = true;
                    }
                }
            }
        
            else
            {
                foreach (var enemy in enemies)
                {
 
                      int check = enemy.Walk(gameTime, 0, step);
                    if (check == -1)
                    {
                        _gameObjects.Score.Lives = -1;
                    }
                }
                _direction = -_direction;
                _hasHitEdge = false;
            }
            var rnd = new Random();
            if (rnd.Next(1000)<50)
            {
                var shooter= enemies[rnd.Next(enemies.Count)];
                bool canShoot=true;
                foreach (var enemy in enemies)
                {
                    if (shooter.Location.X == enemy.Location.X && shooter.Location.Y < enemy.Location.Y)
                    {
                        canShoot = false;
                        break;
                    }
                }
                if (canShoot)
                {
                    var bomb = new Bomb(_gameObjects);
                    bomb.Location = shooter.Location+new Vector2(shooter.Width/2,shooter.Height);
                    _entities.Add(bomb);
                }
                
            }
        }

    }
}
