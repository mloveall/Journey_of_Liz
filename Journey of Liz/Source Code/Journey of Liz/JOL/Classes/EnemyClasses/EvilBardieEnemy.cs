
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
    class EvilBardieEnemy : IEnemy
    {
        public bool toDelete { get; set; }
        public enum KoopaState { offScreen, normal, stoppedShell, movingShell, transition, dead };

        Texture2D sprite;
        KoopaState currentState;

        SoundEffect sound;
        SoundEffectInstance soundInstance;

        public Rectangle DestRectangle { get; set; }
        public bool IsAlive {get; set;}
        
        bool facingRight = false;

        int xPosSource = 2, yPosSource = 2, xPosDest, yPosDest;
        int magnifier = 2;

        private static int KOOPA_SPEED = 2, SHELL_SPEED = 4;

        public float FallSpeed { get; set; }

        private static int HEIGHT = 24, WIDTH = 16;

        int currentFrame = 0, frameDelayClock = 0, inShellFor = 0;
        private static int NUMBER_OF_FRAMES = 2, FRAME_WIDTH = 20, FRAME_DELAY = 15;
        private static int UNSHELL_TIME = 240, TRANSITION_TIME = 120, SHELL_FRAME=2, TRANSITION_FRAME=3;

        public EvilBardieEnemy(Texture2D sprite, int xPos, int yPos)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            currentState = KoopaState.normal;
            toDelete = false;
        }

        public EvilBardieEnemy(Texture2D sprite, SoundEffect sound, int xPos, int yPos)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            currentState = KoopaState.normal;
            toDelete = false;
            this.sound = sound;
            soundInstance = sound.CreateInstance();
            IsAlive = false;
        }

        public EvilBardieEnemy()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                switch (currentState)
                {
                    case KoopaState.offScreen:
                        break;

                    case KoopaState.normal:
                        frameDelayClock++;
                        if (frameDelayClock > FRAME_DELAY)
                        {
                            frameDelayClock = 0;
                            currentFrame++;
                            currentFrame = currentFrame % NUMBER_OF_FRAMES;
                        }

                        if (facingRight)
                        {
                            xPosDest += KOOPA_SPEED;
                        }
                        else
                        {
                            xPosDest -= KOOPA_SPEED;
                        }

                        yPosDest += (int)FallSpeed;
                        if (FallSpeed.CompareTo(10.0f) < 0)
                        {
                            FallSpeed = FallSpeed * 1.05f;
                        }

                        break;

                    case KoopaState.stoppedShell:
                        inShellFor++;
                        if (inShellFor > TRANSITION_TIME)
                        {
                            currentState = KoopaState.transition;
                        }
                        break;

                    case KoopaState.movingShell:

                        if (facingRight)
                        {
                            xPosDest += SHELL_SPEED;
                        }
                        else
                        {
                            xPosDest -= SHELL_SPEED;
                        }
                        yPosDest += (int)FallSpeed;
                        if (FallSpeed.CompareTo(10.0f) < 0)
                        {
                            FallSpeed = FallSpeed * 1.05f;
                        }

                        break;

                    case KoopaState.transition:
                        inShellFor++;
                        if (inShellFor > UNSHELL_TIME)
                        {
                            inShellFor = 0;
                            currentState = KoopaState.normal;
                        }
                        break;
                    case KoopaState.dead:
                        break;

                    default:
                        break;
                }

                DestRectangle = new Rectangle(xPosDest, yPosDest, magnifier * WIDTH, magnifier * HEIGHT);
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), magnifier * WIDTH, magnifier * HEIGHT);
            Rectangle sourceRectangle;

            switch (currentState)
            {
                case KoopaState.offScreen:
                    break;

                case KoopaState.normal:

                    sourceRectangle = new Rectangle(xPosSource + currentFrame * FRAME_WIDTH, yPosSource, WIDTH, HEIGHT);
                    if (facingRight)
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
                    }

                    break;

                case KoopaState.stoppedShell:

                    sourceRectangle = new Rectangle(xPosSource + SHELL_FRAME * FRAME_WIDTH, yPosSource, WIDTH, HEIGHT);
                    spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);

                    break;

                case KoopaState.movingShell:

                    sourceRectangle = new Rectangle(xPosSource + SHELL_FRAME * FRAME_WIDTH, yPosSource, WIDTH, HEIGHT);
                    spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);

                    break;

                case KoopaState.transition:
                    sourceRectangle = new Rectangle(xPosSource + TRANSITION_FRAME * FRAME_WIDTH, yPosSource, WIDTH, HEIGHT);
                    spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);

                    break;
                case KoopaState.dead:
                    break;

                default:
                    break;
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

        // Returns whether the hit was dangerous for player
        public bool Hit(CollisionDetection.CollisionType collisionType, bool hitRight) 
        {
            bool isHit = false;
            switch (collisionType)
            {      
                case CollisionDetection.CollisionType.TopCollision:
                    soundInstance.Play();
                    switch (currentState)
                    {
                        case KoopaState.normal:
                            currentState = KoopaState.stoppedShell;
                            
                            break;

                        case KoopaState.stoppedShell:
                            currentState = KoopaState.movingShell;
                            facingRight = hitRight;
                            inShellFor = 0;
                            break;

                        case KoopaState.movingShell:
                            currentState = KoopaState.stoppedShell;
                            
                            break;

                        case KoopaState.transition:
                            currentState = KoopaState.movingShell;
                            facingRight = hitRight;
                            inShellFor = 0;
                            break;
                    }
                    break;
                case CollisionDetection.CollisionType.LeftCollision:
                case CollisionDetection.CollisionType.RightCollision:
                    switch (currentState)
                    {
                        case KoopaState.normal:
                            isHit = true;
                            break;

                        case KoopaState.stoppedShell:
                            currentState = KoopaState.movingShell;
                            facingRight = hitRight;
                            inShellFor = 0;
                            break;

                        case KoopaState.movingShell:
                            isHit = true;
                            break;

                        case KoopaState.transition:
                            currentState = KoopaState.movingShell;
                            facingRight = hitRight;
                            inShellFor = 0;
                            break;
                    }
                    break;
                case CollisionDetection.CollisionType.BottomCollision:
                    switch (currentState)
                    {
                        case KoopaState.normal:
                            isHit = true;
                            break;

                        case KoopaState.stoppedShell:
                            currentState = KoopaState.movingShell;
                            facingRight = hitRight;
                            inShellFor = 0;
                            break;

                        case KoopaState.movingShell:
                            isHit = true;
                            break;

                        case KoopaState.transition:
                            currentState = KoopaState.movingShell;
                            facingRight = hitRight;
                            inShellFor = 0;
                            break;
                    }
                    break;
            }
            return isHit;
        }
        


        
    }
}

