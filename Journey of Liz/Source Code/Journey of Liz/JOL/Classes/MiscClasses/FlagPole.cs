using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JOL.Classes.MiscClasses
{
    public class FlagPole
    {
        public Rectangle DestRectangle;
        public Rectangle CollisionRectangle;
        public Texture2D texture;

        private int height = 304, width = 40;
        private int collidableWidth = 10;

        public FlagPole(Texture2D texture, Vector2 location)
        {
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            CollisionRectangle = new Rectangle((int)location.X + width - collidableWidth, (int)location.Y, collidableWidth, height);
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), width, height);

            spriteBatch.Draw(texture, relativeDestRectangle, Color.White);
        }
    }
}
