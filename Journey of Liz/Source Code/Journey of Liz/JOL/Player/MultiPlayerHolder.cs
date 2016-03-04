using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL
{
    /// <summary>
    /// This class handles the case that there may be two marios in the same scene.
    /// </summary>

    public class MultiPlayerHolder
    {
        Player player1, player2;

        public MultiPlayerHolder (Player mario, Player luigi){
            this.player1 = mario;
            this.player2 = luigi;
        }

        public Player getCurrentMario()
        {
            if (!player1.isPaused)
            {
                return player1;
            }
            else
            {
                return player2; 
            }
        }
    }
}
