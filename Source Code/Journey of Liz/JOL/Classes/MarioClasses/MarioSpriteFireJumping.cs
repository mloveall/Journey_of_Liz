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
    class MarioSpriteFireJumping : IMarioSprite
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

        int spriteWidth = 32;
        public int spriteHeight = 64;
        public int status = 0;
        public float Gravity { get; set; }
        public Color Tint { get; set; }
        SoundEffect sound;


        public MarioSpriteFireJumping(IMarioSprite previousSprite)
        {
            FacingRight = previousSprite.FacingRight;
            SpritePosition = new Vector2(previousSprite.SpritePosition.X, previousSprite.SpritePosition.Y - (spriteHeight - previousSprite.DestRectangle.Height)); FacingRight = previousSprite.FacingRight;
            ContentManager = previousSprite.ContentManager;
            DestRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, spriteWidth, spriteHeight);
            Sprite = ContentManager.Load<Texture2D>("Marios/fire_mario_jump");
            StarTimer = previousSprite.StarTimer;
            IsMoving = previousSprite.IsMoving;
            IsJumping = true;
            FallSpeed = -7.4f;
            Gravity = 0.2f;
            Tint = Color.White;
            sound = ContentManager.Load<SoundEffect>("Sounds/small_jump");
            SoundInstance = sound.CreateInstance();
            //soundEngineInstance.Play();
        }

        public void Update(GameTime gameTime)
        {
            if (IsJumping)
            {
                Vector2 tempPos = SpritePosition;
                tempPos.Y += FallSpeed;
                SpritePosition = tempPos;
                FallSpeed += Gravity;
            }

            if (FacingRight == true && IsMoving)
            {
                Vector2 tempPos = SpritePosition;
                tempPos.X += 3;
                SpritePosition = tempPos;
            }
            else if (IsMoving)
            {
                Vector2 tempPos = SpritePosition;
                tempPos.X -= 3;
                SpritePosition = tempPos;
            }

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
