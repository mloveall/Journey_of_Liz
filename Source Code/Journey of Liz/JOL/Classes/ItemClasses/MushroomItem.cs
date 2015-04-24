﻿using System;
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
    public class MushroomItem : IItem
    {
        public bool toDelete { get; set; }
        public bool isActive { get; set; }
        bool isSpawning = false;
        Texture2D sprite;
        public Rectangle DestRectangle { get; set; }

        bool facingRight = true;
        public float FallSpeed { get; set; }

        int xPosDest = 300, yPosDest = 100;

        SoundEffect sound;
        SoundEffectInstance soundInstance;
       
        int magnifier = 2, spawnHeight;
        private static int HEIGHT = 16, WIDTH = 16, MUSHROOM_SPEED = 2;

        public MushroomItem()
        {
            
        }

        public MushroomItem(Texture2D sprite, int xPos, int yPos, bool isActive)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            toDelete = false;
            this.isActive = isActive;
        }

        public MushroomItem(Texture2D sprite, int xPos, int yPos, bool isActive, SoundEffect sound)
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
            if (isActive)
            {
                    if (facingRight)
                    {
                        xPosDest += MUSHROOM_SPEED;
                    }
                    else
                    {
                        xPosDest -= MUSHROOM_SPEED;
                    }
                    yPosDest += (int)FallSpeed;
                    if (FallSpeed.CompareTo(10.0f) < 0)
                    {
                        FallSpeed = FallSpeed * 1.05f;
                    }
                
            }
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
        }
        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle destRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), magnifier * WIDTH, magnifier * HEIGHT);
            if (isActive || isSpawning)
            {
                spriteBatch.Draw(sprite, destRectangle, Color.White);
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
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
        }
        public void Spawn()
        {
            isSpawning = true;
            soundInstance.Play();
        }

        public void Flip()
        {
            facingRight = !facingRight;
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            xPosDest = xPosition;
            yPosDest = yPosition;
            DestRectangle = new Rectangle(xPosition, yPosition, magnifier * WIDTH, magnifier * HEIGHT);
        }
    }
}