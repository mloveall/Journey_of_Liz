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

namespace JOL.PlayerStates
{
    /// <summary>
    /// Jumping state of the demo player.
    /// </summary>

    class PlayerStateDemoJumping : PlayerState
    {
        public PlayerStateDemoJumping(Player player) : base(player)
        {

        }

        public override void Left()
        {
            if (player.playerSprite.isFacingRight == false)
            {
                player.playerSprite.isMoving = true;
            }
            else
                player.playerSprite.isFacingRight = false;
        }

        public override void Right()
        {
            if (player.playerSprite.isFacingRight == true)
            {
                player.playerSprite.isMoving = true;
            }
            else
                player.playerSprite.isFacingRight = true;
        }

        public override void Hit()
        {
            player.playerState = new PlayerStateCollectBlinking(player, new PlayerStateRidingJumping(player));
            player.playerSprite = new TransitionSprite(player.playerSprite, new PlayerSpriteRidingJumping(player.playerSprite), -1);
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