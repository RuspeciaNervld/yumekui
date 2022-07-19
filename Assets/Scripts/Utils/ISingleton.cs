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
                    //obj.hideFlags = HideFlags.HideAndDontSave; //! 隐藏实例化对象,也就是无法销毁
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake() {//! 设置为不可销毁,如果已经选择隐藏可不重载此方法
        DontDestroyOnLoad(this.gameObject);
        if(_instance == null) {
            _instance = this as T;
        } else {
            GameObject.Destroy(this.gameObject);
        }
    }
}
