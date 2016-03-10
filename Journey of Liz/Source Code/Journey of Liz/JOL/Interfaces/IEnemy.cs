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
using JOL.Interfaces;

namespace JOL
{
    /// <summary>
    /// An abstract base class for various enemies.
    /// </summary>

    public interface IEnemy : IDeletable
    {
        Rectangle destRectangle { get; set; }
        bool isAlive { get; set; }
        float fallSpeed { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, ICamera camera);
        void MoveTo(int xPosition, int yPosition);
        void Flip();
        bool Hit(CollisionDetection.CollisionType collisionType, bool hitRight); //returns bool determining whether enemy hit mario
    }
}
