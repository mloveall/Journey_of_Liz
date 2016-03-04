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
    class PlayerSpriteDead : IPlayerSprite
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
        public float GRAVITY { get; set; }
        public float starTimer { get; set; }
        public ContentManager contentManager { get; set; }
        public SoundEffectInstance soundInstance { get; set; }

        int spriteWidth = 32;
        public int spriteHeight = 32;
        public int status = 0;
        public Color tint { get; set; }
        SoundEffect sound;

        public PlayerSpriteDead(IPlayerSprite previousSprite)
        {
            isFacingRight = previousSprite.isFacingRight;
            spritePosition = new Vector2(previousSprite.spritePosition.X, previousSprite.spritePosition.Y - (spriteHeight - previousSprite.destRectangle.Height)); isFacingRight = previousSprite.isFacingRight;
            contentManager = previousSprite.contentManager;
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
            sprite = contentManager.Load<Texture2D>("Liz/liz_dead");
            starTimer = previousSprite.starTimer;
            tint = Color.White;
            fallSpeed = -5f;
            GRAVITY = 0.2f;
            sound = contentManager.Load<SoundEffect>("Sounds/lizdie");
            soundInstance = sound.CreateInstance();
        }

        public PlayerSpriteDead(ContentManager content)
        {
            contentManager = content;
            sprite = contentManager.Load<Texture2D>("Liz/liz_dead");
            isFacingRight = true;
            spritePosition = new Vector2(390, 300);
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
        }


        public void Update(GameTime gameTime)
        {
            Vector2 tempPos = spritePosition;
            tempPos.Y += fallSpeed;
            spritePosition = tempPos;
            if (fallSpeed < 10f)
                fallSpeed += GRAVITY;

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
