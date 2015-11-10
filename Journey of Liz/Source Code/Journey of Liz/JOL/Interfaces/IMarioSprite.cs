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

namespace JOL.Interfaces
{
    /// <summary>
    /// An abstract base class for various Mario sprites.
    /// </summary>

    public interface IMarioSprite
    {
        Texture2D Sprite { get; set; }
        Vector2 SpritePosition { get; set; }
        Rectangle DestRectangle { get; set; }
        Rectangle BotRectangle { get; set; }
        bool FacingRight { get; set; }
        bool IsMoving { get; set; }
        bool IsJumping { get; set; }
        float FallSpeed { get; set; }
        float StarTimer { get; set; }
        ContentManager ContentManager { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, ICamera camera);
        void MoveTo(int xPosition, int yPosition);
        float Velocity { get; set; }
        float Gravity { get; set; }
        Color Tint { get; set; }
        SoundEffectInstance SoundInstance { get; set; }
    }
}
