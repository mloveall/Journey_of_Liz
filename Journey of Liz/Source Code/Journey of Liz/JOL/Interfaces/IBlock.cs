using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JOL.Interfaces
{
    /// <summary>
    /// An abstract base class for various blocks.
    /// </summary>

    public interface IBlock : IDeletable
    {
        Rectangle destRectangle { get; set; }
        void Update(GameTime gameTime);
        void Reset();
        void Draw(SpriteBatch spriteBatch, ICamera camera);
        void Bump(Player mario);
    }
}
