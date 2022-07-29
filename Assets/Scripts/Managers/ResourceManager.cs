using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ResourceManager : ISingleton<ResourceManager> {
    public override void Awake() {
        base.Awake();
        // this.gameObject.AddComponent<AssetBundleManager>();
    }

    public T GetAssetCache<T>(string name) where T : UnityEngine.Object {
#if UNITY_EDITOR
        // if (AssetBundleConfig.IsEditorMode)
        {
            // string path = AssetBundleUtility.PackagePathToAssetsPath(name);
            string path2 = "Assets/Resources/" + name;
            // Debug.Log(path);
            UnityEngine.Object target2 = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path2);
            return target2 as T;
        }
        // return AssetBundleManager.Instance.GetAssetCache(name) as T;
#endif
        string path = name.Split('.')[0];
        Object target = Resources.Load<T>(path);
        return target as T;
    }
}
