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
using JOL.Interfaces;

namespace JOL
{
    /// <summary>
    /// A general enemy class that is being used as the parent of every specific enemy.
    /// </summary>

    public class Enemy : IEnemy
    {
        public Rectangle destRectangle { get; set; }
        public bool isAlive { get; set; }
        public bool toDelete { get; set; }
        public int height { get; protected set; }
        public int width { get; protected set; }
        public float fallSpeed { get; set; }

        protected bool toDraw = true;
        protected bool isDying = false;
        protected bool isFacingRight = false;
        protected int xPosSource = 2, yPosSource = 2, xPosDest, yPosDest;
        protected int magnifier = 2;
        protected Texture2D sprite;

        public Enemy(Texture2D sprite, int xPos, int yPos)
        {
            this.sprite = sprite;
            xPosDest = xPos;
            yPosDest = yPos;
            isAlive = false;
            toDelete = false;
        }

        public void Initialize()
        {
            destRectangle = new Rectangle(xPosDest, yPosDest, magnifier * width, magnifier * height);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, ICamera camera)
        {

        }

        public virtual bool Hit(CollisionDetection.CollisionType collisionType, bool hitRight)
        {
            return false;
        }

        public void Flip()
        {
            isFacingRight = !isFacingRight;
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            xPosDest = xPosition;
            yPosDest = yPosition;
            destRectangle = new Rectangle(xPosition, yPosition, magnifier * width, magnifier * height);
        }
    }
}