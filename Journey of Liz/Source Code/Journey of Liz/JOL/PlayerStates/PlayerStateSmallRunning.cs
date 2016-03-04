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
    /// Running state of the small Mario.
    /// </summary>

    class PlayerStateSmallRunning : IPlayerState
    {
        Player player;

        public PlayerStateSmallRunning(Player player)
        {
            this.player = player;
        }

        public void Left()
        {
            if (player.PlayerSprite.isFacingRight == true)
            {
                player.State = new PlayerStateSmallIdle(player);
                player.PlayerSprite = new PlayerSpriteSmallIdle(player.PlayerSprite);
            }
            else
            {
                player.PlayerSprite.velocity = 3;
            }
        }

        public void Right()
        {
            if (player.PlayerSprite.isFacingRight == false)
            {
                player.State = new PlayerStateSmallIdle(player);
                player.PlayerSprite = new PlayerSpriteSmallIdle(player.PlayerSprite);
            }
            else
            {
                player.PlayerSprite.velocity = 3;
            }
        }

        public void Up()
        {
            player.State = new PlayerStateSmallJumping(player);
            player.PlayerSprite = new PlayerSpriteSmallJumping(player.PlayerSprite);
            player.PlayerSprite.soundInstance.Play();
        }

        public void Down()
        {
            // Do nothing.
        }

        public void Hit()
        {
            player.State = new DeadMarioState(player);
            player.PlayerSprite = new PlayerSpriteDead(player.PlayerSprite);
            player.MyState = 0;
            player.level.lives--;
            player.level.dyingAnimation = true;
            player.MediaManager(2);
            player.PlayerSprite.soundInstance.Play();
        }

        public void Collect(IItem item)
        {
            if (item is FireFlowerItem)
            {
                player.State = new PlayerStateCollectBlinking(player, new PlayerStateDemoRunning(player));
                player.PlayerSprite = new TransitionSprite(player.PlayerSprite, new PlayerSpriteFireRunning(player.PlayerSprite), 1);
                player.MyState = 3;
                player.PlayerSprite.soundInstance.Play();
            }
            else if (item is MushroomItem)
            {
                player.State = new PlayerStateCollectBlinking(player, new BigRunningMarioState(player));
                player.PlayerSprite = new TransitionSprite(player.PlayerSprite, new PlayerSpriteBigRunning(player.PlayerSprite), 1);
                player.MyState = 2;
                player.PlayerSprite.soundInstance.Play();
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
            if (player.PlayerSprite.velocity <= 0.0f)
            {
                player.State = new PlayerStateSmallIdle(player);
                player.PlayerSprite = new PlayerSpriteSmallIdle(player.PlayerSprite);
            }
            if (player.PlayerSprite.fallSpeed >= 1f)
            {
                player.State = new PlayerStateSmallJumping(player);
                player.PlayerSprite = new PlayerSpriteSmallJumping(player.PlayerSprite);
                player.PlayerSprite.fallSpeed = 1f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            player.PlayerSprite.Draw(spriteBatch, camera);
        }
    }
}