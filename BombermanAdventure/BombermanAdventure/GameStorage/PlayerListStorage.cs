using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BombermanAdventure.GameObjects;

namespace BombermanAdventure.GameStorage
{
    class PlayerListStorage
    {

        private static PlayerList pl = null;

        public static PlayerList PlayerList
        {
            get 
            {
                if (pl == null) {
                    pl = new PlayerList();
                    PlayerListStorage.ReadObject<PlayerList>(out pl, "profile.bin");
                    if (pl == null)
                    {
                        pl = new PlayerList();
                    }
                }
                return pl;
            }
        }

        public static void Save()
        {
            WriteObject(ref pl, "profile.bin");
        }

        private static void WriteObject<T>(ref T gameObject, string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, gameObject);
            }
        }

        private static void ReadObject<T>(out T gameObject, string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    gameObject = (T)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
                gameObject = default(T);
            }

        }

    }
}
