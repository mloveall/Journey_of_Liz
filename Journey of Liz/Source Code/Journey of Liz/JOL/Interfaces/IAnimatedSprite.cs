using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JOL
{
    /// <summary>
    /// An abstract base class for animating various Sprites.
    /// </summary>

    public interface IAnimatedSprite
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Texture2D texture);
    }
}
