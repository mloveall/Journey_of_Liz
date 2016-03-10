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
    /// Animating the transition from one stage to another.
    /// </summary>

    class PlayerStateCollectBlinking : PlayerState
    {
        IPlayerState nextPlayerState;

        public PlayerStateCollectBlinking(Player player, IPlayerState nextPlayerState) : base(player)
        {
            this.nextPlayerState = nextPlayerState;
        }

        // Disable all the controls
        public override void Left()
        {
        }

        public override void Right()
        {
        }

        public override void Up()
        {
        }

        public override void Down()
        {
        }

        public override void Hit()
        {
        }

        public override void Collect(IItem item)
        {
        }

        public override void Update(GameTime gameTime)
        {
            player.playerSprite.Update(gameTime);
            TransitionSprite sprite = (TransitionSprite) player.playerSprite;
            if (sprite.doneTransitioning)
            {
                player.playerState = nextPlayerState;
                player.playerSprite = sprite.nextPlayerSprite;
            }
        }
    }
}
