using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JOL.PlayerStates;
using JOL.Classes.PlayerClasses;
using JOL.Classes.ItemClasses;
using JOL.Interfaces;

namespace JOL
{
    /// <summary>
    /// Death state of Mario.
    /// </summary>

    class DeadMarioState : IPlayerState
    {
        Player mario;

        public DeadMarioState(Player mario)
        {
            this.mario = mario;
        }

        // Disable all the controls
        public void Left()
        {
        }

        public void Right()
        {
        }

        public void Up()
        {
        }

        public void Down()
        {
        }

        public void Hit()
        {
        }

        public void Collect(IItem item)
        {
        }

        public void Update(GameTime gameTime)
        {
            mario.PlayerSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.PlayerSprite.Draw(spriteBatch,camera);
        }
    }
}
