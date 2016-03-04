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
using JOL.Classes.PlayerClasses;
using JOL.Classes.ItemClasses;
using JOL.Mario_States;
using JOL.Interfaces;

namespace JOL.PlayerStates
{
    /// <summary>
    /// Animating the transition from one stage to another.
    /// </summary>

    class PlayerStateCollectBlinking : IPlayerState
    {
        Player mario;
        IPlayerState nextMarioState;

        public PlayerStateCollectBlinking(Player mario, IPlayerState nextMarioState)
        {
            this.mario = mario;
            this.nextMarioState = nextMarioState;
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
            TransitionSprite sprite = (TransitionSprite) mario.PlayerSprite;
            if (sprite.doneTransitioning)
            {
                mario.State = nextMarioState;
                mario.PlayerSprite = sprite.nextMarioSprite;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.PlayerSprite.Draw(spriteBatch, camera);
        }
    }
}
