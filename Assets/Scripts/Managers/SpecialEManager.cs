using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class SpecialEManager : ISingleton<SpecialEManager>
{
    public GameObject camera;
    public Volume volume;

    private Vector3 cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 相机抖动
    /// </summary>
    /// <param name="range">抖动强度，推荐0.1f</param>
    /// <param name="time">抖动时间，推荐0.35f</param>
    public void DoShake(float range,float time) {
        EventManager.Instance.StopTask("ShakeCamera");
        camera.transform.position = cameraPos;
        EventManager.Instance.AddTask("ShakeCamera", ShakeCamera(range, time));
        EventManager.Instance.StartTask("ShakeCamera");
    }

    IEnumerator ShakeCamera(float range,float time) {
        Vector3 prePos = camera.transform.position;
        while (time >=0) {

            time -= Time.deltaTime;
            if (time < 0) {
                break;
            }
            Vector3 pos = camera.transform.position;
            pos.x += Random.Range(-range, range);
            pos.y += Random.Range(-range, range);
            camera.transform.position = pos;
            yield return null;
        }
        camera.transform.position = prePos;
    }

    /// <summary>
    /// 子弹时间
    /// </summary>
    /// <param name="time">持续时间，推荐0.35f</param>
    /// /// <param name="timeScale">时间缩放比例，推荐0.25f</param>
    public void DoBulletTime(float time,float timeScale) {
        EventManager.Instance.StopTask("BulletTime");
        Time.timeScale = 1;
        EventManager.Instance.AddTask("BulletTime", BulletTime(time, timeScale));
        EventManager.Instance.StartTask("BulletTime");
    }

    IEnumerator BulletTime(float time,float timeScale) {
        Time.timeScale = timeScale;
        while(time >= 0) {
            time -= Time.deltaTime;
            if(time < 0) {
                break;
            }
            yield return null;
        }
        Time.timeScale = 1;
    }
}
