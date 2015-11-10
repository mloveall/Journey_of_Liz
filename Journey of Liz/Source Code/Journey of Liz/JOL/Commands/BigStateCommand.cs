using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class BigStateCommand : ICommand
    {
          Mario mario;

          public BigStateCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {
            mario.Collect(new MushroomItem());
        }
    }
}
