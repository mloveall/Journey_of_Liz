//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using CSE3902Project.Interfaces;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace CSE3902Project.Classes.BlockClasses
//{
//    class QuestionBlockBackUp : IBlock
//    {
//        bool isAlive = true;
//        static int FrameDelay = 15;
//        int xPosDest = 300, yPosDest = 100, height=35, width=30;
//        int xPosSource = 85, yPosSource = 50, magnifier = 2;
//        int currentFrame = 0, frameDelayClock, numberOfFrames = 3, frameWidth;

//        Texture2D spriteTextures;

//        public QuestionBlockBackUp(Texture2D texture)
//        {
//            spriteTextures = texture;
//        }

//        public void Update(GameTime gameTime);
//        {
//            //handles currentFrame calculations
//            frameDelayClock++;
//            if (frameDelayClock >= FrameDelay)
//            {
//                frameDelayClock = 0;
//                currentFrame++;
//                currentFrame = currentFrame % numberOfFrames;
                
//            }
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            if (isAlive)
//            {
//                frameWidth = spriteTextures.Width / numberOfFrames;

//                Rectangle destinationRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
//                Rectangle sourceRectangle = new Rectangle(xPosSource + currentFrame * frameWidth, yPosSource, width, height);

//                spriteBatch.Draw(spriteTextures, destinationRectangle, sourceRectangle, Color.White);
//            }
//        }

//        public void Bump()
//        {
//            isAlive = !isAlive;
//        }
//    }
//}
