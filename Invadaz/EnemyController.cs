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

        public Enemy[] Enemies = new Enemy[50];
        private int direction = 1;
        private bool _hasHit;

        public void Startup(Rectangle gameBounds, Texture2D[] textures)
        {
            var location = new Vector2(0, 50);
            EnemysLeft = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 10; j++)
                {

                    Enemies[EnemysLeft] = new Enemy(textures[i], 6, 1, gameBounds, 3);
                    Enemies[EnemysLeft].Location = location;
                    location.X += 50;
                    EnemysLeft++;

                }
                location.Y += 40;
                location.X = 0;
            }
        }

        public void Update(GameTime gameTime)
        {

            if (!_hasHit)
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    if (Enemies[i] != null)
                    {
                        int check = Enemies[i].Update(gameTime, direction, step);
                        if (check == 1)
                        {
                            _hasHit = true;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    if (Enemies[i] != null)
                    {
                        Enemies[i].Update(gameTime, 0, step);
                    }
                }
                direction = -direction;
                _hasHit = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i] != null)
                {
                    Enemies[i].Draw(spriteBatch);
                }
            }
        }
    }
}
