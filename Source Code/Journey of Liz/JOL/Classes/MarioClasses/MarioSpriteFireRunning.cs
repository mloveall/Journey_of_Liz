using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using JOL.Interfaces;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Classes.MarioClasses
{
    class MarioSpriteFireRunning : IMarioSprite
    {
        public Texture2D Sprite { get; set; }
        public Vector2 SpritePosition { get; set; }
        public Rectangle DestRectangle { get; set; }
        public Rectangle BotRectangle { get; set; }
        public bool FacingRight { get; set; }
        public bool IsMoving { get; set; }
        public float Velocity { get; set; }
        public bool IsJumping { get; set; }
        public float FallSpeed { get; set; }
        public float StarTimer { get; set; }
        public ContentManager ContentManager { get; set; }
        public SoundEffectInstance SoundInstance { get; set; }

        float timer = 0f;
        int currentFrame = 0;
        public float Gravity { get; set; }
        private float friction = 0.4f;
        public int numberOfFrames = 3;
        int spriteWidth = 32;
        public int spriteHeight = 64;
        public int status = 0;
        public Color Tint { get; set; }


        public MarioSpriteFireRunning(IMarioSprite previousSprite)
        {
            FacingRight = previousSprite.FacingRight;
            SpritePosition = new Vector2(previousSprite.SpritePosition.X, previousSprite.SpritePosition.Y-(spriteHeight-previousSprite.DestRectangle.Height));
            ContentManager = previousSprite.ContentManager;
            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y , spriteWidth, spriteHeight);
            Sprite = ContentManager.Load<Texture2D>("Marios/fire_mario_moving");
            StarTimer = previousSprite.StarTimer;
            FallSpeed = previousSprite.FallSpeed;
            IsMoving = true;
            IsJumping = false;
            Gravity = 0.2f;
            Velocity = 3.0f;
            Tint = Color.White;
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

            Vector2 tempY = SpritePosition;
            tempY.Y += FallSpeed;
            SpritePosition = tempY;
            if (FallSpeed < 10f)
                FallSpeed += Gravity;

            if (FacingRight == true && IsMoving)
            {
                Vector2 tempPos = SpritePosition;
                tempPos.X += Velocity;
                SpritePosition = tempPos;
            }
            else if (IsMoving)
            {
                Vector2 tempPos = SpritePosition;
                tempPos.X -= Velocity;
                SpritePosition = tempPos;
            }

            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, spriteWidth, spriteHeight);

            if (Velocity > 0)
            {
                Velocity = Velocity - friction;
            }
            else
            {
                Velocity = 0.0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle destRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), spriteWidth, spriteHeight);
            if (FacingRight)
            {
                spriteBatch.Draw(Sprite, destRectangle, new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight), Tint);
            }
            else
            {
                spriteBatch.Draw(Sprite, destRectangle, new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight), Tint, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
            }
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            SpritePosition = new Vector2(xPosition, yPosition);
            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, spriteWidth, spriteHeight);
        }

    }
}
