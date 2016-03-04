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
using JOL.PlayerStates;
using JOL.Classes.PlayerClasses;
using JOL.Classes.ItemClasses;

namespace JOL
{
    /// <summary>
    /// This class handles the general management of the Mario character.
    /// </summary>

    public class Player
    {
        // Global variables
        public IPlayerState State {get; set;}
        public IPlayerSprite PlayerSprite {get; set;}
        public ContentManager myContent;
        public Level level;
        public Song song;
        public bool isInvulnerable;
        public bool isPaused;
        public int MyState { get; set; }
        
        private int coinScore = 100, OneUpScore = 1000;
        private int coinsToOneUp = 10;
        private int updateCounter = 0;
        private byte alpha = 0;
        private float starTimer;

        // Basic Constructor
        public Player(Texture2D sprite, ContentManager content)
        {
            PlayerSprite = new PlayerSpriteSmallIdle(content);
            this.State = new PlayerStateSmallIdle(this);
            MyState = 1;
            myContent = content;
            isPaused = false;
        }

        // Constructor with parameters for location and game state
        public Player(Texture2D sprite, ContentManager content, Vector2 location, bool isPaused)
        {
            PlayerSprite = new PlayerSpriteSmallIdle(content);
            this.State = new PlayerStateSmallIdle(this);
            MyState = 1;
            PlayerSprite.MoveTo((int)location.X, (int)location.Y);
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
                    PlayerSprite = new PlayerSpriteDead(PlayerSprite);
                    level.lives--;
                    level.dyingAnimation = true;
                    MediaManager(2);
                    PlayerSprite.soundInstance.Play();
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
                    if (updateCounter == 0)
                    {
                        Random r = new Random();
                        alpha = (byte)r.Next(50, 150);
                        PlayerSprite.tint = new Color(180, 180, 180, alpha);
                        updateCounter = 5;
                    }
                    PlayerSprite.tint = new Color(200, 200, 200, alpha);
                    updateCounter--;
                }
                else
                {
                    PlayerSprite.tint = Color.White;
                    isInvulnerable = false;
                    updateCounter = 0;
                }
                State.Update(gameTime);
            }
        }

        // Draw the sprites
        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            PlayerSprite.Draw(spriteBatch, camera);
        }

        // Teleport Mario to target location
        public void MoveTo(int xPosition, int yPosition)
        {
            if (!isPaused)
            {
                PlayerSprite.MoveTo(xPosition, yPosition);
            }
        }

    }
}
