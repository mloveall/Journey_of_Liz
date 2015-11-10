using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class ToggleJumpIdleCrouchCommand : ICommand
    {
        MultiMarioHolder holder;

        public ToggleJumpIdleCrouchCommand(MultiMarioHolder holder)
        {
            this.holder = holder;
        }

        public void Execute()
        {
            holder.getCurrentMario().Down();

        }


    }
}
