using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanAdventure.GameObjects
{
    [Serializable]
    public class PlayerList
    {
        public List<Profile> Profiles;

        public PlayerList()
        {
            Profiles = new List<Profile>(1);
        }
    }
}
