using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JOL
{
    /// <summary>
    /// Display a congratulation scene when a level is beaten.
    /// </summary>
    
    public class WinScreen
    {
        // Global variables
        private Game1 game;
        private Level level;

        static int displayFor = 120;
        SpriteFont font;
        Vector2 position = new Vector2(280, 170);
        int displayTimer = 0;

        // Constructor
        public WinScreen(Game1 game, Level level, ContentManager content)
        {
            this.game = game;
            this.level = level;
            this.font = content.Load<SpriteFont>("PauseFont");
        }

        public void Update(GameTime gameTime)
        {
            displayTimer++;
            if (displayTimer > displayFor)
            {
                displayTimer = 0;
                game.gameState = GameState.Playing;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "You Beat Level " + game.getLevel() + "!\n\n    Lives x " + level.lives, position, Color.White);
        }
    }
}
