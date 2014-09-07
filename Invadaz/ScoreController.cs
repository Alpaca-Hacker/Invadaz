using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invadaz
{
    public class ScoreController
    {
        private Rectangle _gameBounds;
        private GameObjects _gameObjects;
        private int _lives;

        public ScoreController(GameObjects gameObjects)
        {
            _gameBounds = gameObjects.GameBounds;
            _gameObjects = gameObjects;
        }

        public int Level { get; set; }
        public int Score { get; set; }
        public int Lives {
            get { return _lives; }
            set
            {
                if (value < 0)
                {
                    _gameObjects.Game.IsRunning = false;
                }
                else
                {
                    _lives = value;
                }
            }
            }

        public void Draw (SpriteBatch spriteBatch)
        {
            
            var DisplayText = string.Format("Score: {0} Lives {1} Level {2}",Score, Lives, Level);
            DisplayCentre(spriteBatch, DisplayText);
            
        }

        private void DisplayCentre(SpriteBatch spriteBatch, string DisplayText)
        {
            var gameFont = _gameObjects.Content.GameFont;
            var TextLocation = new Vector2((_gameBounds.Width / 2) - (gameFont.MeasureString(DisplayText).X / 2),
                _gameBounds.Height - 25);
            spriteBatch.DrawString(gameFont, DisplayText, TextLocation, Color.Wheat);
        }
    }
}
