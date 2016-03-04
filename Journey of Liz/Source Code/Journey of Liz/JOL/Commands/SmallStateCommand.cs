using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class SmallStateCommand : ICommand
    {
         Player mario;

         public SmallStateCommand(Player mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {

        }
    }
}
