using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class ToggleJumpIdleCrouchCommand : ICommand
    {
        MultiPlayerHolder holder;

        public ToggleJumpIdleCrouchCommand(MultiPlayerHolder holder)
        {
            this.holder = holder;
        }

        public void Execute()
        {
            holder.getCurrentMario().Down();

        }


    }
}
