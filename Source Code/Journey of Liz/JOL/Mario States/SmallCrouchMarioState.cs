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

namespace JOL.MarioStates
{
    /// <summary>
    /// Crouch state of the small Mario.
    /// </summary>
    
    class SmallCrouchMarioState : IMarioState
    {
        Mario mario;

        public SmallCrouchMarioState(Mario mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
            if (mario.MarioSprite.FacingRight == false)
            {
                mario.State = new SmallRunningMarioState(mario);
                mario.MarioSprite = new MarioSpriteSmallRunning(mario.MarioSprite);
            }
            else
                mario.MarioSprite.FacingRight = false;
        }

        public void Right()
        {
            if (mario.MarioSprite.FacingRight == true)
            {
                mario.State = new SmallRunningMarioState(mario);
                mario.MarioSprite = new MarioSpriteSmallRunning(mario.MarioSprite);
            }
            else
                mario.MarioSprite.FacingRight = true;
        }

        public void Up()
        {
            mario.State = new SmallJumpingMarioState(mario);
            mario.MarioSprite = new MarioSpriteSmallJumping(mario.MarioSprite);
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
                mario.State = new CollectBlinkingMarioState(mario, new FireCrouchMarioState(mario));
                mario.MarioSprite = new TransitionSprite(mario.MarioSprite, new MarioSpriteFireCrouch(mario.MarioSprite), 1);
                mario.MyState = 3;
                mario.MarioSprite.SoundInstance.Play();
            }
            else if (item is MushroomItem)
            {
                mario.State = new CollectBlinkingMarioState(mario, new BigCrouchMarioState(mario));
                mario.MarioSprite = new TransitionSprite(mario.MarioSprite, new MarioSpriteBigCrouch(mario.MarioSprite), 1);
                mario.MyState = 2;
                mario.MarioSprite.SoundInstance.Play();
            }
            else if (item is DeadMushroomItem)
            {
                mario.State = new DeadMarioState(mario);
                mario.MarioSprite = new MarioSpriteDead(mario.MarioSprite);
                mario.MyState = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            mario.MarioSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.MarioSprite.Draw(spriteBatch, camera);
        }
    }
}
