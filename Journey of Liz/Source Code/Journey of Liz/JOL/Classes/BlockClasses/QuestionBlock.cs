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
    /// A gift block that can be bumped to get rewards.
    /// </summary>

    public class QuestionBlock : IBlock
    {
        // Global variables
        public Rectangle DestRectangle { get; set; }
        public bool toDelete { get; set; }
        public bool isAlive;
        
        private int currentFrame = 0; //keeps track of which frame to use
        private int totalFrames = 3;
        private int height = 32, width = 32;
        
        IItemContainer itemContainer;
        Texture2D aliveTexture, usedTexture; // Spritesheet for animation
        Vector2 location;
        float timer = 0f;

        // Constructor
        public QuestionBlock(Texture2D aliveTexture, Texture2D usedTexture, Vector2 location, IItemContainer itemContainer) 
        {
            isAlive = true;
            this.aliveTexture = aliveTexture;
            this.usedTexture = usedTexture;
            this.location = location;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            toDelete = false;
            this.itemContainer = itemContainer;
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
            isAlive = true;
        }

        // "SpriteBatch" will be the spritebatch used for this animation, "location" is where we want it drawn
        public void Draw(SpriteBatch spriteBatch, ICamera camera)
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

        // Bump is called when Mario hits the bottom of the block
        public void Bump(Player mario)
        {
            bool isSmallMario = false;
            if (mario.MyState == 1)
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
