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
using JOL.Mario_States;
using JOL.Interfaces;

namespace JOL
{
    /// <summary>
    /// Running state of the big Mario.
    /// </summary>

    class BigRunningMarioState : IPlayerState
    {
        Player mario;

        public BigRunningMarioState(Player mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
                if (mario.PlayerSprite.isFacingRight == true)
                {
                    mario.State = new BigIdleMarioState(mario);
                    mario.PlayerSprite = new PlayerSpriteBigIdle(mario.PlayerSprite);
                }
                else
                {
                    mario.PlayerSprite.velocity = 3;
                }
        }

        public void Right()
        {
                if (mario.PlayerSprite.isFacingRight == false)
                {
                    mario.State = new BigIdleMarioState(mario);
                    mario.PlayerSprite = new PlayerSpriteBigIdle(mario.PlayerSprite);
                }
                else
                {
                    mario.PlayerSprite.velocity = 3;
                }
        }

        public void Up()
        {
                mario.State = new PlayerStateBigJumping(mario);
                mario.PlayerSprite = new PlayerSpriteBigJumping(mario.PlayerSprite);
                mario.PlayerSprite.soundInstance.Play();
        }

        public void Down()
        {
                mario.State = new BigCrouchMarioState(mario);
                mario.PlayerSprite = new PlayerSpriteBigCrouch(mario.PlayerSprite);
        }

        public void Hit()
        {
            mario.State = new PlayerStateCollectBlinking(mario, new PlayerStateSmallRunning(mario));
            mario.PlayerSprite = new TransitionSprite(mario.PlayerSprite, new PlayerSpriteSmallRunning(mario.PlayerSprite), -1);
            mario.MyState = 1;
            mario.PlayerSprite.soundInstance.Play();
        }

        public void Collect(IItem item)
        {
            if (item is FireFlowerItem)
            {
                mario.State = new PlayerStateCollectBlinking(mario,new PlayerStateDemoRunning(mario));
                mario.PlayerSprite = new TransitionSprite(mario.PlayerSprite, new PlayerSpriteFireRunning(mario.PlayerSprite), 1);
                mario.MyState = 3;
                mario.PlayerSprite.soundInstance.Play();
            }
            else if (item is MushroomItem)
            {
                // Do nothing since already in big stage.
            }
            else if (item is DeadMushroomItem)
            {
                mario.State = new DeadMarioState(mario);
                mario.PlayerSprite = new PlayerSpriteDead(mario.PlayerSprite);
            }
            
        }

        public void Update(GameTime gameTime)
        {
            mario.PlayerSprite.Update(gameTime);
            if (mario.PlayerSprite.velocity <= 0.0f)
            {
                mario.State = new BigIdleMarioState(mario);
                mario.PlayerSprite = new PlayerSpriteBigIdle(mario.PlayerSprite);
            }
            if (mario.PlayerSprite.fallSpeed >= 1f)
            {
                mario.State = new PlayerStateBigJumping(mario);
                mario.PlayerSprite = new PlayerSpriteBigJumping(mario.PlayerSprite);
                mario.PlayerSprite.fallSpeed = 1f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.PlayerSprite.Draw(spriteBatch, camera);
        }
    }
}
