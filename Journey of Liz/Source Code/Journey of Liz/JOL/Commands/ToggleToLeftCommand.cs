using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class ToggleToLeftCommand : ICommand
    {
        MultiPlayerHolder holder;

        public ToggleToLeftCommand(MultiPlayerHolder holder)
        {
            this.holder = holder;
        }

        public void Execute()
        {
            holder.getCurrentMario().Left();

        }


    }
}
