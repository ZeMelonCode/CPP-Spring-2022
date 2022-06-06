using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LoadSaveSystem
{
    public static void SaveState(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.xml";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataManager gameState = new DataManager(manager);

        //Bun Bun useless garbo
        //string json = JsonUtility.ToJson(gameState);
       // File.WriteAllText(path, json);



        formatter.Serialize(stream, gameState);
        stream.Close();

        Debug.Log("Game Saved !");
    }

    public static DataManager LoadState()
    {
        string path = Application.persistentDataPath + "/data.xml";
        FileStream stream = new FileStream(path, FileMode.Open);
        if (File.Exists(path) && stream.Length > 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            
            
            //return JsonUtility.FromJson<DataManager>(File.ReadAllText(path));

            DataManager data = formatter.Deserialize(stream) as DataManager;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
             return null;
        }
    }
}
