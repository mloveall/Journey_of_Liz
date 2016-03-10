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
    /// A general block class that is being used as the parent of every specific block.
    /// </summary>

    public class Block : IBlock
    {
        // Global variables
        public Rectangle destRectangle { get; set; }
        public Texture2D texture { get; set; }
        public bool toDelete { get; set; }
        public bool isAlive { get; set; }
        public int height { get; protected set; }
        public int width { get; protected set; }

        protected Vector2 location;

        // Constructor
        public Block(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;
            this.toDelete = false;
            this.isAlive = true;
        }

        public void Initialize()
        {
            destRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Reset()
        {

        }

        // Bump is called when player hits the bottom of the block
        public virtual void Bump(Player player)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            if (isAlive)
            {
                Rectangle relativeDestRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
                spriteBatch.Draw(texture, relativeDestRectangle, Color.White);
            }
        }
    }
}
