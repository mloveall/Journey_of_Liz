using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace CSE3902Project
{
    class KeyboardController : IController
    {
            
        Dictionary<KeyboardState, ICommand> ButtonMapping;

        public KeyboardController(ICommand toggleCrouchIdleJumpCommand, ICommand toggleJumpIdleCrouchCommand, ICommand toggleToLeftCommand, ICommand toggleToRightCommand,
           ICommand bigStateCommand, ICommand fireStateCommand, ICommand deadStateCommand, ICommand smallStateCommand, ICommand questionBlockCommand, ICommand usedBlockCommand,
            ICommand brickBlockCommand, ICommand floorBlockCommand, ICommand stairPyramidBlockCommand, ICommand hiddenBlockCommand)
        {   
            ButtonMapping = new Dictionary<KeyboardState, ICommand>();

            ButtonMapping.Add(new KeyboardState(Keys.Up), toggleCrouchIdleJumpCommand);
            ButtonMapping.Add(new KeyboardState(Keys.W), toggleCrouchIdleJumpCommand);

            ButtonMapping.Add(new KeyboardState(Keys.Down), toggleJumpIdleCrouchCommand);
            ButtonMapping.Add(new KeyboardState(Keys.S), toggleJumpIdleCrouchCommand);

            ButtonMapping.Add(new KeyboardState(Keys.Left), toggleToLeftCommand);
            ButtonMapping.Add(new KeyboardState(Keys.A), toggleToLeftCommand);

            ButtonMapping.Add(new KeyboardState(Keys.Right), toggleToRightCommand);
            ButtonMapping.Add(new KeyboardState(Keys.D), toggleToRightCommand);
          
            ButtonMapping.Add(new KeyboardState(Keys.U), bigStateCommand);
            
            ButtonMapping.Add(new KeyboardState(Keys.I), fireStateCommand);
           
            ButtonMapping.Add(new KeyboardState(Keys.O), deadStateCommand);

            ButtonMapping.Add(new KeyboardState(Keys.Y), smallStateCommand);
           
            ButtonMapping.Add(new KeyboardState(Keys.Z), questionBlockCommand);

            ButtonMapping.Add(new KeyboardState(Keys.X), usedBlockCommand);

            ButtonMapping.Add(new KeyboardState(Keys.C), brickBlockCommand);

            ButtonMapping.Add(new KeyboardState(Keys.V), floorBlockCommand);

            ButtonMapping.Add(new KeyboardState(Keys.B), stairPyramidBlockCommand);

            ButtonMapping.Add(new KeyboardState(Keys.N), hiddenBlockCommand);

        }

        

        public void Update()
        {
            
            KeyboardState currentKeysState = Keyboard.GetState();
            ICommand command;
            Boolean validCommand = ButtonMapping.TryGetValue(currentKeysState, out command);

            if (validCommand  == true)
            {
                command.Execute();
            }

            
        }
    }
}