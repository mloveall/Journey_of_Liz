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
    /// Jumping state of the small Mario.
    /// </summary>

    class PlayerStateSmallJumping: IPlayerState
    {
        Player player;

        public PlayerStateSmallJumping(Player player)
        {
            this.player = player;
        }

        public void Left()
        {
            if (player.PlayerSprite.isFacingRight == false)
            {
                player.PlayerSprite.isMoving = true;
            }
            else
                player.PlayerSprite.isFacingRight = false;
        }

        public void Right()
        {
            if (player.PlayerSprite.isFacingRight == true)
            {
                player.PlayerSprite.isMoving = true;
            }
            else
                player.PlayerSprite.isFacingRight = true;
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
            if (item.GetType().Equals(new FireFlowerItem().GetType()))
            {
                player.State = new PlayerStateCollectBlinking(player, new PlayerStateDemoJumping(player));
                player.PlayerSprite = new TransitionSprite(player.PlayerSprite, new PlayerSpriteFireJumping(player.PlayerSprite), 1);
                player.MyState = 3;
                player.PlayerSprite.soundInstance.Play();
            }
            else if (item.GetType().Equals(new MushroomItem().GetType()))
            {
                player.State = new PlayerStateCollectBlinking(player, new PlayerStateBigJumping(player));
                player.PlayerSprite = new TransitionSprite(player.PlayerSprite, new PlayerSpriteBigJumping(player.PlayerSprite), 1);
                player.MyState = 2;
                player.PlayerSprite.soundInstance.Play();
            }
            else if (item.GetType().Equals(new DeadMushroomItem().GetType()))
            {
                player.State = new DeadMarioState(player);
                player.PlayerSprite = new PlayerSpriteDead(player.PlayerSprite);
                player.MyState = 0;
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
