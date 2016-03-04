﻿using System;
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
using JOL.Interfaces;
using JOL.Mario_States;

namespace JOL.PlayerStates
{
    /// <summary>
    /// Crouch state of the fire Mario.
    /// </summary>

    class PlayerStateDemoCrouch : IPlayerState
    {
        Player mario;

        public PlayerStateDemoCrouch(Player mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
            if (mario.PlayerSprite.isFacingRight == false)
            {
                mario.State = new PlayerStateDemoRunning(mario);
                mario.PlayerSprite = new PlayerSpriteFireRunning(mario.PlayerSprite);
            }
            else
                mario.PlayerSprite.isFacingRight = false;
        }

        public void Right()
        {
            if (mario.PlayerSprite.isFacingRight == true)
            {
                mario.State = new PlayerStateDemoRunning(mario);
                mario.PlayerSprite = new PlayerSpriteFireRunning(mario.PlayerSprite);
            }
            else
                mario.PlayerSprite.isFacingRight = true;
        }

        public void Up()
        {
            mario.State = new FireIdleMarioState(mario);
            mario.PlayerSprite = new PlayerSpriteFireIdle(mario.PlayerSprite);
        }

        public void Down()
        {
            // Do nothing.
        }

        public void Hit()
        {
            mario.State = new PlayerStateCollectBlinking(mario, new BigCrouchMarioState(mario));
            mario.PlayerSprite = new TransitionSprite(mario.PlayerSprite, new PlayerSpriteBigCrouch(mario.PlayerSprite), -1);
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
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            mario.PlayerSprite.Draw(spriteBatch, camera);
        }
    }
}