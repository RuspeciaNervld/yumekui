using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : ISingleton<EventManager> {
    public delegate void event_handler(/*string event_name, object udata*/);

    private Dictionary<string,IEnumerator> tasks = new Dictionary<string, IEnumerator>();
    private Dictionary<string, event_handler> dic = new Dictionary<string, event_handler>();

    public void init() {
    }

    public void AddTask(string task_name , IEnumerator task) {
        if (this.tasks.ContainsKey(task_name)) {
            this.tasks[task_name] = task;
        } else {
            this.tasks.Add(task_name, task);
        }
    }

    public void RemoveTask(string task_name) {
        if (!this.tasks.ContainsKey(task_name)) {
            return;
        }

        this.tasks.Remove(task_name);
    }

    public void StartTask(string task_name) {
        if (!this.tasks.ContainsKey(task_name)) {
            return;
        }

        StartCoroutine(this.tasks[task_name]);
    }

    public void StopTask(string task_name) {
        if (!this.tasks.ContainsKey(task_name)) {
            return;
        }

        StopCoroutine(this.tasks[task_name]);
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


    public void Emit(string event_name/*, object udata*/) {
        if (!this.dic.ContainsKey(event_name)) {
            return;
        }

        this.dic[event_name](/*event_name, udata*/);
    }
}
