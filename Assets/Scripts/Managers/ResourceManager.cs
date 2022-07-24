using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ResourceManager : ISingleton<ResourceManager>
{
    public override void Awake() {
        base.Awake();
        // this.gameObject.AddComponent<AssetBundleManager>();
    }

    public T GetAssetCache<T>(string name) where T : UnityEngine.Object {
//#if UNITY_EDITOR
//        // if (AssetBundleConfig.IsEditorMode)
//        {
//            // string path = AssetBundleUtility.PackagePathToAssetsPath(name);
//            string path = "Assets/Resources/" + name;
//            // Debug.Log(path);
//            UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
//            return target as T;
//        }
//        // return AssetBundleManager.Instance.GetAssetCache(name) as T;
//#endif
        string path = name.Split('.')[0];
        Object target = Resources.Load<T>(path);
        return target as T;
    }
}
