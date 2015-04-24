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
    public class BlockSprite : IAnimatedSprite
    {
        int xPosDest = 500, yPosDest = 300, height = 35, width = 30, yVel = 5;
        int xPosSource = 85, yPosSource = 50, magnifier = 2;
        int windowHeight = 400;

        Texture2D spriteTextures;

        public BlockSprite(Texture2D texture)
        {
            spriteTextures = texture;
        }



        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {

            spriteBatch.Draw(texture, new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height), new Rectangle(xPosSource, yPosSource, width, height), Color.White);
        }

    }
}
