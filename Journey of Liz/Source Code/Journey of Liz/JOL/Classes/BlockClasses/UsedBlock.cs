using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JOL.Classes.BlockClasses
{
    /// <summary>
    /// An indestructible block that is used to display a disabled question block.
    /// </summary>

    class UsedBlock : Block
    {
        // Constructor
        public UsedBlock(Texture2D texture, Vector2 location) : base(texture, location)
        {
            height = 32;
            width = 32;

            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Nothing to update for used block since no animation or change
        }

        public override void Reset()
        {
            // Nothing need to be reset
        }

        public override void Bump(Player mario)
        {
            // Nothing happens when being bumped
        }
    }
}
