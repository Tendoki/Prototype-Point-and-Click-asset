using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySavingSystem
{
    public static void SaveGame(string SaveName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + SaveName + ".save";    

        FileStream stream = new FileStream(path, FileMode.Create);

        Save save = new Save();

        formatter.Serialize(stream, save);

        stream.Close();

        //Debug.Log("Game Saved" + path);
    }

    public static Save LoadGame(string SaveName)
	{
        string path = Application.persistentDataPath + "/" + SaveName + ".save";
        if (File.Exists(path))
		{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save save = formatter.Deserialize(stream) as Save;
            stream.Close();
            return save;
		}
        else
		{
            Debug.LogError("Save file not found in " + path);
            return null;
		}            
    }
}
