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

    class HiddenBlock : Block
    {
        public bool isHidden;
        IItemContainer itemContainer;
        Texture2D exposedTexture, hiddenTexture;

        // Constructor
        public HiddenBlock(Texture2D hiddenTexture, Texture2D exposedTexture, Vector2 location, IItemContainer itemContainer) : base(exposedTexture, location)
        {
            height = 32;
            width = 32;
            this.hiddenTexture = hiddenTexture;
            this.exposedTexture = exposedTexture;
            this.isHidden = true;
            this.itemContainer = itemContainer;

            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Nothing to update for used block since no animation or change
        }

        public override void Reset()
        {
            isHidden = true;
        }

        // Don't draw anything until it is being bumbed.
        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            if(isHidden)
                spriteBatch.Draw(hiddenTexture, relativeDestRectangle, Color.White);
            else
                spriteBatch.Draw(exposedTexture, relativeDestRectangle, Color.White);
        }

        public override void Bump(Player player)
        {
            bool isSmallMario = false;
            if (player.myState == 1)
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
