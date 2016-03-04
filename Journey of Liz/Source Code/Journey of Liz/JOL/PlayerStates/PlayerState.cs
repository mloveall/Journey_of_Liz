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

    class PlayerState : IPlayerState
    {
        protected Player player;

        public PlayerState(Player player)
        {
            this.player = player;
        }

        public virtual void Left()
        {
            
        }

        public virtual void Right()
        {

        }

        public virtual void Up()
        {

        }

        public virtual void Down()
        {

        }

        public virtual void Hit()
        {

        }

        public virtual void Collect(IItem item)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            player.PlayerSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            player.PlayerSprite.Draw(spriteBatch, camera);
        }
    }
}