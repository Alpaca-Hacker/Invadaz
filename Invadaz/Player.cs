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
        private bool _isDead = false;

        public bool IsVisible { get; set; }
        
        public Player(GameObjects gameObjects) 
            :base (gameObjects.Content.PlayerTexture)
        {
            _gameObjects = gameObjects;
            IsVisible = true;
        }

        public override int Update(GameTime gameTime)
        {
            var location = this.Location;
            var screenEdge = _gameObjects.GameBounds.Width-(this.Width);

            if (_lastFiredTicks > 0)
            {
                _lastFiredTicks--;
            }
            if (_isDead)
                if (_lastFiredTicks >0)
                {
                    return 0;
                }
                else
                {
                    _gameObjects.GameController.NewPlayer();
                    _gameObjects.Score.Lives--;
                    return 1;
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
                    location.X -= _moveSpeed;
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                    && location.X < screenEdge)
                {
                    location.X += _moveSpeed;
                }

            this.Location = location;
  
            return base.Update(gameTime);

        }
        public override void Draw(SpriteBatch spriteBatch, float size = 0.75f)
        {
            
            if (!IsVisible)
            {
                return;
            }
            base.Draw(spriteBatch, size);
        }

        public void Death()
        {
            if (_isDead) { return; }
            var explosion = new Explosion(_gameObjects.Content.BigExplosionTexure);
            explosion.Location = Location + new Vector2((Width / 2)-explosion.Width/2, (Height / 2)-explosion.Height/2);
            _gameObjects.Entities.Add(explosion);
            _isDead = true;
            _lastFiredTicks = 100;
            IsVisible = false;
        }




       
    }
}
