using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JOL.Interfaces;

namespace JOL
{
    public class FireFlowerItem : IItem
    {
        public bool toDelete { get; set; }
        Texture2D sprite;
        public Rectangle DestRectangle { get; set; }
        public bool isActive { get; set; }
        bool isSpawning = false;
        public float FallSpeed { get; set; }
        int xPosDest, yPosDest;
        int xPosSource = 2, yPosSource = 2, magnifier = 2;
        int currentFrame = 0, frameDelayClock=0, spawnHeight=0;
        private static int NUMBER_OF_FRAMES = 4, FRAME_WIDTH = 20, HEIGHT = 16, WIDTH = 16, FRAME_DELAY = 15;
        SoundEffect sound;
        SoundEffectInstance soundInstance;

        public FireFlowerItem()
        {
            
        }

        public FireFlowerItem(Texture2D sprite, int xPos, int yPos, bool isActive)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            toDelete = false;
            this.isActive = isActive;
        }

        public FireFlowerItem(Texture2D sprite, int xPos, int yPos, bool isActive, SoundEffect sound)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            toDelete = false;
            this.isActive = isActive;
            this.sound = sound;
            soundInstance = sound.CreateInstance();
        }

        public void Update(GameTime gameTime)
        {
            if (isSpawning)
                {

                    if (spawnHeight < HEIGHT * magnifier)
                    {
                        yPosDest--;
                        spawnHeight++;
                    }
                    else
                    {
                        isSpawning = false;
                        isActive = true;
                    }
                }
            if (isActive || isSpawning)
            {
                frameDelayClock++;
                if (frameDelayClock >= FRAME_DELAY)
                {
                    frameDelayClock = 0;
                    currentFrame++;
                    currentFrame = currentFrame % NUMBER_OF_FRAMES;
                }
            }
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), magnifier * WIDTH, magnifier * HEIGHT);
            if (isActive || isSpawning)
            {
                FRAME_WIDTH = sprite.Width / NUMBER_OF_FRAMES;
                Rectangle sourceRectangle = new Rectangle(xPosSource + currentFrame * FRAME_WIDTH, yPosSource, WIDTH, HEIGHT);
                spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
            }
        }

        public void Collect()
        {
            isActive = false;
            toDelete = true;
            DestRectangle = new Rectangle(1800, 1800, magnifier * WIDTH, magnifier * HEIGHT);
        }

        public void Reset()
        {
            isActive = true;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
        }

        public void Spawn()
        {
            isSpawning = true;
            soundInstance.Play();
        }

        public void Flip()
        {

        }

        public void MoveTo(int xPosition, int yPosition)
        {
            xPosDest = xPosition;
            yPosDest = yPosition;
            DestRectangle = new Rectangle(xPosition, yPosition, magnifier * WIDTH, magnifier * HEIGHT);
        }
    }
}
