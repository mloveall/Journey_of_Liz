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

namespace JOL.PlayerStates
{
    /// <summary>
    /// Running state of the demo player.
    /// </summary>
    
    class PlayerStateDemoRunning : PlayerState
    {
        public PlayerStateDemoRunning(Player player) : base(player)
        {

        }

        public override void Left()
        {
            if (player.playerSprite.isFacingRight == true)
            {
                player.playerState = new FireIdleMarioState(player);
                player.playerSprite = new PlayerSpriteDemoIdle(player.playerSprite);
            }
            else
            {
                player.playerSprite.velocity = 3;
            }
        }

        public override void Right()
        {
            if (player.playerSprite.isFacingRight == false)
            {
                player.playerState = new FireIdleMarioState(player);
                player.playerSprite = new PlayerSpriteDemoIdle(player.playerSprite);
            }
            else
            {
                player.playerSprite.velocity = 3;
            }
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
            player.playerState = new PlayerStateCollectBlinking(player, new PlayerStateRidingRunning(player));
            player.playerSprite = new TransitionSprite(player.playerSprite, new PlayerSpriteRidingRunning(player.playerSprite), -1);
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

        public override void Update(GameTime gameTime)
        {
            player.playerSprite.Update(gameTime);
            if (player.playerSprite.velocity <= 0.0f)
            {
                player.playerState = new FireIdleMarioState(player);
                player.playerSprite = new PlayerSpriteDemoIdle(player.playerSprite);
            }
            if (player.playerSprite.fallSpeed >= 1f)
            {
                player.playerState = new PlayerStateDemoJumping(player);
                player.playerSprite = new PlayerSpriteDemoJumping(player.playerSprite);
                player.playerSprite.fallSpeed = 1f;
            }
        }
    }
}
