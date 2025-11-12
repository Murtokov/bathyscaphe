using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    public static SavesManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private static Dictionary<string, System.Type> sceneToConfigType;

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        sceneToConfigType = new Dictionary<string, System.Type>
        {
            { "Level1MainBase", typeof(Level1MainBase) },
            { "Level1ParkourRoom", typeof(Level1ParkourRoom) },
            { "Level1UpperBaseLeftSide", typeof(Level1UpperBaseLeftSide) },
            { "Level1UpperBaseRightSide", typeof(Level1UpperBaseRightSide) },
            { "Level1Ocean", typeof(Level1Ocean) }
        };
    }

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

    public static object LoadConfigForScene(string sceneName)
    {
        if (sceneToConfigType.ContainsKey(sceneName))
        {
            System.Type configType = sceneToConfigType[sceneName];
            var method = typeof(SavesManager).GetMethod("LoadConfig").MakeGenericMethod(configType);
            return method.Invoke(null, new object[] { sceneName });
        }
        return null;
    }

    public static void SaveProgress()
    {
        InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        string filePath = Path.Combine(Application.persistentDataPath + "/Inventory.json");
        Inventory.SaveInventory(filePath);
    }
}
