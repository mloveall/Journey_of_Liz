using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JOL.Interfaces;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Classes.ItemClasses
{
    /// <summary>
    /// A general item class that is being used as the parent of every specific item.
    /// </summary>

    public class Item : IItem
    {
        public Rectangle destRectangle { get; set; }
        public int height { get; protected set; }
        public int width { get; protected set; }
        public bool toDelete { get; set; }
        public bool isActive { get; set; }
        public float fallSpeed { get; set; }

        protected int xPosDest, yPosDest;
        protected int xPosSource = 2, yPosSource = 2, magnifier = 2, spawnHeight = 0;
        protected bool isSpawning = false;
        protected bool isFacingRight = true;
        protected Texture2D sprite;
        protected SoundEffect sound;
        protected SoundEffectInstance soundInstance;

        public Item()
        {

        }

        public Item(Texture2D sprite, int xPos, int yPos, bool isActive)
        {
            this.sprite = sprite;
            this.isActive = isActive;
            xPosDest = xPos;
            yPosDest = yPos;
            toDelete = false;
        }

        public Item(Texture2D sprite, SoundEffect sound, int xPos, int yPos, bool isActive)
        {
            this.sprite = sprite;
            this.isActive = isActive;
            this.sound = sound;
            soundInstance = sound.CreateInstance();
            xPosDest = xPos;
            yPosDest = yPos;
            toDelete = false;
        }

        public void Initialize()
        {
            destRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, ICamera camera)
        {

        }

        public virtual void Collect()
        {

        }

        public virtual void Spawn()
        {
            isSpawning = true;
        }

        public virtual void Reset()
        {
            destRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
        }

        public void Flip()
        {
            isFacingRight = !isFacingRight;
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            xPosDest = xPosition;
            yPosDest = yPosition;
            destRectangle = new Rectangle(xPosition, yPosition, magnifier * width, magnifier * height);
        }
    }
}