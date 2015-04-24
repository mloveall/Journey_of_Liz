using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    public class CharacterSwitchCommand : ICommand
    {
        Mario mario, luigi;
        public CharacterSwitchCommand(Mario mario, Mario luigi)
        {
            this.mario = mario;
            this.luigi = luigi;
        }

        public void Execute()
        {
            mario.isPaused = !mario.isPaused;
            luigi.isPaused = !luigi.isPaused;
        }
    }
}
