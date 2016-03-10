using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL
{
    class DemoStateCommand : ICommand
    {
        Player player;

        public DemoStateCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.Collect(new CheatPotionItem());
        }

    }
}
