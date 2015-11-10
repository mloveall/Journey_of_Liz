using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class ToggleCrouchIdleJumpCommand : ICommand
    {
        MultiMarioHolder holder;

        public ToggleCrouchIdleJumpCommand(MultiMarioHolder holder)
        {
            this.holder = holder;
        }

        public void Execute()
        {
            holder.getCurrentMario().Up();

        }

    }
}
