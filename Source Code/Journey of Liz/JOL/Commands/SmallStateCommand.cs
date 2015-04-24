using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class SmallStateCommand : ICommand
    {
         Mario mario;

         public SmallStateCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {

        }
    }
}
