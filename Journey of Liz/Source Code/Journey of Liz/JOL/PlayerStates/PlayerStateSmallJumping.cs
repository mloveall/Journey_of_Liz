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
    /// Jumping state of the small player.
    /// </summary>

    class PlayerStateSmallJumping: PlayerState
    {
        public PlayerStateSmallJumping(Player player) : base(player)
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
            player.playerState = new PlayerStateDead(player);
            player.playerSprite = new PlayerSpriteDead(player.playerSprite);
            player.myState = 0;
            player.level.lives--;
            player.level.dyingAnimation = true;
            player.MediaManager(2);
            player.playerSprite.soundInstance.Play();
        }

        public override void Collect(IItem item)
        {
            if (item.GetType().Equals(new CheatPotionItem().GetType()))
            {
                player.playerState = new PlayerStateCollectBlinking(player, new PlayerStateDemoJumping(player));
                player.playerSprite = new TransitionSprite(player.playerSprite, new PlayerSpriteDemoJumping(player.playerSprite), 1);
                player.myState = 3;
                player.playerSprite.soundInstance.Play();
            }
            else if (item.GetType().Equals(new BardieEggItem().GetType()))
            {
                player.playerState = new PlayerStateCollectBlinking(player, new PlayerStateRidingJumping(player));
                player.playerSprite = new TransitionSprite(player.playerSprite, new PlayerSpriteRidingJumping(player.playerSprite), 1);
                player.myState = 2;
                player.playerSprite.soundInstance.Play();
            }
            else if (item.GetType().Equals(new DeathPotionItem().GetType()))
            {
                player.playerState = new PlayerStateDead(player);
                player.playerSprite = new PlayerSpriteDead(player.playerSprite);
                player.myState = 0;
            }
        }
    }
}
