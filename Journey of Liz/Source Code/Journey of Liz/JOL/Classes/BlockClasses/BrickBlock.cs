using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Classes.ItemClasses;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Classes.BlockClasses
{
    /// <summary>
    /// A basic brick block that can be destroyed through bumping.
    /// </summary>

    class BrickBlock : IBlock
    {
        // Global variables
        public Rectangle DestRectangle { get; set; }
        public Texture2D Texture { get; set; }
        public bool toDelete { get; set; }
        public bool isAlive;

        private int height = 32, width = 32;

        IItemContainer itemContainer;
        Vector2 location;
        SoundEffect sound;
        SoundEffectInstance soundInstance;

        // Basic constructor
        public BrickBlock(Texture2D texture, Vector2 location, IItemContainer itemContainer)
        {
            Texture = texture;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            isAlive = true;
            this.location = location;
            toDelete = false;
            this.itemContainer = itemContainer;
        }

        // Constructor with sound effect parameter
        public BrickBlock(Texture2D texture, Vector2 location, IItemContainer itemContainer, SoundEffect sound)
        {
            Texture = texture;
            DestRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            isAlive = true;
            this.location = location;
            toDelete = false;
            this.itemContainer = itemContainer;
            this.sound = sound;
            soundInstance = sound.CreateInstance();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Reset()
        {
            if (isAlive == false)
            {
                isAlive = true;
                location.X -= 1800;
                location.Y -= 1800;
                DestRectangle = new Rectangle((int)location.X, (int)location.Y, 32, 32);
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(location.X - camera.Position.X), (int)(location.Y - camera.Position.Y), width, height);
            if(isAlive)
                spriteBatch.Draw(Texture, relativeDestRectangle, Color.White);
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
                toDelete = true;
                soundInstance.Play();
            }
            else
            {
                IItem item = itemContainer.ProduceItem(isSmallMario);
                item.Spawn();
                if (item is CoinItem)
                {
                    mario.Collect(item);
                }
            }
        }
    }
}
