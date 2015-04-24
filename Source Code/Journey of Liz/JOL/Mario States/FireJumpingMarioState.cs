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
using JOL.Interfaces;
using JOL.Mario_States;

namespace JOL
{
    /// <summary>
    /// Jumping state of the fire Mario.
    /// </summary>

    class FireJumpingMarioState : IMarioState
    {
        Mario mario;

        public FireJumpingMarioState(Mario mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
            if (mario.MarioSprite.FacingRight == false)
            {
                mario.MarioSprite.IsMoving = true;
            }
            else
                mario.MarioSprite.FacingRight = false;
        }

        public void Right()
        {
            if (mario.MarioSprite.FacingRight == true)
            {
                mario.MarioSprite.IsMoving = true;
            }
            else
                mario.MarioSprite.FacingRight = true;
        }

        public void Up()
        {
            // Do nothing since already jumping.
        }

        public void Down()
        {
            // Do nothing.
        }

        public void Hit()
        {
            mario.State = new CollectBlinkingMarioState(mario, new BigJumpingMarioState(mario));
            mario.MarioSprite = new TransitionSprite(mario.MarioSprite, new MarioSpriteBigJumping(mario.MarioSprite), -1);
            mario.MyState = 2;
            mario.MarioSprite.SoundInstance.Play();
        }

        public void Collect(IItem item)
        {
            if (item is FireFlowerItem)
            {
                // Do nothing since alreay in fire stage.
            }
            else if (item is MushroomItem)
            {
                // Do nothing since alreay in fire stage.
            }
            else if (item is DeadMushroomItem)
            {
                mario.State = new DeadMarioState(mario);
                mario.MarioSprite = new MarioSpriteDead(mario.MarioSprite);
            }
            
           
        }

        public void Update(GameTime gameTime)
        {
            mario.MarioSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.MarioSprite.Draw(spriteBatch,camera);
        }
    }
}