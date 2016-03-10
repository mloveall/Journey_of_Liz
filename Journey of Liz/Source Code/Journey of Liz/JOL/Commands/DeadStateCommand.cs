using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Classes.ItemClasses;

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
            mario.Collect(new DeathPotionItem());
        }
    }
}
