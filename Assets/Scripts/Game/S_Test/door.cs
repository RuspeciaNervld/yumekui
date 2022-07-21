using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public BoxCollider2D cd;
    public Vector3 before;
    public Vector3 after;
    //IEnumerator open() {
    //    yield return null;
    //}

    //private void open() {
    //    var cur = Vector3.Lerp(before, after, 0.1f);
    //    //var cur = after;
    //    transform.localPosition = cur;
    //}

    IEnumerator open() {
        while (true) {
            //var cur = Vector3.Lerp(before, after, 0.1f);
            Vector3 cur = transform.localPosition + new Vector3(0, 0.1f, 0);
            transform.localPosition = cur;
            if (cur.y >= after.y) {
                //StopCoroutine(open()); //自然结束就可以了
                break;
            }
            yield return 0;//! 等到下一帧
        }
    }

    private void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddTask("open"+GetInstanceID(), open());
    }

    private void OnDestroy() {
        EventManager.Instance.StopTask("open"+GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EventManager.Instance.StartTask("open" + GetInstanceID());
        }
    }
}
