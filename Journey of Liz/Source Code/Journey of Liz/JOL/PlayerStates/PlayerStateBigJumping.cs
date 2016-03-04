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
using JOL.Interfaces;
using JOL.Mario_States;

namespace JOL.PlayerStates
{
    /// <summary>
    /// Jumping state of the big Mario.
    /// </summary>

    class PlayerStateBigJumping : IPlayerState
    {
        Player mario;

        public PlayerStateBigJumping(Player mario)
        {
            this.mario = mario;
        }

        public void Left()
        {
                if (mario.PlayerSprite.isFacingRight == false)
                {
                    mario.PlayerSprite.isMoving = true;
                }
                else
                    mario.PlayerSprite.isFacingRight = false;
        }

        public void Right()
        {
                if (mario.PlayerSprite.isFacingRight == true)
                {
                    mario.PlayerSprite.isMoving = true;
                }
                else
                    mario.PlayerSprite.isFacingRight = true;
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
            mario.State = new PlayerStateCollectBlinking(mario, new PlayerStateSmallJumping(mario));
            mario.PlayerSprite = new TransitionSprite(mario.PlayerSprite, new PlayerSpriteSmallJumping(mario.PlayerSprite), -1);
            mario.MyState = 1;
            mario.PlayerSprite.soundInstance.Play();
        }

        public void Collect(IItem item)
        {
            if (item is FireFlowerItem)
            {
                mario.State = new PlayerStateCollectBlinking(mario,new PlayerStateDemoJumping(mario));
                mario.PlayerSprite = new TransitionSprite(mario.PlayerSprite, new PlayerSpriteFireJumping(mario.PlayerSprite), 1);
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
                mario.MyState = 0;
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
