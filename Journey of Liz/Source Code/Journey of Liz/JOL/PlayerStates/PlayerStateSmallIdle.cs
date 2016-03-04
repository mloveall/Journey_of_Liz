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
    /// Idle state of the small Mario.
    /// </summary>

    class PlayerStateSmallIdle : PlayerState
    {
        public PlayerStateSmallIdle(Player player) : base(player)
        {
            this.player = player;
        }

        public override void Left()
        {
            if (player.PlayerSprite.isFacingRight == false)
            {
                player.State = new PlayerStateSmallRunning(player);
                player.PlayerSprite = new PlayerSpriteSmallRunning(player.PlayerSprite);
            }
            else
                player.PlayerSprite.isFacingRight = false;
        }

        public override void Right()
        {
            if (player.PlayerSprite.isFacingRight == true)
            {
                player.State = new PlayerStateSmallRunning(player);
                player.PlayerSprite = new PlayerSpriteSmallRunning(player.PlayerSprite);
            }
            else
                player.PlayerSprite.isFacingRight = true;
        }

        public override void Up()
        {
            player.State = new PlayerStateSmallJumping(player);
            player.PlayerSprite = new PlayerSpriteSmallJumping(player.PlayerSprite);
            player.PlayerSprite.soundInstance.Play();
        }

        public override void Down()
        {
            player.State = new PlayerStateSmallCrouch(player);
            player.PlayerSprite = new PlayerSpriteSmallCrouch(player.PlayerSprite);
        }

        public override void Hit()
        {
            player.State = new DeadMarioState(player);
            player.PlayerSprite = new PlayerSpriteDead(player.PlayerSprite);
            player.MyState = 0;
            player.level.lives--;
            player.level.dyingAnimation = true;
            player.MediaManager(2);
            player.PlayerSprite.soundInstance.Play();
        }

        public override void Collect(IItem item)
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
                player.State = new PlayerStateCollectBlinking(player, new BigIdleMarioState(player));
                player.PlayerSprite = new TransitionSprite(player.PlayerSprite, new PlayerSpriteBigIdle(player.PlayerSprite), 1);
                player.MyState = 2;
                player.PlayerSprite.soundInstance.Play();
            }
            else if (item is DeadMushroomItem)
            {
                player.State = new DeadMarioState(player);
                player.PlayerSprite = new PlayerSpriteDead(player.PlayerSprite);
                player.MyState = 0;
            }
        }
    }
}
