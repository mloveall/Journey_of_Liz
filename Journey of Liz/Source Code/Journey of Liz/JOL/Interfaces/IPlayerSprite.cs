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

    public interface IPlayerSprite
    {
        Texture2D sprite { get; set; }
        Vector2 spritePosition { get; set; }
        Rectangle destRectangle { get; set; }
        Rectangle botRectangle { get; set; }
        bool isFacingRight { get; set; }
        bool isMoving { get; set; }
        bool isJumping { get; set; }
        float fallSpeed { get; set; }
        float starTimer { get; set; }
        ContentManager contentManager { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, ICamera camera);
        void MoveTo(int xPosition, int yPosition);
        float velocity { get; set; }
        Color tint { get; set; }
        SoundEffectInstance soundInstance { get; set; }
    }
}
