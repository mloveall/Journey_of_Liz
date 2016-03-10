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
using JOL.Interfaces;

namespace JOL
{
    /// <summary>
    /// Idle state of the demo player.
    /// </summary>

    class FireIdleMarioState : PlayerState
    {
        public FireIdleMarioState(Player player) : base(player)
        {
            
        }

        public override void Left()
        {
            if (player.playerSprite.isFacingRight == false)
            {
                player.playerState = new PlayerStateDemoRunning(player);
                player.playerSprite = new PlayerSpriteDemoRunning(player.playerSprite);
            }
            else
                player.playerSprite.isFacingRight = false;
        }

        public override void Right()
        {
            if (player.playerSprite.isFacingRight == true)
            {
                player.playerState = new PlayerStateDemoRunning(player);
                player.playerSprite = new PlayerSpriteDemoRunning(player.playerSprite);
            }
            else
                player.playerSprite.isFacingRight = true;
        }

        public override void Up()
        {
            player.playerState = new PlayerStateDemoJumping(player);
            player.playerSprite = new PlayerSpriteDemoJumping(player.playerSprite);
            player.playerSprite.soundInstance.Play();
        }

        public override void Down()
        {
            player.playerState = new PlayerStateDemoCrouch(player);
            player.playerSprite = new PlayerSpriteDemoCrouch(player.playerSprite);
        }

        public override void Hit()
        {
            player.playerState = new PlayerStateCollectBlinking(player, new PlayerStateRidingIdle(player));
            player.playerSprite = new TransitionSprite(player.playerSprite, new PlayerSpriteRidingIdle(player.playerSprite), -1);
            player.myState = 2;
            player.playerSprite.soundInstance.Play();
        }

        public override void Collect(IItem item)
        {
            if (item is CheatPotionItem)
            {
                // Do nothing since alreay in demo state.
            }
            else if (item is BardieEggItem)
            {
                // Do nothing since alreay in demo state.
            }
            else if (item is DeathPotionItem)
            {
                player.playerState = new PlayerStateDead(player);
                player.playerSprite = new PlayerSpriteDead(player.playerSprite);
            }
        }
    }
}
