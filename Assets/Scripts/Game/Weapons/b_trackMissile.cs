using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b_trackMissile : IBullet
{

    [SerializeField] public Vector2 midPos;
    [SerializeField] public Vector2 startPos;//? 加不加serialize差别这么大嘛
    [SerializeField] public Vector2 lastTargetPos;
    [SerializeField] public Transform target;
    [SerializeField] public float percentSpeed;
    [SerializeField] public float percent;
    public override void Init(float attack, Vector2 parent) {
        this.attack = attack;
        transform.position = parent;
        startPos = transform.position;

        GameObject[] enemys = GameObject.FindGameObjectsWithTag(CONSTS.TAG_ENEMY);

        if (enemys.Length > 0){
            int rdId = Random.Range(0, enemys.Length);
            target = enemys[rdId].transform;
            lastTargetPos = target.position;
        } else {
            //lastTargetPos =Utils
        }

        

        midPos = GetMiddlePosition(parent, lastTargetPos);
        percentSpeed = speed / (lastTargetPos - startPos).magnitude;

        initiailized = true;
    }

    Vector2 GetMiddlePosition(Vector2 a, Vector2 b) {
        Vector2 m = Vector2.Lerp(a, b, 0.1f);
        Vector2 normal = Vector2.Perpendicular(a - b).normalized;
        float rd = Random.Range(-2f,0.5f);
        float curveRatio = 0.3f;
        return m + (a - b).magnitude * curveRatio * rd * normal;
    }

    private void Update() {
        if (!initiailized) {
            return;
        }
        if (target) {
            lastTargetPos = target.position;
        }
        percent += percentSpeed * Time.deltaTime;
        if (percent > 1) {
            Vanish();
        }
        transform.LookAt(target.transform);
        transform.position = Utils.Bezier(percent, startPos, midPos, lastTargetPos);
    }

    private void Vanish() {
        Destroy(gameObject);
    }
}
