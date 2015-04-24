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
    /// Display a white scene when the game is paused.
    /// </summary>

    public class PauseMenu
    {
        // Global variables
        KeyboardController controller;
        SpriteFont font;
        Level level;
        Vector2 position = new Vector2(330, 200);

        // Constructor
        public PauseMenu(Level level, ContentManager content)
        {
            this.level = level;
            this.font = content.Load<SpriteFont>("PauseFont");
            this.controller = new KeyboardController(new PauseCommand(level));
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Lives x " + level.lives, position, Color.White);
        }
    }
}
