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

    class UsedBlock : IBlock
    {
        // Global variables
        public Rectangle DestRectangle { get; set; }
        public Texture2D Texture { get; set; }
        public bool toDelete { get; set; }

        private int height = 32, width = 32;

        Vector2 location;

        // Constructor
        public UsedBlock(Texture2D texture, Vector2 location)
        {
            this.location = location;
            Texture = texture;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
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
            Rectangle destRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            spriteBatch.Draw(Texture, destRectangle, Color.White);
        }

        public void Bump(Mario mario)
        {
           
        }
    }
}
