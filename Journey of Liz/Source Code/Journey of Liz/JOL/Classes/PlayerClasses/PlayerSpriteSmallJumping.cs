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
    class PlayerSpriteSmallJumping : PlayerSprite
    {
        SoundEffect sound;

        public PlayerSpriteSmallJumping(IPlayerSprite previousSprite) : base(previousSprite)
        {
            spriteWidth = 24;
            spriteHeight = 32;
            sprite = contentManager.Load<Texture2D>("Liz/liz_jump");
            isMoving = previousSprite.isMoving;
            isJumping = true;
            fallSpeed = -7.4f;
            sound = contentManager.Load<SoundEffect>("Sounds/small_jump");
            soundInstance = sound.CreateInstance();
        }

        public PlayerSpriteSmallJumping(ContentManager content) : base(content)
        {
            contentManager = content;
            spriteWidth = 24;
            spriteHeight = 32;
            sprite = contentManager.Load<Texture2D>("Liz/liz_jump");
            spritePosition = new Vector2(390,300);
            botRectangle = new Rectangle((int)spritePosition.X, ((int)spritePosition.Y + 32), spriteWidth, spriteHeight);
        }


        public override void Update(GameTime gameTime)
        {
            if (isJumping)
            {
                Vector2 tempPos = spritePosition;
                tempPos.Y += fallSpeed;
                spritePosition = tempPos;
                if (fallSpeed < 10f)
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

        public void MoveTo(int xPosition, int yPosition)
        {
            spritePosition = new Vector2(xPosition, yPosition);
            destRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteWidth, spriteHeight);
            botRectangle = new Rectangle((int)spritePosition.X, ((int)spritePosition.Y + 32), spriteWidth, spriteHeight);
        }
    }
}
