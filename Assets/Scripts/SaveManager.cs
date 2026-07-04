using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            return new SaveData();
        }
    }
}
