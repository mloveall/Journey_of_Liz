using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class ToggleToLeftCommand : ICommand
    {
        MultiMarioHolder holder;

        public ToggleToLeftCommand(MultiMarioHolder holder)
        {
            this.holder = holder;
        }

        public void Execute()
        {
            holder.getCurrentMario().Left();

        }


    }
}
