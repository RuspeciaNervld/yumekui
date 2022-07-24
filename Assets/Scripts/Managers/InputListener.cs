using UnityEngine;

public class InputListener : ISingleton<InputListener> {
    public bool keyHold;
    public KeyCode currentKey;

    private void OnGUI() {
        if (Input.anyKey) {
            currentKey = Event.current.keyCode;
            //Debug.Log("key:"+ currentKey);
            //Debugger.Instance.log.text = "key: " + currentKey;
            if (currentKey != KeyCode.None) {
                keyHold = true;
            }
            keyHold = true;
        } else {
            keyHold = false;
        }
    }
}
