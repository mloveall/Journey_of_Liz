using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL
{
    class FireStateCommand : ICommand
    {
        Mario mario;

        public FireStateCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {
            mario.Collect(new FireFlowerItem());
        }

    }
}
