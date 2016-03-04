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
    class PlayerSpriteFireRunning : IPlayerSprite
    {
        public Texture2D sprite { get; set; }
        public Vector2 spritePosition { get; set; }
        public Rectangle destRectangle { get; set; }
        public Rectangle botRectangle { get; set; }
        public bool isFacingRight { get; set; }
        public bool isMoving { get; set; }
        public float velocity { get; set; }
        public bool isJumping { get; set; }
        public float fallSpeed { get; set; }
        public float starTimer { get; set; }
        public ContentManager contentManager { get; set; }
        public SoundEffectInstance soundInstance { get; set; }

        float timer = 0f;
        int currentFrame = 0;
        public float GRAVITY { get; set; }
        private float friction = 0.4f;
        public int numberOfFrames = 3;
        int spriteWidth = 32;
        public int spriteHeight = 64;
        public int status = 0;
        public Color tint { get; set; }


        public PlayerSpriteFireRunning(IPlayerSprite previousSprite)
        {
            isFacingRight = previousSprite.isFacingRight;
            spritePosition = new Vector2(previousSprite.spritePosition.X, previousSprite.spritePosition.Y-(spriteHeight-previousSprite.destRectangle.Height));
            contentManager = previousSprite.contentManager;
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y , spriteWidth, spriteHeight);
            sprite = contentManager.Load<Texture2D>("Marios/liz_demo_moving");
            starTimer = previousSprite.starTimer;
            fallSpeed = previousSprite.fallSpeed;
            isMoving = true;
            isJumping = false;
            GRAVITY = 0.2f;
            velocity = 3.0f;
            tint = Color.White;
        }


        public void Update(GameTime gameTime)
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

            if (velocity > 0)
            {
                velocity = velocity - friction;
            }
            else
            {
                velocity = 0.0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
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

        public void MoveTo(int xPosition, int yPosition)
        {
            spritePosition = new Vector2(xPosition, yPosition);
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
        }

    }
}
