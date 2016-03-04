using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL
{
    class DeadStateCommand : ICommand
    {
        Player mario;

        public DeadStateCommand(Player mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {
            mario.Collect(new DeadMushroomItem());
        }

    }
}
