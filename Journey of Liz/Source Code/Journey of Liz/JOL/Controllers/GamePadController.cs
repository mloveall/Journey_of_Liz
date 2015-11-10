using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace JOL
{
    /// <summary>
    /// This class handles user inputs from an XBox Game Pad by calling the corresponding command for each input.
    /// </summary>

    public class GamePadController : IController
    {
        Dictionary<GamePadButtons, ICommand> ButtonMapping;

        public GamePadController(ICommand aButtonCommand, ICommand bButtonCommand, ICommand xButtonCommand, ICommand startButtonCommand)
        {
            //maps buttons to commands
            ButtonMapping = new Dictionary<GamePadButtons, ICommand>();
            ButtonMapping.Add(new GamePadButtons(Buttons.A), aButtonCommand);
            ButtonMapping.Add(new GamePadButtons(Buttons.B), bButtonCommand);
            ButtonMapping.Add(new GamePadButtons(Buttons.X), xButtonCommand);
            ButtonMapping.Add(new GamePadButtons(Buttons.Start), startButtonCommand);
        }

        // Update is called every frame
        public void Update(GameTime gameTime)
        {
            GamePadButtons currentButtonState = GamePad.GetState(PlayerIndex.Two).Buttons;
            ICommand command;
            Boolean validCommand = ButtonMapping.TryGetValue(currentButtonState, out command);

            if (validCommand  == true)
            {
                command.Execute();
            }
        }
    }
}
