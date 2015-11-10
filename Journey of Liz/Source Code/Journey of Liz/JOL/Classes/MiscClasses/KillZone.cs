using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JOL.Classes.BlockClasses
{
    public class KillZone
    {
        public Rectangle DestRectangle { get; set; }
        private int height = 32, width = 32;

        public KillZone(Vector2 location)
        {
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
        }
    }
}
