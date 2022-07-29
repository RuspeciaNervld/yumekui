using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBullet : MonoBehaviour
{
    AudioClip se;
    Effector2D ve;

    [Header("=== settings ===")]
    public bool canBeAgainst;
    public float attack;
    public float speed;

    [SerializeField]protected bool initiailized;
    public abstract void Init(float attack,Vector2 parent);

   
}
