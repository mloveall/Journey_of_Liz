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
    class PlayerSpriteDemoIdle : PlayerSprite
    {
        public PlayerSpriteDemoIdle(IPlayerSprite previousSprite) : base(previousSprite)
        {
            spriteWidth = 32;
            spriteHeight = 64;
            sprite = contentManager.Load<Texture2D>("Liz/liz_demo_idle");
            isMoving = false;
            isJumping = false;

            Initialize(previousSprite);
        }
    }
}
