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
    class PlayerSprite : IPlayerSprite
    {
        public Texture2D sprite { get; set; }
        public Vector2 spritePosition { get; set; }
        public Rectangle destRectangle { get; set; }
        public Rectangle botRectangle { get; set; }
        public ContentManager contentManager { get; set; }
        public SoundEffectInstance soundInstance { get; set; }
        public Color tint { get; set; }

        public int spriteWidth = 32;
        public int spriteHeight = 32;
        public int status = 0;
        public const float GRAVITY = 0.2f;
        public float velocity { get; set; }
        public float fallSpeed { get; set; }
        public float starTimer { get; set; }
        public bool isJumping { get; set; }
        public bool isFacingRight { get; set; }
        public bool isMoving { get; set; }

        public PlayerSprite(IPlayerSprite previousSprite)
        {
            contentManager = previousSprite.contentManager;
            starTimer = previousSprite.starTimer;
            sprite = contentManager.Load<Texture2D>("Liz/liz_idle");
            isMoving = false;
            isJumping = false;
            isFacingRight = previousSprite.isFacingRight;
            fallSpeed = previousSprite.fallSpeed;
            tint = Color.White;
        }

        public PlayerSprite(ContentManager content)
        {
            contentManager = content;
            sprite = contentManager.Load<Texture2D>("Liz/liz_idle");
            isMoving = false;
            isJumping = false;
            isFacingRight = true;
            fallSpeed = 0f;
            tint = Color.White;
        }

        public void Initialize()
        {
            spritePosition = new Vector2(385, 225);
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
            botRectangle = new Rectangle((int)spritePosition.X, ((int)spritePosition.Y + 32), spriteWidth, spriteHeight);
        }

        public void Initialize(IPlayerSprite previousSprite)
        {
            spritePosition = new Vector2(previousSprite.spritePosition.X, previousSprite.spritePosition.Y - (spriteHeight - previousSprite.destRectangle.Height));
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
            botRectangle = new Rectangle((int)spritePosition.X, ((int)spritePosition.Y + 32), spriteWidth, spriteHeight);
        }

        public virtual void Update(GameTime gameTime)
        {
            Vector2 tempY = spritePosition;
            tempY.Y += fallSpeed;
            spritePosition = tempY;
            if (fallSpeed < 10f)
                fallSpeed += GRAVITY;

            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
        }

        public virtual void Draw(SpriteBatch spriteBatch, ICamera camera)
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
            botRectangle = new Rectangle((int)spritePosition.X, ((int)spritePosition.Y + 32), spriteWidth, spriteHeight);
        }
    }
}
