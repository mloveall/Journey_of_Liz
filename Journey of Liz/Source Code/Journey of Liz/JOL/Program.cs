using System;

namespace JOL
{
#if WINDOWS || XBOX

    /// <summary>
    /// The main entry point for the application.
    /// </summary>

    static class Program
    {
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

