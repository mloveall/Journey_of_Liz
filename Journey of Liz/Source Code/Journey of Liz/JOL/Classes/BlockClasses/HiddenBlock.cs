using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JOL.Classes.BlockClasses
{
    /// <summary>
    /// A hidden block that can be bumped to display.
    /// </summary>

    class HiddenBlock : IBlock
    {
        // Global variables
        public Rectangle DestRectangle { get; set; }
        public bool toDelete { get; set; }
        public bool isHidden;

        private int height = 32, width = 32;

        IItemContainer itemContainer;
        Texture2D exposedTexture, hiddenTexture;
        Vector2 location;

        // Constructor
        public HiddenBlock(Texture2D hiddenTexture, Texture2D exposedTexture, Vector2 location, IItemContainer itemContainer)
        {
            this.hiddenTexture = hiddenTexture;
            this.exposedTexture = exposedTexture;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            isHidden = true;
            this.location = location;
            toDelete = false;
            this.itemContainer = itemContainer;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Reset()
        {
            isHidden = true;
        }

        // Don't draw anything until it is being bumbed.
        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle destRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            if(isHidden)
                spriteBatch.Draw(hiddenTexture, destRectangle, Color.White);
            else
                spriteBatch.Draw(exposedTexture, destRectangle, Color.White);
        }

        // Bump is called when Mario hits the bottom of the block
        public void Bump(Mario mario)
        {
            bool isSmallMario = false;
            if (mario.MyState == 1)
            {
                isSmallMario = true;
            }
            if (itemContainer.IsEmpty())
            {
                isHidden = false;
            }
            else
            {
                IItem item = itemContainer.ProduceItem(isSmallMario);
                item.Spawn();
                if (itemContainer.IsEmpty())
                {
                    isHidden = false;
                }
            }
        }
    }
}
