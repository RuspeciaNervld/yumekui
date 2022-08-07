using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GetName : MonoBehaviour
{
    public GameObject parent;
    private void Start() {
        Text text = GetComponent<Text>();
        text.text = parent.name;
    }
}
