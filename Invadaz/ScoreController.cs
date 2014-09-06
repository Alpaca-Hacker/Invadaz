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

        public ScoreController(SpriteFont gameFont, Rectangle gameBounds)
        {
            _gameFont = gameFont;
            _gameBounds = gameBounds;
        }

        public int Score { get; set; }

        public void Draw (SpriteBatch spriteBatch)
        {
            var displayText = string.Format("Score: {0}",Score);
            var textLocation = new Vector2((_gameBounds.Width / 2) -(_gameFont.MeasureString(displayText).X / 2), 
                _gameBounds.Height - 25);
            spriteBatch.DrawString(_gameFont, displayText, textLocation, Color.Wheat);
            
        }
    }
}
