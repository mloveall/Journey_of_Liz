using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.MarioStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JOL.Classes.MarioClasses;
using JOL.Classes.ItemClasses;
using JOL.Interfaces;
using JOL.Mario_States;

namespace JOL
{
    /// <summary>
    /// Running state of the fire Mario.
    /// </summary>
    
    class FireRunningMarioState : IMarioState
    {
        Mario mario;

        public FireRunningMarioState(Mario mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
            if (mario.MarioSprite.FacingRight == true)
            {
                mario.State = new FireIdleMarioState(mario);
                mario.MarioSprite = new MarioSpriteFireIdle(mario.MarioSprite);
            }
            else
            {
                mario.MarioSprite.Velocity = 3;
            }
        }

        public void Right()
        {
            if (mario.MarioSprite.FacingRight == false)
            {
                mario.State = new FireIdleMarioState(mario);
                mario.MarioSprite = new MarioSpriteFireIdle(mario.MarioSprite);
            }
            else
            {
                mario.MarioSprite.Velocity = 3;
            }
        }

        public void Up()
        {
            mario.State = new FireJumpingMarioState(mario);
            mario.MarioSprite = new MarioSpriteFireJumping(mario.MarioSprite);
            mario.MarioSprite.SoundInstance.Play();
        }

        public void Down()
        {
            mario.State = new FireCrouchMarioState(mario);
            mario.MarioSprite = new MarioSpriteFireCrouch(mario.MarioSprite);
        }

        public void Hit()
        {
            mario.State = new CollectBlinkingMarioState(mario, new BigRunningMarioState(mario));
            mario.MarioSprite = new TransitionSprite(mario.MarioSprite, new MarioSpriteBigRunning(mario.MarioSprite), -1);
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
            if (mario.MarioSprite.Velocity <= 0.0f)
            {
                mario.State = new FireIdleMarioState(mario);
                mario.MarioSprite = new MarioSpriteFireIdle(mario.MarioSprite);
            }
            if (mario.MarioSprite.FallSpeed >= 1f)
            {
                mario.State = new FireJumpingMarioState(mario);
                mario.MarioSprite = new MarioSpriteFireJumping(mario.MarioSprite);
                mario.MarioSprite.FallSpeed = 1f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.MarioSprite.Draw(spriteBatch, camera);
        }
    }
}
