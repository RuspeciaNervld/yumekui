using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRusRandom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"No.{RusRandomer.hello()}:Negev");
        Debug.Log(RusRandomer.randNum(1, 10));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
