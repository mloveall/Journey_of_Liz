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
    public class KabamEnemy : Enemy
    {
        private static int NORMAL_SPEED = 2, NUMBER_OF_FRAMES = 2, FRAME_WIDTH = 20, FRAME_DELAY = 15, DEAD_LENGTH = 10;

        private int currentFrame = 0, frameDelayClock = 0, deadTimer = 0;
        private SoundEffect sound;
        private SoundEffectInstance soundInstance;

        public KabamEnemy(Texture2D sprite, SoundEffect sound, int xPos, int yPos) : base(sprite, xPos, yPos)
        {
            height = 16;
            width = 16;
            this.sound = sound;
            soundInstance = sound.CreateInstance();

            Initialize();
        } 

        public override void Update(GameTime gameTime)
        {
            if (isAlive)
            {
                if (!isDying)
                {
                    frameDelayClock++;
                    if (frameDelayClock > FRAME_DELAY)
                    {
                        frameDelayClock = 0;
                        currentFrame++;
                        currentFrame = currentFrame % NUMBER_OF_FRAMES;
                    }

                    if (isFacingRight)
                    {
                        xPosDest += NORMAL_SPEED;
                    }
                    else
                    {
                        xPosDest -= NORMAL_SPEED;
                    }

                    yPosDest += (int)fallSpeed;
                    if (fallSpeed.CompareTo(10.0f) < 0)
                    {
                        fallSpeed = fallSpeed * 1.05f;
                    }
                }
                else
                {
                    deadTimer++;
                    if (deadTimer >= DEAD_LENGTH)
                    {
                        toDraw = false;
                        toDelete = true;
                    }
                }
                destRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(destRectangle.X - camera.Position.X), (int)(destRectangle.Y - camera.Position.Y), magnifier * width, magnifier * height);
            if (toDraw)
            {
                    Rectangle sourceRectangle = new Rectangle(xPosSource + currentFrame * FRAME_WIDTH, yPosSource, width, height);
                    if (isFacingRight)
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                    }
            }
        }

        public override bool Hit(CollisionDetection.CollisionType collisionType, bool hitRight)
        {
            bool isHit = false;
            if (!isDying)
            {
                switch (collisionType)
                {
                    case CollisionDetection.CollisionType.TopCollision:
                        isDying = true;
                        soundInstance.Play();
                        break;

                    case CollisionDetection.CollisionType.BottomCollision:
                    case CollisionDetection.CollisionType.RightCollision:
                    case CollisionDetection.CollisionType.LeftCollision:
                        isHit = true;
                        break;
                }
            }
            return isHit;
        }
    }
}
