using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL
{
    class FireStateCommand : ICommand
    {
        Player mario;

        public FireStateCommand(Player mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {
            mario.Collect(new FireFlowerItem());
        }

    }
}
