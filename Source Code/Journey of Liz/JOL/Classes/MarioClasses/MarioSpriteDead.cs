using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Classes.MarioClasses
{
    class MarioSpriteDead : IMarioSprite
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
        public float Gravity { get; set; }
        public float StarTimer { get; set; }
        public ContentManager ContentManager { get; set; }
        public SoundEffectInstance SoundInstance { get; set; }

        int spriteWidth = 32;
        public int spriteHeight = 32;
        public int status = 0;
        public Color Tint { get; set; }
        SoundEffect sound;

        public MarioSpriteDead(IMarioSprite previousSprite)
        {
            FacingRight = previousSprite.FacingRight;
            SpritePosition = new Vector2(previousSprite.SpritePosition.X, previousSprite.SpritePosition.Y - (spriteHeight - previousSprite.DestRectangle.Height)); FacingRight = previousSprite.FacingRight;
            ContentManager = previousSprite.ContentManager;
            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, spriteWidth, spriteHeight);
            Sprite = ContentManager.Load<Texture2D>("Marios/small_mario_dead");
            StarTimer = previousSprite.StarTimer;
            Tint = Color.White;
            FallSpeed = -5f;
            Gravity = 0.2f;
            sound = ContentManager.Load<SoundEffect>("Sounds/mariodie");
            SoundInstance = sound.CreateInstance();
        }

        public MarioSpriteDead(ContentManager content)
        {
            ContentManager = content;
            Sprite = ContentManager.Load<Texture2D>("Marios/small_mario_dead");
            FacingRight = true;
            SpritePosition = new Vector2(390, 300);
            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, spriteWidth, spriteHeight);
        }


        public void Update(GameTime gameTime)
        {
            Vector2 tempPos = SpritePosition;
            tempPos.Y += FallSpeed;
            SpritePosition = tempPos;
            if (FallSpeed < 10f)
                FallSpeed += Gravity;

            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, spriteWidth, spriteHeight);

        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle destRectangle = new Rectangle((int)(DestRectangle.X - camera.Position.X), (int)(DestRectangle.Y - camera.Position.Y), spriteWidth, spriteHeight);
            if (FacingRight)
            {
                spriteBatch.Draw(Sprite, destRectangle, Tint);
            }
            else
            {
                spriteBatch.Draw(Sprite, destRectangle, null, Tint, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
            }

        }

        public void MoveTo(int xPosition, int yPosition)
        {
            SpritePosition = new Vector2(xPosition, yPosition);
            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, spriteWidth, spriteHeight);
        }
    }
}
