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
    /// Running state of the small Mario.
    /// </summary>

    class SmallRunningMarioState : IMarioState
    {
        Mario mario;

        public SmallRunningMarioState(Mario mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
            if (mario.MarioSprite.FacingRight == true)
            {
                mario.State = new SmallIdleMarioState(mario);
                mario.MarioSprite = new MarioSpriteSmallIdle(mario.MarioSprite);
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
                mario.State = new SmallIdleMarioState(mario);
                mario.MarioSprite = new MarioSpriteSmallIdle(mario.MarioSprite);
            }
            else
            {
                mario.MarioSprite.Velocity = 3;
            }
        }

        public void Up()
        {
            mario.State = new SmallJumpingMarioState(mario);
            mario.MarioSprite = new MarioSpriteSmallJumping(mario.MarioSprite);
            mario.MarioSprite.SoundInstance.Play();
        }

        public void Down()
        {
            // Do nothing.
        }

        public void Hit()
        {
            mario.State = new DeadMarioState(mario);
            mario.MarioSprite = new MarioSpriteDead(mario.MarioSprite);
            mario.MyState = 0;
            mario.level.lives--;
            mario.level.dyingAnimation = true;
            mario.MediaManager(2);
            mario.MarioSprite.SoundInstance.Play();
        }

        public void Collect(IItem item)
        {
            if (item is FireFlowerItem)
            {
                mario.State = new CollectBlinkingMarioState(mario, new FireRunningMarioState(mario));
                mario.MarioSprite = new TransitionSprite(mario.MarioSprite, new MarioSpriteFireRunning(mario.MarioSprite), 1);
                mario.MyState = 3;
                mario.MarioSprite.SoundInstance.Play();
            }
            else if (item is MushroomItem)
            {
                mario.State = new CollectBlinkingMarioState(mario, new BigRunningMarioState(mario));
                mario.MarioSprite = new TransitionSprite(mario.MarioSprite, new MarioSpriteBigRunning(mario.MarioSprite), 1);
                mario.MyState = 2;
                mario.MarioSprite.SoundInstance.Play();
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
                mario.State = new SmallIdleMarioState(mario);
                mario.MarioSprite = new MarioSpriteSmallIdle(mario.MarioSprite);
            }
            if (mario.MarioSprite.FallSpeed >= 1f)
            {
                mario.State = new SmallJumpingMarioState(mario);
                mario.MarioSprite = new MarioSpriteSmallJumping(mario.MarioSprite);
                mario.MarioSprite.FallSpeed = 1f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.MarioSprite.Draw(spriteBatch, camera);
        }
    }
}