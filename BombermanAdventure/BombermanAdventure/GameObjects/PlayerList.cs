using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanAdventure.GameObjects
{
    [Serializable]
    public class PlayerList
    {
        public List<Player> profiles;

        public PlayerList()
        {
            profiles = new List<Player>(1);
        }
    }
}
