using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanAdventure.GameStorage
{
    class XmlStorage
    {

        public static void WriteObject<T>(ref T gameObject, string filename)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                xmlSerializer.Serialize(file, p);
                file.Close();
            }
        }

    }
}
