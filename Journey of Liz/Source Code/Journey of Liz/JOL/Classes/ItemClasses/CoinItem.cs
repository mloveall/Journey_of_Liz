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
    public class CoinItem: Item
    {
        private int currentFrame = 0, frameDelayClock = 0, frameWidth = 20;
        private const int NUMBER_OF_FRAMES = 4, FRAME_DELAY = 10;

        public CoinItem()
            : base()
        {
            width = 12;
            height = 16;

            xPosDest = 300;
            yPosDest = 100;
        }

        public CoinItem(Texture2D sprite, int xPos, int yPos, bool isActive)
            : base(sprite, xPos, yPos, isActive)
        {
            width = 12;
            height = 16;

            Initialize();
        }

        public CoinItem(Texture2D sprite, SoundEffect sound, int xPos, int yPos, bool isActive)
            : base(sprite, sound, xPos, yPos, isActive)
        {
            width = 12;
            height = 16;

            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (isSpawning)
            {
                if (spawnHeight < height * magnifier)
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
            destRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
        }

        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(destRectangle.X - camera.Position.X), (int)(destRectangle.Y - camera.Position.Y), magnifier * width, magnifier * height);
            if (isActive || isSpawning)
            {
                frameWidth = sprite.Width / NUMBER_OF_FRAMES;
                Rectangle sourceRectangle = new Rectangle(xPosSource + currentFrame * frameWidth, yPosSource, width, height);
                spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
            }
        }

        public override void Collect()
        {
            soundInstance.Play();
            isActive = false;
            toDelete = true;
            destRectangle = new Rectangle(1800, 1800, magnifier * width, magnifier * height);
        }
    }
}
