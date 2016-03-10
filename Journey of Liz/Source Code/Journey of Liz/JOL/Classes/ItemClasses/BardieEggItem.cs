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

namespace JOL.Classes.ItemClasses
{
    public class BardieEggItem : Item
    {
        private const int MOVEMENT_SPEED = 2;

        public BardieEggItem()
            : base()
        {
            width = 16;
            height = 16;

            xPosDest = 300;
            yPosDest = 100;
        }

        public BardieEggItem(Texture2D sprite, int xPos, int yPos, bool isActive)
            : base(sprite, xPos, yPos, isActive)
        {
            width = 16;
            height = 16;

            Initialize();
        }

        public BardieEggItem(Texture2D sprite, int xPos, int yPos, bool isActive, SoundEffect sound)
            : base(sprite, sound, xPos, yPos, isActive)
        {
            width = 16;
            height = 16;

            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (isSpawning)
            {
                if (spawnHeight < height * magnifier)
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
            if (isActive)
            {
                    if (isFacingRight)
                    {
                        xPosDest += MOVEMENT_SPEED;
                    }
                    else
                    {
                        xPosDest -= MOVEMENT_SPEED;
                    }
                    yPosDest += (int)fallSpeed;
                    if (fallSpeed.CompareTo(10.0f) < 0)
                    {
                        fallSpeed = fallSpeed * 1.05f;
                    }
            }
            destRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
        }

        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(destRectangle.X - camera.Position.X), (int)(destRectangle.Y - camera.Position.Y), magnifier * width, magnifier * height);
            if (isActive || isSpawning)
            {
                spriteBatch.Draw(sprite, relativeDestRectangle, Color.White);
            }
        }

        public override void Collect()
        {
            isActive = false;
            toDelete = true;
            destRectangle = new Rectangle(1800, 1800, magnifier * width, magnifier * height);
        }

        public override void Spawn()
        {
            isSpawning = true;
            soundInstance.Play();
        }
    }
}
