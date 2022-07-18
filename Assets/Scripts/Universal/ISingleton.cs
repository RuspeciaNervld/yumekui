using UnityEngine;

public abstract class ISingleton<T> : MonoBehaviour
    where T : Component {
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null) {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave; //! 隐藏实例化对象
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
