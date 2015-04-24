using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    public class PauseCommand : ICommand
    {
        Level level;

        public PauseCommand(Level level)
        {
            this.level = level;
        }

        public PauseCommand()
        {
        }

        public void Execute()
        {
            level.Pause();
        }
    }
}
