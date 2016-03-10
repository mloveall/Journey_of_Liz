
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
    class EvilBardieEnemy : Enemy
    {
        public enum EvilBardieState {
            offScreen,
            normal,
            stoppedShell,
            movingShell,
            transition,
            dead
        };

        EvilBardieState currentState;

        private static int NORMAL_SPEED = 2, SHELL_SPEED = 4;
        private static int NUMBER_OF_FRAMES = 2, FRAME_WIDTH = 20, FRAME_DELAY = 15;
        private static int UNSHELL_TIME = 240, TRANSITION_TIME = 120, SHELL_FRAME = 2, TRANSITION_FRAME = 3;

        private int currentFrame = 0, frameDelayClock = 0, inShellFor = 0;
        private SoundEffect sound;
        private SoundEffectInstance soundInstance;

        public EvilBardieEnemy(Texture2D sprite, int xPos, int yPos) : base(sprite, xPos, yPos)
        {
            height = 24;
            width = 16;
            currentState = EvilBardieState.normal;

            Initialize();
        }

        public EvilBardieEnemy(Texture2D sprite, SoundEffect sound, int xPos, int yPos) : base(sprite, xPos, yPos)
        {
            height = 24;
            width = 16;
            this.sound = sound;
            soundInstance = sound.CreateInstance();
            currentState = EvilBardieState.normal;

            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (isAlive)
            {
                switch (currentState)
                {
                    case EvilBardieState.offScreen:
                        break;

                    case EvilBardieState.normal:
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
                        break;

                    case EvilBardieState.stoppedShell:
                        inShellFor++;
                        if (inShellFor > TRANSITION_TIME)
                        {
                            currentState = EvilBardieState.transition;
                        }
                        break;

                    case EvilBardieState.movingShell:
                        if (isFacingRight)
                        {
                            xPosDest += SHELL_SPEED;
                        }
                        else
                        {
                            xPosDest -= SHELL_SPEED;
                        }
                        yPosDest += (int)fallSpeed;
                        if (fallSpeed.CompareTo(10.0f) < 0)
                        {
                            fallSpeed = fallSpeed * 1.05f;
                        }
                        break;

                    case EvilBardieState.transition:
                        inShellFor++;
                        if (inShellFor > UNSHELL_TIME)
                        {
                            inShellFor = 0;
                            currentState = EvilBardieState.normal;
                        }
                        break;

                    case EvilBardieState.dead:
                        break;

                    default:
                        break;
                }

                destRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(destRectangle.X - camera.Position.X), (int)(destRectangle.Y - camera.Position.Y), magnifier * width, magnifier * height);
            Rectangle sourceRectangle;

            switch (currentState)
            {
                case EvilBardieState.offScreen:
                    break;

                case EvilBardieState.normal:
                    sourceRectangle = new Rectangle(xPosSource + currentFrame * FRAME_WIDTH, yPosSource, width, height);
                    if (isFacingRight)
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
                    }
                    break;

                case EvilBardieState.stoppedShell:
                    sourceRectangle = new Rectangle(xPosSource + SHELL_FRAME * FRAME_WIDTH, yPosSource, width, height);
                    spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
                    break;

                case EvilBardieState.movingShell:
                    sourceRectangle = new Rectangle(xPosSource + SHELL_FRAME * FRAME_WIDTH, yPosSource, width, height);
                    spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
                    break;

                case EvilBardieState.transition:
                    sourceRectangle = new Rectangle(xPosSource + TRANSITION_FRAME * FRAME_WIDTH, yPosSource, width, height);
                    spriteBatch.Draw(sprite, relativeDestRectangle, sourceRectangle, Color.White);
                    break;

                case EvilBardieState.dead:
                    break;

                default:
                    break;
            }
                
        }

        // Returns whether the hit was dangerous for player
        public override bool Hit(CollisionDetection.CollisionType collisionType, bool hitRight) 
        {
            bool isHit = false;
            switch (collisionType)
            {      
                case CollisionDetection.CollisionType.TopCollision:
                    soundInstance.Play();
                    switch (currentState)
                    {
                        case EvilBardieState.normal:
                            currentState = EvilBardieState.stoppedShell;
                            break;

                        case EvilBardieState.stoppedShell:
                            currentState = EvilBardieState.movingShell;
                            isFacingRight = hitRight;
                            inShellFor = 0;
                            break;

                        case EvilBardieState.movingShell:
                            currentState = EvilBardieState.stoppedShell;
                            
                            break;

                        case EvilBardieState.transition:
                            currentState = EvilBardieState.movingShell;
                            isFacingRight = hitRight;
                            inShellFor = 0;
                            break;
                    }
                    break;
                case CollisionDetection.CollisionType.LeftCollision:
                case CollisionDetection.CollisionType.RightCollision:
                    switch (currentState)
                    {
                        case EvilBardieState.normal:
                            isHit = true;
                            break;

                        case EvilBardieState.stoppedShell:
                            currentState = EvilBardieState.movingShell;
                            isFacingRight = hitRight;
                            inShellFor = 0;
                            break;

                        case EvilBardieState.movingShell:
                            isHit = true;
                            break;

                        case EvilBardieState.transition:
                            currentState = EvilBardieState.movingShell;
                            isFacingRight = hitRight;
                            inShellFor = 0;
                            break;
                    }
                    break;
                case CollisionDetection.CollisionType.BottomCollision:
                    switch (currentState)
                    {
                        case EvilBardieState.normal:
                            isHit = true;
                            break;

                        case EvilBardieState.stoppedShell:
                            currentState = EvilBardieState.movingShell;
                            isFacingRight = hitRight;
                            inShellFor = 0;
                            break;

                        case EvilBardieState.movingShell:
                            isHit = true;
                            break;

                        case EvilBardieState.transition:
                            currentState = EvilBardieState.movingShell;
                            isFacingRight = hitRight;
                            inShellFor = 0;
                            break;
                    }
                    break;
            }
            return isHit;
        }
    }
}

