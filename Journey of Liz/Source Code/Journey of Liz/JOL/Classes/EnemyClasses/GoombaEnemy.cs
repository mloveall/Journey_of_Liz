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
    public class GoombaEnemy : IEnemy
    {
        public bool toDelete { get; set; }
        Texture2D sprite;
        SoundEffect sound;
        SoundEffectInstance soundInstance;
        public Rectangle DestRectangle { get; set; }
        public bool IsAlive { get; set;}
        bool toDraw = true, isDying = false;
        bool facingRight = false;
        int xPosSource = 2, yPosSource = 2, xPosDest, yPosDest;
        int magnifier = 2;

        private static int HEIGHT=16, WIDTH=16;
        
        public float FallSpeed { get; set; }

        int currentFrame = 0, frameDelayClock = 0,  deadTimer=0;
        private static int GOOMBA_SPEED = 2, NUMBER_OF_FRAMES = 2, FRAME_WIDTH = 20, FRAME_DELAY = 15, DEAD_LENGTH = 10;

        public GoombaEnemy(Texture2D sprite, SoundEffect sound, int xPos, int yPos)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            IsAlive = false;
            toDelete = false;
            this.sound = sound;
            soundInstance = sound.CreateInstance();
        } 

        public void Update(GameTime gameTime)
        {
            if (IsAlive)
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

                    if (facingRight)
                    {
                        xPosDest += GOOMBA_SPEED;
                    }
                    else
                    {
                        xPosDest -= GOOMBA_SPEED;
                    }
                    yPosDest += (int)FallSpeed;
                    if (FallSpeed.CompareTo(10.0f) < 0)
                    {
                        FallSpeed = FallSpeed * 1.05f;
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

                DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), magnifier * WIDTH, magnifier * HEIGHT);
            if (toDraw)
            {
                    Rectangle sourceRectangle = new Rectangle(xPosSource + currentFrame * FRAME_WIDTH, yPosSource, WIDTH, HEIGHT);
                    if (facingRight)
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                    }
            }
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

        public bool Hit(CollisionDetection.CollisionType collisionType, bool hitRight)
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
