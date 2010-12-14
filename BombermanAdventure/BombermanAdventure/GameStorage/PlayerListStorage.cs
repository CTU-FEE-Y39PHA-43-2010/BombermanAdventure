using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BombermanAdventure.GameObjects;

namespace BombermanAdventure.GameStorage
{
    internal class PlayerListStorage
    {
        private static PlayerList _pl;

        public static PlayerList PlayerList
        {
            get
            {
                if (_pl == null)
                {
                    _pl = new PlayerList();
                    ReadObject(out _pl, "profile.bin");
                    if (_pl == null || _pl.Profiles == null)
                    {
                        _pl = new PlayerList();
                    }
                }
                return _pl;
            }
        }

        public static void Save()
        {
            WriteObject(ref _pl, "profile.bin");
        }

        private static void WriteObject<T>(ref T gameObject, string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                var bin = new BinaryFormatter();
                bin.Serialize(stream, gameObject);
            }
        }

        private static void ReadObject<T>(out T gameObject, string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Open))
                {
                    var bin = new BinaryFormatter();

                    gameObject = (T) bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
                gameObject = default(T);
            }
        }
    }
}
