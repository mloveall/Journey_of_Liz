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
    /// An abstract base class for various items.
    /// </summary>
    
    public interface IItem : IDeletable
    {
        bool isActive { get; set; }
        Rectangle destRectangle { get; set; }
        float fallSpeed { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, ICamera camera);
        void Collect();
        void Reset();
        void Spawn();
        void MoveTo(int xPosition, int yPosition);
        void Flip();
    }
}
