using UnityEngine;

public class InputListener : ISingleton<InputListener> {
    public bool keyHold;
    public KeyCode currentKey;

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
    }

    private void OnGUI() {
        if (Input.anyKey) {
            currentKey = Event.current.keyCode;
            //Debug.Log("key:"+ currentKey.ToString());
            if (currentKey != KeyCode.None) {
                keyHold = true;
            }
        } else {
            keyHold = false;
        }
    }
}
