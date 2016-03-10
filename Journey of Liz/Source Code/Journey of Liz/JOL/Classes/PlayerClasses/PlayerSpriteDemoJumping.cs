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
    class PlayerSpriteDemoJumping : PlayerSprite
    {
        SoundEffect sound;

        public PlayerSpriteDemoJumping(IPlayerSprite previousSprite) : base(previousSprite)
        {
            spriteWidth = 32;
            spriteHeight = 64;
            sprite = contentManager.Load<Texture2D>("Marios/liz_demo_jump");
            isMoving = previousSprite.isMoving;
            isJumping = true;
            fallSpeed = -7.4f;
            sound = contentManager.Load<SoundEffect>("Sounds/small_jump");
            soundInstance = sound.CreateInstance();

            Initialize(previousSprite);
        }

        public override void Update(GameTime gameTime)
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
    }
}
