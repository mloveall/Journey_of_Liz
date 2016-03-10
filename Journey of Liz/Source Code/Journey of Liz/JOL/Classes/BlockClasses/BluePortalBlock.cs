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

    class BluePortalBlock : Block
    {
        public int portalIndex;

        private int currentFrame = 0; //keeps track of which frame to use
        private int totalFrames = 3;

        OrangePortalBlock outPortal;
        Texture2D usedTexture;
        float timer = 0f;

        // Constructor
        public BluePortalBlock(Texture2D texture, Texture2D usedTexture, Vector2 location, int portalIndex) : base(texture, location)
        {
            height = 32;
            width = 32;
            this.usedTexture = usedTexture;
            this.portalIndex = portalIndex;

            Initialize();
        }

        // Update is called every frame
        public override void Update(GameTime gameTime)
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

        public override void Reset()
        {
            isAlive = true;
        }

        // "SpriteBatch" will be the spritebatch used for this animation, "location" is where we want it drawn
        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            Rectangle sourceRectangle = new Rectangle(width*currentFrame,0, width,height);
            if (isAlive)
                spriteBatch.Draw(texture, relativeDestRectangle, sourceRectangle, Color.White);
            else
                spriteBatch.Draw(usedTexture, relativeDestRectangle, Color.White);
        }

        public override void Bump(Player player)
        {
            if (isAlive)
            {
                int x = outPortal.destRectangle.X;
                int y = outPortal.destRectangle.Y + player.playerSprite.destRectangle.Height;
                player.MoveTo(x, y);
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
