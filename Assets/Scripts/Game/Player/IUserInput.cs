using UnityEngine;

public abstract class IUserInput : MonoBehaviour {

    [Header("=== objects ===")]
    public LayerMask ground; //地面层
    public LayerMask wall;

    public Collider2D c2d; // 自身的碰撞器

    [Header("=== output signals ===")]
    public int xDir; // 水平方向移动，为1、0、-1
    public bool jump;
    public bool jumpKeyDown;
    public bool isGrounded;
    public bool isOnWall;
    public bool dash;
    public bool dashTrigger; // 动画专用信号
}
