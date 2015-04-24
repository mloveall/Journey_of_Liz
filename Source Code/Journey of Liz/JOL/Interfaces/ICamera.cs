using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JOL.Interfaces
{
    /// <summary>
    /// An abstract base class for various cameras.
    /// </summary>

    public interface ICamera
    {
        Vector2 Position { get; set; }
        bool IsInView(Rectangle rectangle);
        void Update(GameTime gameTime);
    }

}
