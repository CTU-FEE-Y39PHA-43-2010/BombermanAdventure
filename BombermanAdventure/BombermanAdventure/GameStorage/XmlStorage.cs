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
            string path = filename;
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                xmlSerializer.Serialize(file, gameObject);
            }
        }

        public static void ReadObject<T>(out T gameObject, string filename)
        {
            string path = filename;
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                gameObject = (T)reader.Deserialize(file);
            }            
        }

        public static string GenerateHashedPlayerProfileFileName(string filename)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(filename);
            data = x.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();
            return ret;
        }

    }
}
