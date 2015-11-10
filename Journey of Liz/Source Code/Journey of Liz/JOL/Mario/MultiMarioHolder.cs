using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL
{
    /// <summary>
    /// This class handles the case that there may be two marios in the same scene.
    /// </summary>

    public class MultiMarioHolder
    {
        Mario mario, luigi;

        public MultiMarioHolder (Mario mario, Mario luigi){
            this.mario = mario;
            this.luigi = luigi;
        }

        public Mario getCurrentMario()
        {
            if (!mario.isPaused)
            {
                return mario;
            }
            else
            {
                return luigi; 
            }
        }
    }
}
