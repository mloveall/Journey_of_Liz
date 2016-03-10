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
    /// This class handles the general management of the player.
    /// </summary>

    public class Player
    {
        // Global variables
        public IPlayerState playerState {get; set;}
        public IPlayerSprite playerSprite {get; set;}
        public ContentManager myContent;
        public Level level;
        public Song song;
        public bool isInvulnerable;
        public bool isPaused;
        public int myState { get; set; }
        
        private int coinScore = 100, OneUpScore = 1000;
        private int coinsToOneUp = 10;
        private int updateCounter = 0;
        private byte alpha = 0;
        private float starTimer;

        // Basic Constructor
        public Player(Texture2D sprite, ContentManager content)
        {
            playerSprite = new PlayerSpriteSmallIdle(content);
            this.playerState = new PlayerStateSmallIdle(this);
            myState = 1;
            myContent = content;
            isPaused = false;
        }

        // Constructor with parameters for location and game state
        public Player(Texture2D sprite, ContentManager content, Vector2 location, bool isPaused)
        {
            playerSprite = new PlayerSpriteSmallIdle(content);
            this.playerState = new PlayerStateSmallIdle(this);
            myState = 1;
            playerSprite.MoveTo((int)location.X, (int)location.Y);
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
                playerState.Left();
        }

        public void Right()
        {
            if (!isPaused)
                playerState.Right();
        }

        public void Up()
        {
            if (!isPaused)
                playerState.Up();
        }

        public void Down()
        {
            if (!isPaused)
                playerState.Down();
        }

        public void Hit()
        {
            playerState.Hit();
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
                else if (item is OneUpHeartItem)
                {
                    level.lives++;
                    level.score += OneUpScore;
                }
                else if (item is StealthPotionItem)
                {
                    starTimer = 10;
                    isInvulnerable = true;
                }
                else if (item is DeathPotionItem)
                {
                    playerState = new PlayerStateDead(this);
                    playerSprite = new PlayerSpriteDead(playerSprite);
                    level.lives--;
                    level.dyingAnimation = true;
                    MediaManager(2);
                    playerSprite.soundInstance.Play();
                }
                else
                {
                    playerState.Collect(item);
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
                        playerSprite.tint = new Color(180, 180, 180, alpha);
                        updateCounter = 5;
                    }
                    playerSprite.tint = new Color(200, 200, 200, alpha);
                    updateCounter--;
                }
                else
                {
                    playerSprite.tint = Color.White;
                    isInvulnerable = false;
                    updateCounter = 0;
                }
                playerState.Update(gameTime);
            }
        }

        // Draw the sprites
        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            playerSprite.Draw(spriteBatch, camera);
        }

        // Teleport Mario to target location
        public void MoveTo(int xPosition, int yPosition)
        {
            if (!isPaused)
            {
                playerSprite.MoveTo(xPosition, yPosition);
            }
        }

    }
}
