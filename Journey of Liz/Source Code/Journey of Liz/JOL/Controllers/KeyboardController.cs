using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using JOL.Commands;

namespace JOL
{
    /// <summary>
    /// This class handles user inputs from a keyboard by calling the corresponding command for each input.
    /// <summary>

    class KeyboardController : IController
    {
        // Global variables
        Dictionary<KeyboardState, ICommand> ButtonMapping;
        bool check = false;

        // Constructor
        public KeyboardController(ICommand upCommand, ICommand downCommand, ICommand leftCommand, ICommand rightCommand, ICommand characterSwitchCommand, ICommand resetCommand, ICommand quitCommand, ICommand pauseCommand)
        {
            // Initialize the mapping
            ButtonMapping = new Dictionary<KeyboardState, ICommand>();
            
            // Maps "up" commands
            ButtonMapping.Add(new KeyboardState(Keys.Up), upCommand);
            ButtonMapping.Add(new KeyboardState(Keys.W), upCommand);
            ButtonMapping.Add(new KeyboardState(Keys.W, Keys.D), upCommand);
            ButtonMapping.Add(new KeyboardState(Keys.W, Keys.A), upCommand);
            ButtonMapping.Add(new KeyboardState(Keys.Up, Keys.Left), upCommand);
            ButtonMapping.Add(new KeyboardState(Keys.Up, Keys.Right), upCommand);

            // Maps "down" commands
            ButtonMapping.Add(new KeyboardState(Keys.Down), downCommand);
            ButtonMapping.Add(new KeyboardState(Keys.S), downCommand);

            // Maps "left" commands
            ButtonMapping.Add(new KeyboardState(Keys.Left), leftCommand);
            ButtonMapping.Add(new KeyboardState(Keys.A), leftCommand);

            // Maps "right" commands
            ButtonMapping.Add(new KeyboardState(Keys.Right), rightCommand);
            ButtonMapping.Add(new KeyboardState(Keys.D), rightCommand);

            // Maps "switch" commands
            ButtonMapping.Add(new KeyboardState(Keys.Right, Keys.Space), characterSwitchCommand);
            ButtonMapping.Add(new KeyboardState(Keys.Left, Keys.Space), characterSwitchCommand);
            ButtonMapping.Add(new KeyboardState(Keys.Up, Keys.Space), characterSwitchCommand);
            ButtonMapping.Add(new KeyboardState(Keys.D, Keys.Space), characterSwitchCommand);
            ButtonMapping.Add(new KeyboardState(Keys.A, Keys.Space), characterSwitchCommand);
            ButtonMapping.Add(new KeyboardState(Keys.W, Keys.Space), characterSwitchCommand);
            ButtonMapping.Add(new KeyboardState(Keys.Space), characterSwitchCommand);

            // Maps miscellaneous commands
            ButtonMapping.Add(new KeyboardState(Keys.R), resetCommand);
            ButtonMapping.Add(new KeyboardState(Keys.Q), quitCommand);
            ButtonMapping.Add(new KeyboardState(Keys.Escape), quitCommand);
            ButtonMapping.Add(new KeyboardState(Keys.P), pauseCommand);
        }

        // Alternative constructor for "pause" command
        public KeyboardController(ICommand pauseCommand)
        {
            ButtonMapping = new Dictionary<KeyboardState, ICommand>();
            ButtonMapping.Add(new KeyboardState(Keys.P), pauseCommand);
        }

        // Update is called every frame
        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeysState = Keyboard.GetState();
            ICommand command;
            Boolean validCommand = ButtonMapping.TryGetValue(currentKeysState, out command);

            if (validCommand && check)
            {
                if(command is PauseCommand || command is CharacterSwitchCommand)
                {
                    check = false;
                }
                command.Execute();
            }
            else if (!validCommand)
                check = true;
        }
    }
}