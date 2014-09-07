using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    public class GameController
    {
        private GameObjects _gameObjects;
        private Game1 _game;

        public GameController(GameObjects gameObjects)
        {
            _gameObjects = gameObjects;
            _game = gameObjects.Game;
        }


        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length != 0)
            {
                _game.IsRunning = true;
                return;
            }
            var score = _gameObjects.Score;
            score.Lives = 3;
            score.Score = 0;
            _gameObjects.Entities.Clear();
            _gameObjects.EnemyController.Startup();
            NewPlayer();
        }

       public void NewPlayer()
        {          
            var player = new Player(_gameObjects);
            _gameObjects.Player = player; 
            _gameObjects.Entities.Add(player);
            player.Location = new Vector2(0, _gameObjects.GameBounds.Height - 100);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var titleFont = _gameObjects.Content.TitleFont;
            var titleText = "SAUCERZ";
            var textlocation = new Vector2((float)(_gameObjects.GameBounds.Width / 2) - 
                (titleFont.MeasureString(titleText).X / 2), 100);
            spriteBatch.DrawString(titleFont, titleText, textlocation, Color.BlueViolet);

            var gameFont = _gameObjects.Content.GameFont;
            var keyText = "Press Any Key To Begin";
            textlocation = new Vector2((float)(_gameObjects.GameBounds.Width / 2) -
                (gameFont.MeasureString(keyText).X / 2), 300);
            spriteBatch.DrawString(gameFont, keyText, textlocation, Color.Wheat);

        }
    }
}
