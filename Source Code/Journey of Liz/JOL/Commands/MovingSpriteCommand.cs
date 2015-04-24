using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CSE3902Project
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MovingSpriteCommand : ICommand
    {
        Game1 game;
        MovingSprite commandSprite;
        
        public MovingSpriteCommand(Game1 game, MovingSprite commandSprite)
        {
            this.game = game;
            this.commandSprite = commandSprite;
        }

        public void Execute()
        {
            game.SwitchSprites(commandSprite);
        }
    }
}
