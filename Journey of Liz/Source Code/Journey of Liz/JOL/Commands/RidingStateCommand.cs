using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Commands
{
    class RidingStateCommand : ICommand
    {
          Player player;

          public RidingStateCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.Collect(new BardieEggItem());
        }
    }
}
