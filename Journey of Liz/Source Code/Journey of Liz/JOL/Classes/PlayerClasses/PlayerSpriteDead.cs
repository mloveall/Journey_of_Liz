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
    class PlayerSpriteDead : PlayerSprite
    {
        SoundEffect sound;

        public PlayerSpriteDead(IPlayerSprite previousSprite) : base(previousSprite)
        {
            spriteWidth = 32;
            spriteHeight = 32;
            sprite = contentManager.Load<Texture2D>("Liz/liz_dead");
            fallSpeed = -5f;
            sound = contentManager.Load<SoundEffect>("Sounds/lizdie");
            soundInstance = sound.CreateInstance();

            Initialize(previousSprite);
        }

        public PlayerSpriteDead(ContentManager content) : base(content)
        {
            spriteWidth = 32;
            spriteHeight = 32;
            sprite = contentManager.Load<Texture2D>("Liz/liz_dead");
            isFacingRight = true;
            spritePosition = new Vector2(390, 300);

            Initialize();
        }
    }
}
