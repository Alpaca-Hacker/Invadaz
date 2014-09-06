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

        private List<Sprite> _emtities;
        private Texture2D[] _textures ;
        private Rectangle _gameBounds;
        private int _direction;
        private bool _hasHitEdge;

       public EnemyController(GameObjects gameObjects)
        {
            _emtities = gameObjects.Entities;
            _gameBounds = gameObjects.GameBounds;
            _textures = new Texture2D[gameObjects.Content.EnemyTextures.Length];
            gameObjects.Content.EnemyTextures.CopyTo(_textures,0);

        }

        public void Startup()
        {
           
            var location = new Vector2(0, 50);
            _direction = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    var enemy = new Enemy(_textures[i], 6, 1, _gameBounds, 3);
                    _emtities.Add(enemy);
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
            List<Sprite> enemies = new List<Sprite>();
            enemies = _emtities.FindAll(x => x.GetType().Name == "Enemy");
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
 
                        enemy.Walk(gameTime, 0, step);
                }
                _direction = -_direction;
                _hasHitEdge = false;
            }
        }

    }
}
