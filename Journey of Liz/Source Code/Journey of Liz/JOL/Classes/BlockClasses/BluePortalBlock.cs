using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Classes.BlockClasses
{
    /// <summary>
    /// The blue end of the portal block system.
    /// </summary>

    class BluePortalBlock : IBlock
    {
        // Global variables
        public Rectangle DestRectangle { get; set; }
        public int portalIndex;
        public bool toDelete { get; set; }
        public bool isAlive;
        
        private int currentFrame = 0; //keeps track of which frame to use
        private int totalFrames = 3;
        private int height = 32, width = 32;

        OrangePortalBlock outPortal;
        Vector2 location;
        Texture2D texture; //spritesheet for animation
        Texture2D dead;
        float timer = 0f;

        // Constructor
        public BluePortalBlock(Texture2D texture, Texture2D dead, Vector2 location, int portalIndex) 
        {
            this.texture = texture;
            this.dead = dead;
            this.location = location;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            toDelete = false;
            this.portalIndex = portalIndex;
            isAlive = true;
        }

        // Update is called every frame
        public void Update(GameTime gameTime)
        {
            //will produce the next frame to draw
            if (timer > 2.0f)
            {
                currentFrame++;
                if (currentFrame >= totalFrames)
                    currentFrame = 0;
                timer = 0.0f;
            }
            else
            {
                timer += 0.2f;
            }
        }

        public void Reset()
        {
            
        }

        // "SpriteBatch" will be the spritebatch used for this animation, "location" is where we want it drawn
        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle destRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            Rectangle sourceRectangle = new Rectangle(width*currentFrame,0, width,height);
            if (isAlive)
                spriteBatch.Draw(texture, destRectangle, sourceRectangle, Color.White);
            else
                spriteBatch.Draw(dead, destRectangle, Color.White);
        }

        public void Bump(Mario mario)
        {
            if (isAlive)
            {
                int x = outPortal.DestRectangle.X;
                int y = outPortal.DestRectangle.Y + mario.MarioSprite.DestRectangle.Height;
                mario.MoveTo(x, y);
            }
            outPortal.isAlive = true;
            isAlive = false;
        }

        public void setOutPortal(OrangePortalBlock opBlock)
        {
            outPortal = opBlock;
        }
    }
}
