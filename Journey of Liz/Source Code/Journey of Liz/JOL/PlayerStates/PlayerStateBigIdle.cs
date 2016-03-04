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
    /// Idle state of the big Mario.
    /// </summary>

    class BigIdleMarioState : IPlayerState
    {
        Player player;
        
        public BigIdleMarioState(Player player)
        {
            this.player = player;
        }

        public void Left()
        {
            if (player.PlayerSprite.isFacingRight == false)
            {
                player.State = new BigRunningMarioState(player);
                player.PlayerSprite = new PlayerSpriteBigRunning(player.PlayerSprite);
            }
            else
                player.PlayerSprite.isFacingRight = false;
        }

        public void Right()
        {
            if (player.PlayerSprite.isFacingRight == true)
            {
                player.State = new BigRunningMarioState(player);
                player.PlayerSprite = new PlayerSpriteBigRunning(player.PlayerSprite);
            }
            else
                player.PlayerSprite.isFacingRight = true;
        }

        public void Up()
        {
            player.State = new PlayerStateBigJumping(player);
            player.PlayerSprite = new PlayerSpriteBigJumping(player.PlayerSprite);
            player.PlayerSprite.soundInstance.Play();
        }

        public void Down()
        {
            player.State = new BigCrouchMarioState(player);
            player.PlayerSprite = new PlayerSpriteBigCrouch(player.PlayerSprite);
        }

        public void Hit()
        {
            player.State = new PlayerStateCollectBlinking(player, new PlayerStateSmallIdle(player));
            player.PlayerSprite = new TransitionSprite(player.PlayerSprite, new PlayerSpriteSmallIdle(player.PlayerSprite), -1);
            player.MyState = 1;
            player.PlayerSprite.soundInstance.Play();
        }

        public void Collect(IItem item)
        {
            if (item is FireFlowerItem)
            {
                player.State = new PlayerStateCollectBlinking(player, new FireIdleMarioState(player));
                player.PlayerSprite = new TransitionSprite(player.PlayerSprite, new PlayerSpriteFireIdle(player.PlayerSprite), 1);
                player.MyState = 3;
                player.PlayerSprite.soundInstance.Play();
            }
            else if (item is MushroomItem)
            {
                // Do nothing since Mario is already in big stage.
            }
            else if (item is DeadMushroomItem)
            {
                player.State = new DeadMarioState(player);
                player.PlayerSprite = new PlayerSpriteDead(player.PlayerSprite);
            }
        }

        public void Update(GameTime gameTime)
        {
            player.PlayerSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            player.PlayerSprite.Draw(spriteBatch, camera);
        }
    }
}
