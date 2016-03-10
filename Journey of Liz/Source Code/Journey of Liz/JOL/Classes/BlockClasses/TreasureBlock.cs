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
    /// A treasure block that can be bumped to get rewards.
    /// </summary>

    class TreasureBlock : Block
    {
        private int currentFrame = 0; //keeps track of which frame to use
        private int totalFrames = 3;
        
        IItemContainer itemContainer;
        Texture2D aliveTexture, usedTexture; // Spritesheet for animation
        float timer = 0f;

        // Constructor
        public TreasureBlock(Texture2D aliveTexture, Texture2D usedTexture, Vector2 location, IItemContainer itemContainer) : base(aliveTexture, location)
        {
            height = 32;
            width = 32;
            this.aliveTexture = aliveTexture;
            this.usedTexture = usedTexture;
            this.itemContainer = itemContainer;

            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Will produce the next frame to draw
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

        // "spriteBatch" will be the sprite batch used for this animation, "location" is where we want it drawn
        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            if (isAlive)
            {
                Rectangle sourceRectangle = new Rectangle(width*currentFrame,0, width,height);
                spriteBatch.Draw(aliveTexture, relativeDestRectangle, sourceRectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(usedTexture, relativeDestRectangle, Color.White);
            }
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
                isAlive = false;
            }
            else
            {
                IItem item = itemContainer.ProduceItem(isSmallMario);
                item.Spawn();
                if (itemContainer.IsEmpty())
                {
                    isAlive = false;
                }
            }
        }
    }
}
