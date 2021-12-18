using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RushNDestroy
{
    public class SaveSystem
    {
        public static void SaveData(string saveName, object saveData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            
            string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

            // If file exists it gets deleted and replaced by the same file with new data (overwiriting the file does not work)
            if(File.Exists(path))
                File.Delete(path);

            FileStream file = File.Create(path);
            string json = JsonUtility.ToJson(saveData);
            formatter.Serialize(file, json);
            file.Close();
        }
        public static EntityDataToClassList LoadEntities(string saveName)
        {
            string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
            if(!File.Exists(path))
                return null;
            BinaryFormatter formatter = GetBinaryFormater();
            FileStream file = File.Open(path, FileMode.Open);
            try{
                object deserializedFile = formatter.Deserialize(file);
                string json = deserializedFile.ToString();
                EntityDataToClassList save = JsonUtility.FromJson<EntityDataToClassList>(json);
                file.Close();
                return save;
            }
            catch{
                Debug.LogErrorFormat("Failed to load file at {0}", path);
                file.Close();
                return null;
            }
        }
        public static RewardsDataToClass LoadRewards(string saveName)
        {
            string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
            if(!File.Exists(path))
                return null;
            BinaryFormatter formatter = GetBinaryFormater();
            FileStream file = File.Open(path, FileMode.Open);
            try{
                object deserializedFile = formatter.Deserialize(file);
                string json = deserializedFile.ToString();
                RewardsDataToClass save = JsonUtility.FromJson<RewardsDataToClass>(json);
                file.Close();
                return save;
            }
            catch{
                Debug.LogErrorFormat("Failed to load file at {0}", path);
                file.Close();
                return null;
            }
        }
        public static BinaryFormatter GetBinaryFormater()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter;
        }
    }
}