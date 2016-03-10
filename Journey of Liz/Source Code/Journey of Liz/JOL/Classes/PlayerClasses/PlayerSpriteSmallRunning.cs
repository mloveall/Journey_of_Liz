using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using JOL.Interfaces;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Classes.PlayerClasses
{
    class PlayerSpriteSmallRunning : PlayerSprite
    {
        private float timer = 0f;
        private float friction = 0.4f;
        private int currentFrame = 0;
        private int numberOfFrames = 3;

        public PlayerSpriteSmallRunning(IPlayerSprite previousSprite) : base(previousSprite)
        {
            spriteWidth = 24;
            spriteHeight = 32;
            sprite = contentManager.Load<Texture2D>("Liz/liz_moving");
            isMoving = true;
            isJumping = false;
            velocity = 3.0f;

            Initialize(previousSprite);
        }

        public override void Update(GameTime gameTime)
        {
            if (timer > 1.0f)
            {
                if (currentFrame < numberOfFrames)
                    currentFrame++;
                else
                    currentFrame = 0;
                timer = 0.0f;
            }
            else
            {
                timer += 0.2f;
            }

            Vector2 tempY = spritePosition;
            tempY.Y += fallSpeed;
            spritePosition = tempY;
            if (fallSpeed < 10f)
                fallSpeed += GRAVITY;


            if (isFacingRight == true && isMoving)
            {
                Vector2 tempPos = spritePosition;
                tempPos.X += velocity;
                spritePosition = tempPos;
            }
            else if (isMoving)
            {
                Vector2 tempPos = spritePosition;
                tempPos.X -= velocity;
                spritePosition = tempPos;
            }

            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);

            if (velocity > 0.0f)
            {
                velocity = velocity - friction;
            }
            else
            {
                velocity = 0.0f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(destRectangle.X - camera.Position.X), (int)(destRectangle.Y - camera.Position.Y), spriteWidth, spriteHeight);
            if (isFacingRight)
            {
                spriteBatch.Draw(sprite, relativeDestRectangle, new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight), tint);
            }
            else
            {
                spriteBatch.Draw(sprite, relativeDestRectangle, new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight), tint, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
            }          
        }
    }
}
