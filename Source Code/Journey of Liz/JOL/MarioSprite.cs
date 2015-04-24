using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CSE3902Project
{
    class MarioSprite
    {
        //SpriteBatch spriteBatch;
        public Texture2D sprite;
        Vector2 spritePosition = Vector2.Zero;

        float timer = 0f;
        int currentFrame = 0;
        int spriteWidth = 32;
        int spriteHeight = 32;
        public int status = 0;

        public bool facingRight {get; set;}

        public MarioSprite()
        {
            facingRight = true;
        }

        public void LoadContent(ContentManager getContent, string name)
        {
            sprite = getContent.Load<Texture2D>(name);
            //spriteBatch = SB;
        }

        public void Update(GameTime gameTime)
        {
            if (timer > 1.0f)
            {
                if (currentFrame != 3)
                    currentFrame++;
                else
                    currentFrame = 0;
                timer = 0.0f;
            }
            else
            {
                timer += 0.2f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            if (status == 0)
            {
                spriteBatch.Draw(sprite, spritePosition, Color.White);
            }
            else if (status == 1)
            {
                if (facingRight)
                {
                    spriteBatch.Draw(sprite, spritePosition, new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight), Color.White);
                }
                else
                {
                    spriteBatch.Draw(sprite, spritePosition, new Rectangle(), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
            }
        }
        
    }
}
