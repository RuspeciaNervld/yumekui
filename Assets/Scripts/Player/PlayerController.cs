using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(Rigidbody2D))] //指定必要的关联组件
public class PlayerController : MonoBehaviour
{
    [Header("=== objects ===")]
    private Rigidbody2D rb;
    private Animator anim;
    private IUserInput input;

    [Header("=== value settings ===")]
    public float speed;
    public float jumpForce;
    public float jumpTime;
    public float dashCD;
    public float dashSpeed;
    public float dashTime;

    [Header("=== ability settings ===")]
    public bool canDoubleJump;
    public bool canDash;

    private float jumpTimeCounter;
    private float dashTimeCounter;
    private float dashColdTime;
    private bool isJumping;
    private bool doubleJumped;
    private bool doubleJump;
    private bool holdingJump;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<IUserInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.isGrounded) {
            doubleJumped = false;
            holdingJump = true;
        }

        //! 基本操作
        if (input.isGrounded && input.jump) { // 在地面上按了跳跃
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
        if (input.jump && isJumping && holdingJump) { // 主动上升阶段（持续按住跳跃）
            if(jumpTimeCounter > 0) {
                Debug.Log("3");
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        //! 二段跳模块
        if (canDoubleJump) {
            if (!input.isGrounded && !input.jump && !doubleJumped) { // 空中松开跳跃
                Debug.Log("1");
                doubleJump = true;
                holdingJump = false;
            }
            if (input.jump && !input.isGrounded && doubleJump) { // 空中第一次再次按下跳跃
                Debug.Log("2");
                rb.velocity = Vector2.up * jumpForce;
                doubleJump = false;
                doubleJumped = true;
            }
        }

        //! dash模块
        if (canDash) {
            dashColdTime -= Time.deltaTime;
            dashTimeCounter -= Time.deltaTime;
            if (input.dash) {
                input.dash = false;
                if (dashColdTime <= 0) { // 冷却结束，可以冲刺
                    input.dashTrigger = true; // 给动画信号
                    dashTimeCounter = dashTime;
                    dashColdTime = dashCD;  // 冷却重置
                }
            }
        }
        
    }

    void FixedUpdate() { // 物理相关的更新放在这里
        rb.velocity = new Vector2(input.xDir * (dashTimeCounter < 0?speed:dashSpeed), rb.velocity.y);
    }
}
