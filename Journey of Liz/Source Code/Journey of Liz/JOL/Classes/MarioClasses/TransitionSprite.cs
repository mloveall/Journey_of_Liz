using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Mario_States
{
    class TransitionSprite : IMarioSprite
    {
        public Texture2D Sprite { get; set; }
        public Vector2 SpritePosition { get; set; }
        public Rectangle DestRectangle { get; set; }
        public Rectangle BotRectangle { get; set; }
        public bool FacingRight { get; set; }
        public bool IsMoving { get; set; }
        public bool IsJumping { get; set; }
        public float FallSpeed { get; set; }
        public float Gravity { get; set; }
        public float Velocity { get; set; }
        public float StarTimer { get; set; }
        public ContentManager ContentManager { get; set; }
        public SoundEffectInstance SoundInstance { get; set; }
        public Color Tint { get; set; }

        private static int transitionFor=70, transitionDelay=10;
        int transitionTimer=0;
        bool spriteToDisplay = false; //false is previous sprite, true is next sprite
        public IMarioSprite nextMarioSprite;
        public IMarioSprite previousMarioSprite;
        public bool doneTransitioning = false;
        SoundEffect sound;


        public TransitionSprite(IMarioSprite previousSprite, IMarioSprite nextSprite, int power)
        {
            DestRectangle = previousSprite.DestRectangle;
            FallSpeed = previousSprite.FallSpeed;
            this.previousMarioSprite = previousSprite;
            this.nextMarioSprite = nextSprite;
            nextSprite.FallSpeed = previousSprite.FallSpeed;
            ContentManager = previousSprite.ContentManager;
            if (power > 0)
                sound = ContentManager.Load<SoundEffect>("Sounds/powerup");
            else
                sound = ContentManager.Load<SoundEffect>("Sounds/powerdown");
            SoundInstance = sound.CreateInstance();
        }

        public void Update(GameTime gameTime)
        {
            if(transitionTimer < transitionFor)
            {
                transitionTimer++;
                if(transitionTimer % transitionDelay == 0)
                {
                    spriteToDisplay = !spriteToDisplay;
                }
            }
            else
            {
                doneTransitioning = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {

                if(spriteToDisplay)
                {
                    nextMarioSprite.Draw(spriteBatch, camera);
                }
                else
                {
                    previousMarioSprite.Draw(spriteBatch, camera);
                }
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            
        }
    }
    
}
