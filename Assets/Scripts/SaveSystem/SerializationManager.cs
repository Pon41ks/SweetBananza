using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

    public static class SerializationManager
    {
        private static readonly string Path = Application.persistentDataPath + "/saves/Save.save";

        public static void Save(object saveData)
        {
            BinaryFormatter formatter = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }


            FileStream fileStream = File.Create(Path);

            formatter.Serialize(fileStream, saveData);

            fileStream.Close();

        
        }
        public static object Load()
        {
            if (!File.Exists(Path))
            {
                return null;
            }

            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream fileStream = File.Open(Path, FileMode.Open);

            try
            {
                object save = formatter.Deserialize(fileStream);
                fileStream.Close();
                return save;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to load file at {0}", Path);
                fileStream.Close();
                return null;
            }
        Debug.Log("Success");
        }
        private static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter;
        }
    }
