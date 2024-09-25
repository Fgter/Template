using QFramework;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEditor;
public class Storage : IUtility
{
    static string dataPath = Application.persistentDataPath + PathConfig.SavePath;
    public void Save<T>(T obj, string fileName = null)
    {
        if (!File.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
        string path = Path.Combine(dataPath, typeof(T).ToString() + fileName);
        string json = JsonConvert.SerializeObject(obj, new VectorConverter());//VectorConverter处理坐标信息
        File.WriteAllText(path, json);
#if UNITY_EDITOR
        Debug.Log(path);
#endif
    }

    public T Load<T>(string fileName = null)
    {
        string path = Path.Combine(dataPath, typeof(T).ToString() + fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T data = JsonConvert.DeserializeObject<T>(json, new VectorConverter());
            return data;
        }
        else
            return default;
    }

    public void RemoveSave<T>()
    {
        string path = Path.Combine(dataPath, nameof(T));
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    [MenuItem("Saves/ClearSaves")]
    public static void RemoveAllSaves()
    {
        string path = dataPath;
        DirectoryInfo dir = new DirectoryInfo(path);
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
        foreach (FileSystemInfo i in fileinfo)
        {
            File.Delete(i.FullName);
        }
    }

    [MenuItem("Saves/将时间的存档倒退一天")]
    public static void SavesBackOneDay()
    {
        SaveData.TimeSaveData timeSaveData = new SaveData.TimeSaveData();
        timeSaveData.lastExitTime = System.DateTime.Now.AddDays(-1);

        string path = Path.Combine(dataPath, typeof(SaveData.TimeSaveData).ToString());
        string json = JsonConvert.SerializeObject(timeSaveData, new VectorConverter());
        File.WriteAllText(path, json);
        Debug.Log("目前存档时间" + json);
    }
}
