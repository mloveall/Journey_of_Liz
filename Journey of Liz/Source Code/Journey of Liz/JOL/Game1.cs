using System;
using System.Collections.Generic;
using System.Linq;
using JOL.Classes.BlockClasses;
using JOL.Commands;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JOL
{
    public enum GameState { StartMenu, Paused, Playing, Winning, Losing, Reset, WonGame };

    /// <summary>
    /// This class manages the levels and game states for game1, which is this mario game.
    /// </summary>

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Global variables
        public GameState gameState = GameState.Playing;

        private int levelNumber = 0;
        private static int numberOfLevels = 3;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level currentLevel;
        PauseMenu pauseMenu;
        WinScreen winScreen;
        LevelBuilder levelBuilder;

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice); // Create a new SpriteBatch, which can be used to draw textures.
            levelBuilder = new LevelBuilder(Content);

            currentLevel = levelBuilder.BuildLevel(this, levelNumber,null);
            pauseMenu = new PauseMenu(currentLevel, Content);
            winScreen = new WinScreen(this, currentLevel, Content);                              
        }
       
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        // Update is called every frame
        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.StartMenu:
                    {
                        break;
                    }
                case GameState.Playing:
                    {
                        currentLevel.Update(gameTime);
                        break;
                    }
                case GameState.Paused:
                    {
                        pauseMenu.Update(gameTime);
                        break;
                    }
                case GameState.Winning :
                    {
                        winScreen.Update(gameTime);
                        break;
                    }

                case GameState.Losing:
                    {
                        break;
                    }
                case GameState.Reset:
                    {
                        currentLevel = levelBuilder.BuildLevel(this, levelNumber, currentLevel);
                        gameState = GameState.Playing;
                        break;
                    }
                case GameState.WonGame:
                    {
                        break;
                    }
            }
            base.Update(gameTime);
        }

        // Draw the sprites for current game state
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.StartMenu:
                    {
                        break;
                    }
                case GameState.Playing:
                    {
                        Color sky1 = new Color(49, 184, 206, 255);
                        if (levelNumber == 0)
                            GraphicsDevice.Clear(sky1);
                        else if (levelNumber == 1)
                            GraphicsDevice.Clear(Color.SandyBrown);
                        else if (levelNumber == 2)
                            GraphicsDevice.Clear(new Color(79,47,30));

                        currentLevel.Draw(spriteBatch);
                        base.Draw(gameTime);
                        break;
                    }
                case GameState.Paused:
                    {
                        GraphicsDevice.Clear(Color.Black);
                        pauseMenu.Draw(spriteBatch);
                        break;
                    }
                case GameState.Winning:
                    {
                        GraphicsDevice.Clear(Color.LightSeaGreen);
                        winScreen.Draw(spriteBatch);
                        break;
                    }
                case GameState.Losing:
                    {
                        GraphicsDevice.Clear(Color.PaleVioletRed);
                        base.Draw(gameTime);
                        break;
                    }
                case GameState.WonGame:
                    {
                        GraphicsDevice.Clear(Color.MediumSeaGreen);
                        break;
                    }
            }
            spriteBatch.End();
        }

        // Jump to the next level if the player wins the current one
        internal void Win()
        {
            levelNumber++;
            if (levelNumber < numberOfLevels)
            {
                gameState = GameState.Winning;
                currentLevel = levelBuilder.BuildLevel(this, levelNumber, currentLevel);
            }
            else
            {
                gameState = GameState.WonGame;
            }
        }

        // Return current level number
        public int getLevel()
        {
            return levelNumber;
        }
    }
}
