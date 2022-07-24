using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))] //指定必要的关联组件
public class PlayerController : MonoBehaviour {

    [Header("=== objects ===")]
    public LayerMask ground; //地面层
    public LayerMask wall;

    public Collider2D c2d; // 自身的碰撞器
    
    private Rigidbody2D rb;
    private IUserInput input;
    private Player player;

    [Header("=== value settings ===")]
    public float speed;
    public float jumpForce;
    public float jumpTime;
    public float dashCD;
    public float dashSpeed;
    public float dashTime;
    public float wallForce;
    public float targetG;
    public float rushForce;
    public float woodTime; //! 无法操控时间

    [Header("=== ability settings ===")]
    public bool canDoubleJump;
    public bool canDash;
    public bool canStayWall;
    public bool canControl;
    public bool canRush;

    private float jumpTimeCounter;
    private float dashTimeCounter;
    private float dashColdTime;

    private bool isJumping;
    private bool doubleJumped;
    private bool doubleJump;
    private bool holdingJump;
    private bool rushed;

    private KeyboardInput keyInput;
    private JoystickInput joyInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        keyInput = GetComponent<KeyboardInput>();
        joyInput = GetComponent<JoystickInput>();

        if (Input.GetJoystickNames()[0] == "") { //没插手柄
            input = keyInput;
        } else {
            input = joyInput;
        }
    }

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        if (!canControl) {
            input.enabled = false;
        } else {
            input.enabled = true;
        }

        if (InputListener.Instance.keyHold) {
            input = keyInput;
        } else {
            input = joyInput;
        }

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
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }
        if (input.attack) {
            player.NormalAttack();
        }
        

        //! 二段跳模块
        if (canDoubleJump) {
            if (!input.isGrounded && !input.jump && !doubleJumped) { // 空中松开跳跃
                doubleJump = true;
                holdingJump = false;
            }
            if (input.jump && !input.isGrounded && doubleJump) { // 空中第一次再次按下跳跃
                rb.velocity = Vector2.up * jumpForce;
                doubleJump = false;
                doubleJumped = true;
            }
        }

        //! 挂壁模块
        if (canStayWall) {
            if (input.isOnWall && !input.isGrounded) {
                if (rb.gravityScale != targetG) {
                    rb.gravityScale = targetG;
                    rb.velocity = Vector2.zero;
                }
                if (input.jumpKeyDown) {
                    rb.velocity = new Vector2(0, jumpForce);
                }
            } else {
                rb.gravityScale = 1f;
            }
        }

        //! 加速模块
        if (canDash) {
            dashColdTime -= Time.deltaTime;
            dashTimeCounter -= Time.deltaTime;
            if (input.dash) {
                if (dashColdTime <= 0) { // 冷却结束，可以冲刺
                    canControl = false;
                    input.dash = false;
                    input.dashTrigger = true; // 给动画信号
                    dashTimeCounter = dashTime;
                    dashColdTime = dashCD;  // 冷却重置
                }
            }
        }

        

    }

    private void FixedUpdate() { // 物理相关的更新放在这里
        if (dashTimeCounter < 0) {
            canControl = true;
        }
        rb.velocity = new Vector2( (dashTimeCounter < 0 ? input.xDir * speed : transform.localScale.x * dashSpeed), rb.velocity.y);
    }
}
