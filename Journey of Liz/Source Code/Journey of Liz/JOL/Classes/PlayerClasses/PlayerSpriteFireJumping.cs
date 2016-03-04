using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Classes.PlayerClasses
{
    class PlayerSpriteFireJumping : IPlayerSprite
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

        int spriteWidth = 32;
        public int spriteHeight = 64;
        public int status = 0;
        public float GRAVITY { get; set; }
        public Color tint { get; set; }
        SoundEffect sound;


        public PlayerSpriteFireJumping(IPlayerSprite previousSprite)
        {
            isFacingRight = previousSprite.isFacingRight;
            spritePosition = new Vector2(previousSprite.spritePosition.X, previousSprite.spritePosition.Y - (spriteHeight - previousSprite.destRectangle.Height)); isFacingRight = previousSprite.isFacingRight;
            contentManager = previousSprite.contentManager;
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
            sprite = contentManager.Load<Texture2D>("Marios/liz_demo_jump");
            starTimer = previousSprite.starTimer;
            isMoving = previousSprite.isMoving;
            isJumping = true;
            fallSpeed = -7.4f;
            GRAVITY = 0.2f;
            tint = Color.White;
            sound = contentManager.Load<SoundEffect>("Sounds/small_jump");
            soundInstance = sound.CreateInstance();
            //soundEngineInstance.Play();
        }

        public void Update(GameTime gameTime)
        {
            if (isJumping)
            {
                Vector2 tempPos = spritePosition;
                tempPos.Y += fallSpeed;
                spritePosition = tempPos;
                fallSpeed += GRAVITY;
            }

            if (isFacingRight == true && isMoving)
            {
                Vector2 tempPos = spritePosition;
                tempPos.X += 3;
                spritePosition = tempPos;
            }
            else if (isMoving)
            {
                Vector2 tempPos = spritePosition;
                tempPos.X -= 3;
                spritePosition = tempPos;
            }

            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle relativeDestRectangle = new Rectangle((int)(destRectangle.X - camera.Position.X), (int)(destRectangle.Y - camera.Position.Y), spriteWidth, spriteHeight);
            if (isFacingRight)
            {
                spriteBatch.Draw(sprite, relativeDestRectangle, tint);
            }
            else
            {
                spriteBatch.Draw(sprite, relativeDestRectangle, null, tint, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
            }

        }

        public void MoveTo(int xPosition, int yPosition)
        {
            spritePosition = new Vector2(xPosition, yPosition);
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
        }
    }
}
