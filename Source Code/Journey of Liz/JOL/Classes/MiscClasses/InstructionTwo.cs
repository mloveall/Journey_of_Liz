using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JOL.Interfaces;

namespace JOL.Classes.BlockClasses
{
    public class InstructionTwo
    {
        public Texture2D texture;
        public Rectangle DestRectangle { get; set; }
        private int height = 32, width = 300;

        public InstructionTwo(Texture2D texture, Vector2 location)
        {
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle destRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), width, height);

            spriteBatch.Draw(texture, destRectangle, Color.White);
        }
    }
}
