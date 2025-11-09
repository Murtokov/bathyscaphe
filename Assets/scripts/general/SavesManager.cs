using System.IO;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    public static void SaveConfig<T>(T configData, string fileName = "config") where T : class
    {
        string json = JsonUtility.ToJson(configData);
        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.json");
        File.WriteAllText(filePath, json);
        Debug.Log($"Сохранено: {filePath}");
    }

    public static T LoadConfig<T>(string fileName = "config") where T : class
    {
        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.json");
        if (!File.Exists(filePath))
        {
            Debug.Log($"Файл не найден: {filePath}");
            return null;
        }
        string json = File.ReadAllText(filePath);
        T configData = JsonUtility.FromJson<T>(json);
        return configData;
    }
}
