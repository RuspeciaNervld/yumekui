using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{

    [Header("=== output signals ===")]
    public int xDir; // 水平方向移动，为1、0、-1
    public bool jump;
    public bool isGrounded;
    public bool dash;
    public bool dashTrigger; // 动画专用信号
}
