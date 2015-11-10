using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JOL.Classes.MarioClasses;
using JOL.Classes.ItemClasses;

namespace JOL
{
    /// <summary>
    /// This class handles the general management of the Mario character.
    /// </summary>

    public class Mario
    {
        // Global variables
        public IMarioState State {get; set;}
        public IMarioSprite MarioSprite {get; set;}
        public ContentManager myContent;
        public Level level;
        public Song song;
        public bool isInvulnerable;
        public bool isPaused;
        public int MyState { get; set; }
        
        private int coinScore = 100, OneUpScore = 1000;
        private int coinsToOneUp = 10;
        private float starTimer;

        // Basic Constructor
        public Mario(Texture2D sprite, ContentManager content)
        {
            MarioSprite = new MarioSpriteSmallIdle(content);
            this.State = new SmallIdleMarioState(this);
            MyState = 1;
            myContent = content;
            isPaused = false;
        }

        // Constructor with parameters for location and game state
        public Mario(Texture2D sprite, ContentManager content, Vector2 location, bool isPaused)
        {
            MarioSprite = new MarioSpriteSmallIdle(content);
            this.State = new SmallIdleMarioState(this);
            MyState = 1;
            MarioSprite.MoveTo((int)location.X, (int)location.Y);
            myContent = content;
            song = content.Load<Song>("Music/main_theme");
            MediaPlayer.Play(song);
            this.isPaused = isPaused;
        }

        // Manages the background music
        public void MediaManager(int option)
        {
            if (option == 1)
                MediaPlayer.Play(song);
            else if (option == 2)
                MediaPlayer.Stop();
        }

        // The following functions handle Mario activities
        public void Left()
        {
            if (!isPaused)
                State.Left();
        }

        public void Right()
        {
            if (!isPaused)
                State.Right();
        }

        public void Up()
        {
            if (!isPaused)
                State.Up();
        }

        public void Down()
        {
            if (!isPaused)
                State.Down();
        }

        public void Hit()
        {
            State.Hit();
        }

        public void Collect(IItem item)
        {
            if (!isPaused)
            {
                if (item is CoinItem)
                {
                    level.coins++;
                    level.score += coinScore;
                    if (level.coins == coinsToOneUp)
                    {
                        level.lives++;
                    }
                }
                else if (item is OneUpMushroomItem)
                {
                    level.lives++;
                    level.score += OneUpScore;
                }
                else if (item is StarItem)
                {
                    starTimer = 10;
                    isInvulnerable = true;
                }
                else if (item is DeadMushroomItem)
                {
                    State = new DeadMarioState(this);
                    MarioSprite = new MarioSpriteDead(MarioSprite);
                    level.lives--;
                    level.dyingAnimation = true;
                    MediaManager(2);
                    MarioSprite.SoundInstance.Play();
                }
                else
                {
                    State.Collect(item);
                }
            }
            
        }

        // Update is called every frame
        public void Update(GameTime gameTime)
        {
            if (!isPaused)
            {
                if (starTimer.CompareTo(0) > 0)
                {
                    starTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Random r = new Random();
                    MarioSprite.Tint = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                }
                else
                {
                    MarioSprite.Tint = Color.White;
                    isInvulnerable = false;
                }
                State.Update(gameTime);
            }
        }

        // Draw the sprites
        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            MarioSprite.Draw(spriteBatch, camera);
        }

        // Teleport Mario to target location
        public void MoveTo(int xPosition, int yPosition)
        {
            if (!isPaused)
            {
                MarioSprite.MoveTo(xPosition, yPosition);
            }
        }

    }
}
