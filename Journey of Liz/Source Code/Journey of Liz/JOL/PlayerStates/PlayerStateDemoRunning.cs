using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JOL.Classes.PlayerClasses;
using JOL.Classes.ItemClasses;
using JOL.Interfaces;
using JOL.Mario_States;

namespace JOL.PlayerStates
{
    /// <summary>
    /// Running state of the fire Mario.
    /// </summary>
    
    class PlayerStateDemoRunning : IPlayerState
    {
        Player mario;

        public PlayerStateDemoRunning(Player mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
            if (mario.PlayerSprite.isFacingRight == true)
            {
                mario.State = new FireIdleMarioState(mario);
                mario.PlayerSprite = new PlayerSpriteFireIdle(mario.PlayerSprite);
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
                mario.State = new FireIdleMarioState(mario);
                mario.PlayerSprite = new PlayerSpriteFireIdle(mario.PlayerSprite);
            }
            else
            {
                mario.PlayerSprite.velocity = 3;
            }
        }

        public void Up()
        {
            mario.State = new PlayerStateDemoJumping(mario);
            mario.PlayerSprite = new PlayerSpriteFireJumping(mario.PlayerSprite);
            mario.PlayerSprite.soundInstance.Play();
        }

        public void Down()
        {
            mario.State = new PlayerStateDemoCrouch(mario);
            mario.PlayerSprite = new PlayerSpriteFireCrouch(mario.PlayerSprite);
        }

        public void Hit()
        {
            mario.State = new PlayerStateCollectBlinking(mario, new BigRunningMarioState(mario));
            mario.PlayerSprite = new TransitionSprite(mario.PlayerSprite, new PlayerSpriteBigRunning(mario.PlayerSprite), -1);
            mario.MyState = 2;
            mario.PlayerSprite.soundInstance.Play();
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
                mario.PlayerSprite = new PlayerSpriteDead(mario.PlayerSprite);
            }
        }

        public void Update(GameTime gameTime)
        {
            mario.PlayerSprite.Update(gameTime);
            if (mario.PlayerSprite.velocity <= 0.0f)
            {
                mario.State = new FireIdleMarioState(mario);
                mario.PlayerSprite = new PlayerSpriteFireIdle(mario.PlayerSprite);
            }
            if (mario.PlayerSprite.fallSpeed >= 1f)
            {
                mario.State = new PlayerStateDemoJumping(mario);
                mario.PlayerSprite = new PlayerSpriteFireJumping(mario.PlayerSprite);
                mario.PlayerSprite.fallSpeed = 1f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.PlayerSprite.Draw(spriteBatch, camera);
        }
    }
}
