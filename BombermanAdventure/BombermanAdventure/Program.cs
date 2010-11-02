using System;

namespace BombermanAdventure
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Game starter
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            using (BombermanAdventureGame game = new BombermanAdventureGame())
            {
                game.Run();
            }
        }
    }
#endif
}

