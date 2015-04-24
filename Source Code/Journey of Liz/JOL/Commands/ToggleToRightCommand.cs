using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class ToggleToRightCommand : ICommand
    {
        MultiMarioHolder holder;

        public ToggleToRightCommand(MultiMarioHolder holder)
        {
            this.holder = holder;
        }

        public void Execute()
        {
            holder.getCurrentMario().Right();

        }

    }
}
