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
    public class CoinItem: IItem
    {
        public bool toDelete { get; set; }
        public bool isActive { get; set; }
        bool isSpawning = false;
        Texture2D sprite;
        public Rectangle DestRectangle { get; set; }
        SoundEffect sound;
        SoundEffectInstance soundInstance;

        public float FallSpeed { get; set; }
        int xPosDest, yPosDest;
        int xPosSource = 2, yPosSource = 2, magnifier = 2,spawnHeight=0;
        int currentFrame = 0, frameDelayClock = 0;
        bool check = true;
        private static int HEIGHT = 16, WIDTH = 12, NUMBER_OF_FRAMES = 4, FRAME_WIDTH = 20, FRAME_DELAY = 10;

        public CoinItem()
        {
            
        }

        public CoinItem(Texture2D sprite, int xPos, int yPos, bool isActive)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            toDelete = false;
            this.isActive = isActive;
        }

        public CoinItem(Texture2D sprite, SoundEffect sound, int xPos, int yPos, bool isActive)
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
                    yPosDest-= 2;
                    spawnHeight++;
                }
                else
                {
                    toDelete = true;
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
            Rectangle destRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), magnifier * WIDTH, magnifier * HEIGHT);
            if (isActive || isSpawning)
            {
                FRAME_WIDTH = sprite.Width / NUMBER_OF_FRAMES;
                Rectangle sourceRectangle = new Rectangle(xPosSource + currentFrame * FRAME_WIDTH, yPosSource, WIDTH, HEIGHT);
                spriteBatch.Draw(sprite, destRectangle, sourceRectangle, Color.White);
            }
        }

        public void Collect()
        {
            soundInstance.Play();
            isActive = false;
            toDelete = true;
            DestRectangle = new Rectangle(1800, 1800, magnifier * WIDTH, magnifier * HEIGHT);
        }

        public void Reset()
        {
            check = true;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
        }

        public void Spawn()
        {
            isSpawning = true;
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
