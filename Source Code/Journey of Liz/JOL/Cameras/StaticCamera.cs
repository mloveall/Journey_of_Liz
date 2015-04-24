using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JOL
{
    /// <summary>
    /// Static camera can be used to display a specific place of a scene.
    /// </summary>
    
    public class StaticCamera : ICamera
    {
        // Global variables
        public Vector2 Position { get; set; }

        private int height, width;

        // Constructor
        public StaticCamera()
        {
            Position = new Vector2(0, 0);
            this.height = 0;
            this.width = 0;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public bool IsInView(Rectangle rectangle)
        {
            Rectangle cameraRectangle = new Rectangle((int)Position.X, (int)Position.Y, height, width);
            return cameraRectangle.Intersects(rectangle);
        }

    }
}