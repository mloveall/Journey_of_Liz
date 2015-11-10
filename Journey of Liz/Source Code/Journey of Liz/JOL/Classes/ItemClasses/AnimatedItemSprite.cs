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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AnimatedItemSprite : IAnimatedSprite
    {   
        static int FrameDelay = 15;
        int xPosDest = 300, yPosDest = 100, height = 35, width = 30;
        int xPosSource = 85, yPosSource = 50, magnifier = 2;
        int currentFrame = 0, frameDelayClock, numberOfFrames = 4, frameWidth; 

        Texture2D spriteTextures;

        public AnimatedItemSprite(Texture2D texture)
        {
            spriteTextures = texture;
        }

        public void Update()
        {
            //handles currentFrame calculations
            frameDelayClock++;
            if (frameDelayClock >= FrameDelay)
            {
                frameDelayClock = 0;
                currentFrame++;
                currentFrame = currentFrame % numberOfFrames;

            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            frameWidth = texture.Width / numberOfFrames;

            Rectangle destinationRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
            Rectangle sourceRectangle = new Rectangle(xPosSource + currentFrame * frameWidth, yPosSource, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}


