using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Dictionary<string, GameObject> view = new Dictionary<string, GameObject>();

    private void load_all_object(GameObject root, string path) {
        foreach (Transform tf in root.transform) {
            if (this.view.ContainsKey(path + tf.gameObject.name)) {
                // Debugger.LogWarning("Warning object is exist:" + path + tf.gameObject.name + "!");
                continue;
            }
            this.view.Add(path + tf.gameObject.name, tf.gameObject);
            load_all_object(tf.gameObject, path + tf.gameObject.name + "/");
        }
    }

    public virtual void Awake() {
        this.load_all_object(this.gameObject, "");
    }

    public void add_button_listener(string view_name, UnityAction onclick) {
        Button bt = this.view[view_name].GetComponent<Button>();
        if (bt == null) {
            Debug.LogWarning("UI_manager add_button_listener: not Button Component!");
            return;
        }

        bt.onClick.AddListener(onclick);
    }

    public void add_slider_listener(string view_name, UnityAction<float> on_value_changle) {
        Slider s = this.view[view_name].GetComponent<Slider>();
        if (s == null) {
            Debug.LogWarning("UI_manager add_slider_listener: not Slider Component!");
            return;
        }

        s.onValueChanged.AddListener(on_value_changle);
    }
}

public class UIManager : ISingleton<UIManager> {
    public GameObject canvas;

    public override void Awake() {
        base.Awake();
        this.canvas = GameObject.Find("Canvas");
        if (this.canvas == null) {
            Debug.LogError("UI manager load  Canvas failed!!!!!");
        }
    }

    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
    }

    public UIController ShowUIView(string name) {
        string path = "GUI/UIPrefabs/" + name + ".prefab";
        GameObject ui_prefab = (GameObject)ResourceManager.Instance.GetAssetCache<GameObject>(path);
        GameObject ui_view = GameObject.Instantiate(ui_prefab);
        
        ui_view.name = ui_prefab.name;
        ui_view.transform.SetParent(this.canvas.transform, false);

        //todo 手动改参数修bug
        RectTransform rectTransform = ui_view.GetComponent<RectTransform>();
        //x rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.localScale = new Vector3(1, 1, 1);
        //end

        int lastIndex = name.LastIndexOf("/");
        if (lastIndex > 0) {
            name = name.Substring(lastIndex + 1);
        }

        Type type = Type.GetType(name + "_UICtrl");
        UIController ctrl = (UIController)ui_view.AddComponent(type);

        return ctrl;
    }

    public GameObject ShowSubView(string name, GameObject parent = null) {
        string path = "GUI/UI_Prefabs/" + name + ".prefab";
        GameObject ui_prefab = (GameObject)ResourceManager.Instance.GetAssetCache<GameObject>(path);
        GameObject ui_view = GameObject.Instantiate(ui_prefab);
        ui_view.name = ui_prefab.name;
        if (parent == null) {
            parent = this.canvas;
        }
        ui_view.transform.SetParent(parent.transform, false);
        return ui_view;
    }

    public void RemoveUIView(string name) {
        int lastIndex = name.LastIndexOf("/");
        if (lastIndex > 0) {
            name = name.Substring(lastIndex + 1);
        }

        Transform view = this.canvas.transform.Find(name);
        if (view) {
            GameObject.DestroyImmediate(view.gameObject);
        }
    }

    public void RemoveAllViews() {
        List<Transform> children = new List<Transform>();
        foreach (Transform t in this.canvas.transform) {
            children.Add(t);
        }

        for (int i = 0; i < children.Count; i++) {
            GameObject.DestroyImmediate(children[i].gameObject);
        }
    }
}
