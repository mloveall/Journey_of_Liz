using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JOL
{
    /// <summary>
    /// Display # of lives, coins, and score on top of the screen.
    /// </summary>

    public class HeadsUpDisplay
    {
        // Global variables
        private SpriteFont font;
        Level level;

        // Constructor
        public HeadsUpDisplay( SpriteFont font)
        {
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            String hudString = "Lives x " + level.lives + "                       Coins x " + level.coins + "                    Score: " + level.score;
            spriteBatch.DrawString(font, hudString, new Vector2(), Color.Black);
        }

        internal void setLevel(Level level)
        {
            this.level = level;
        }
    }
}
