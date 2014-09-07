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
        private SpriteFont _gameFont;
        private Rectangle _gameBounds;
        private GameObjects _gameObjects;
        private int _lives;

        public ScoreController(GameObjects gameObjects)
        {
            _gameFont = gameObjects.Content.GameFont;
            _gameBounds = gameObjects.GameBounds;
            _gameObjects = gameObjects;
        }

        public int Score { get; set; }
        public int Lives {
            get { return _lives; }
            set
            {

                if (value < 0)
                {
                    _lives = 3;
                    Score = 0;
                    _gameObjects.Entities.RemoveAll(ent => ent.GetType().Name == "Enemy");
                    _gameObjects.EnemyController.Startup();
                }
                else
                {
                    _lives = value;
                }
            }
            }

        public void Draw (SpriteBatch spriteBatch)
        {
            var DisplayText = string.Format("Score: {0} Lives {1}",Score, Lives);
            DisplayCentre(spriteBatch, DisplayText);
            
        }

        private void DisplayCentre(SpriteBatch spriteBatch, string DisplayText)
        {
            var TextLocation = new Vector2((_gameBounds.Width / 2) - (_gameFont.MeasureString(DisplayText).X / 2),
                _gameBounds.Height - 25);
            spriteBatch.DrawString(_gameFont, DisplayText, TextLocation, Color.Wheat);
        }
    }
}
