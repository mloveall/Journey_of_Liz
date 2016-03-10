using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Classes.MiscClasses
{
    public class Portal
    {
        Texture2D texture; //spritesheet for animation
        public Rectangle DestRectangle { get; set; }
        private static int height = 96, width = 32;
        public int portalIndex;
        Portal outPortal;
        public bool facingLeft;
        private bool recentlyUsed = false;
        private static int dontWarpTime = 30;
        private int timeSinceUse = 0;

        public Portal(Texture2D texture, Vector2 location, int portalIndex, bool facingLeft)
        {
            this.texture = texture;
            this.facingLeft = facingLeft;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            this.portalIndex = portalIndex;
        }

        //spriteBatch will be the spritebatch used for this animation, location is where we want it drawn
        public void Update()
        {
            if (recentlyUsed)
            {
                timeSinceUse++;
                if (timeSinceUse >= dontWarpTime)
                {
                    recentlyUsed = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), width, height);

            if (facingLeft)
            {
                spriteBatch.Draw(texture, relativeDestRectangle, null, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, relativeDestRectangle, null, Color.White,0f,new Vector2(),SpriteEffects.FlipHorizontally,0.5f);
            }

        }

        public void Warp(Player player)
        {
            if (!recentlyUsed)
            {
                int x, y;

                if (outPortal.facingLeft)
                {
                    x = outPortal.DestRectangle.X - player.playerSprite.destRectangle.Width - outPortal.DestRectangle.Width;
                }
                else
                {
                    x = outPortal.DestRectangle.X + outPortal.DestRectangle.Width;
                }

                y = outPortal.DestRectangle.Y + (player.playerSprite.destRectangle.Y - this.DestRectangle.Y);

                if (this.facingLeft == outPortal.facingLeft)
                {
                    player.playerSprite.isFacingRight = !player.playerSprite.isFacingRight;
                }
                player.MoveTo(x, y);
                this.recentlyUsed = true;
                outPortal.recentlyUsed = true;
                timeSinceUse = 0;
            }

        }


        public void setOutPortal(Portal outPortal)
        {
            this.outPortal = outPortal;
        }
    }
}
