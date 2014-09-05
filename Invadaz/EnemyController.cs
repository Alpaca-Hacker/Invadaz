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
        public int EnemysLeft { get; set; }
        int step = 1;

        private List<Sprite> _gameObjects;
        private int _direction = 1;
        private bool _hasHit;

        public void Startup(Rectangle gameBounds, Texture2D[] textures, List<Sprite> game)
        {
            _gameObjects = game;
            var location = new Vector2(0, 50);
            EnemysLeft = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    var enemy = new Enemy(textures[i], 6, 1, gameBounds, 3);
                    _gameObjects.Add(enemy);
                    enemy.Location = location;
                    location.X += 50;
                    EnemysLeft++;
                }

                location.Y += 40;
                location.X = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            List<Sprite> enemies = new List<Sprite>();
            enemies = _gameObjects.FindAll(x => x.GetType().Name == "Enemy");
            if (!_hasHit)
            {
                foreach (var enemy in enemies)
                {
                    int check = enemy.Walk(gameTime, _direction, step);
                    if (check == 1)
                    {
                        _hasHit = true;
                    }
                }
            }
        
            else
            {
                foreach (var enemy in enemies)
                {
 
                        enemy.Walk(gameTime, 0, step);
                }
                _direction = -_direction;
                _hasHit = false;
            }
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    for (int i = 0; i < Enemies.Length; i++)
        //    {
        //        if (Enemies[i] != null)
        //        {
        //            Enemies[i].Draw(spriteBatch);
        //        }
        //    }
        //}
    }
}
