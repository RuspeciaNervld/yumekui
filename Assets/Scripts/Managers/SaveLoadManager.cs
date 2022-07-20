using System.IO;
using UnityEngine;

public class SaveLoadManager : ISingleton<SaveLoadManager> {

    public void SaveByPlayerPrefs(string key, object data) {
        var json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

#if UNITY_EDITOR
        Debug.Log("PlayerPrefs保存数据成功");
#endif

    }

    public string LoadFromPlayerPrefs(string key) {
        return PlayerPrefs.GetString(key,null/*默认值*/);
    }

    public void SaveByJson(string saveFileName,object data) {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try {
            File.WriteAllText(path, json);

#if UNITY_EDITOR
            Debug.Log($"成功存储到{path}");
#endif

        } catch(System.Exception e) {
#if UNITY_EDITOR
            Debug.Log($"存储到{path}失败\n{e}");
#endif
        }
    }

    public T LoadFromJson<T>(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);

            return data;
        }catch(System.Exception e) {
#if UNITY_EDITOR
            Debug.Log($"从{path}读取数据失败{e}");
#endif
            return default;
        }
    }

    public void DeleteSaveFile(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try {
            File.Delete(path);
        }catch(System.Exception e) {
#if UNITY_EDITOR
            Debug.Log($"删除{path}失败\n{e}");
#endif
        }
    }
}
