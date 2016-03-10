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
    class TransitionSprite : PlayerSprite
    {
        private int transitionFor = 70, transitionDelay = 10;
        private int transitionTimer=0;
        private bool spriteToDisplay = false; //false is previous sprite, true is next sprite
        public IPlayerSprite nextPlayerSprite { get; private set; }
        public IPlayerSprite prevPlayerSprite { get; private set; }
        public bool doneTransitioning { get; private set; }
        SoundEffect sound;

        public TransitionSprite(IPlayerSprite previousSprite, IPlayerSprite nextSprite, int power) : base(previousSprite)
        {
            destRectangle = previousSprite.destRectangle;
            this.prevPlayerSprite = previousSprite;
            this.nextPlayerSprite = nextSprite;
            nextSprite.fallSpeed = previousSprite.fallSpeed;
            if (power > 0)
                sound = contentManager.Load<SoundEffect>("Sounds/powerup");
            else
                sound = contentManager.Load<SoundEffect>("Sounds/powerdown");
            soundInstance = sound.CreateInstance();
            doneTransitioning = false;

            Initialize(previousSprite);
        }

        public override void Update(GameTime gameTime)
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

        public override void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            if(spriteToDisplay)
            {
                nextPlayerSprite.Draw(spriteBatch, camera);
            }
            else
            {
                prevPlayerSprite.Draw(spriteBatch, camera);
            }
        }
    }
    
}
