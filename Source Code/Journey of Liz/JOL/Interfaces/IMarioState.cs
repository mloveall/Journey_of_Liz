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
using JOL.Interfaces;

namespace JOL
{
    /// <summary>
    /// An abstract base class for various Mario states.
    /// </summary>

    public interface IMarioState
    {
        void Left();
        void Right();
        void Up();
        void Down();
        void Hit();
        void Collect(IItem item);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, ICamera camera);
    }
}
