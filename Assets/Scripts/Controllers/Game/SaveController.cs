using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveController
{
    public static string GetSavePath()
    {
        return Application.persistentDataPath + "/PlatformerGameSave.f";
    }

    public static void SaveState(Player player, GameController gameController, ItemManager itemManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetSavePath();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        { 
            PlayerData data = new PlayerData(player, gameController, itemManager);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static PlayerData LoadState()
    {
        string path = GetSavePath();
        if (!File.Exists(path))
        {
            Debug.Log("Save File does not exists");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
    }
}
