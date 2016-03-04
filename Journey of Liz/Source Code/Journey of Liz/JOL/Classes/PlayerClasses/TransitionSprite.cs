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
    class TransitionSprite : IPlayerSprite
    {
        public Texture2D sprite { get; set; }
        public Vector2 spritePosition { get; set; }
        public Rectangle destRectangle { get; set; }
        public Rectangle botRectangle { get; set; }
        public bool isFacingRight { get; set; }
        public bool isMoving { get; set; }
        public bool isJumping { get; set; }
        public float fallSpeed { get; set; }
        public float GRAVITY { get; set; }
        public float velocity { get; set; }
        public float starTimer { get; set; }
        public ContentManager contentManager { get; set; }
        public SoundEffectInstance soundInstance { get; set; }
        public Color tint { get; set; }

        private static int transitionFor=70, transitionDelay=10;
        int transitionTimer=0;
        bool spriteToDisplay = false; //false is previous sprite, true is next sprite
        public IPlayerSprite nextMarioSprite;
        public IPlayerSprite previousMarioSprite;
        public bool doneTransitioning = false;
        SoundEffect sound;


        public TransitionSprite(IPlayerSprite previousSprite, IPlayerSprite nextSprite, int power)
        {
            destRectangle = previousSprite.destRectangle;
            fallSpeed = previousSprite.fallSpeed;
            this.previousMarioSprite = previousSprite;
            this.nextMarioSprite = nextSprite;
            nextSprite.fallSpeed = previousSprite.fallSpeed;
            contentManager = previousSprite.contentManager;
            if (power > 0)
                sound = contentManager.Load<SoundEffect>("Sounds/powerup");
            else
                sound = contentManager.Load<SoundEffect>("Sounds/powerdown");
            soundInstance = sound.CreateInstance();
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
