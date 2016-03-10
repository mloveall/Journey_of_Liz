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
    /// Death state of the player.
    /// </summary>

    class PlayerStateDead : PlayerState
    {
        public PlayerStateDead(Player player) : base(player)
        {
            
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
    }
}
