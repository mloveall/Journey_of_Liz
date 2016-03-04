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
    /// An indestructible block that mainly used for decoration.
    /// </summary>

    class SandBlock : IBlock
    {
        // Global variables
        public Rectangle DestRectangle { get; set; }
        public Texture2D Texture { get; set; }
        public bool toDelete { get; set; }

        private int height = 32, width = 32;

        Vector2 location;

        // Constructor
        public SandBlock(Texture2D texture, Vector2 location)
        {
            Texture = texture;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            this.location = location;
            toDelete = false;
        }

        public void Update(GameTime gameTime)
        {
            //nothing to update for used block since no animation or change
        }

        public void Reset()
        {

        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            spriteBatch.Draw(Texture, relativeDestRectangle, Color.White);
        }

        public void Bump(Player mario)
        {
            
        }
    }
}
