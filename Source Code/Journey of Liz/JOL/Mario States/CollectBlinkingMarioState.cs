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
using JOL.MarioStates;
using JOL.Classes.MarioClasses;
using JOL.Classes.ItemClasses;
using JOL.Mario_States;
using JOL.Interfaces;

namespace JOL
{
    /// <summary>
    /// Animating the transition from one stage to another.
    /// </summary>

    class CollectBlinkingMarioState : IMarioState
    {
        Mario mario;
        IMarioState nextMarioState;

        public CollectBlinkingMarioState(Mario mario, IMarioState nextMarioState)
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
            mario.MarioSprite.Update(gameTime);
            TransitionSprite sprite = (TransitionSprite) mario.MarioSprite;
            if (sprite.doneTransitioning)
            {
                mario.State = nextMarioState;
                mario.MarioSprite = sprite.nextMarioSprite;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.MarioSprite.Draw(spriteBatch, camera);
        }
    }
}
