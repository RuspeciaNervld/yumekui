using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void event_handler(string event_name, object udata);

    private Dictionary<string, event_handler> dic = new Dictionary<string, event_handler>();

    public void init() {
    }

    public void AddListener(string event_name, event_handler h) {
        if (this.dic.ContainsKey(event_name)) {
            this.dic[event_name] += h;
        } else {
            this.dic.Add(event_name, h);
        }
    }

    public void RemoveListener(string event_name, event_handler h) {
        if (!this.dic.ContainsKey(event_name)) {
            return;
        }

        this.dic[event_name] -= h;

        if (this.dic[event_name] == null) {
            this.dic.Remove(event_name);
        }
    }

    public void Emit(string event_name, object udata) {
        if (!this.dic.ContainsKey(event_name)) {
            return;
        }

        this.dic[event_name](event_name, udata);
    }
}
