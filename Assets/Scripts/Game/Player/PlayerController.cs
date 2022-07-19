using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))] //ָ����Ҫ�Ĺ������
public class PlayerController : MonoBehaviour {

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
    private KeyboardInput keyInput;
    private JoystickInput joyInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        keyInput = GetComponent<KeyboardInput>();
        joyInput = GetComponent<JoystickInput>();

        if (Input.GetJoystickNames()[0] == "") { //û���ֱ�
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
        if (InputListener.Instance.keyHold) {
            input = keyInput;
        } else {
            input = joyInput;
        }

        if (input.isGrounded) {
            doubleJumped = false;
            holdingJump = true;
        }

        //! ��������
        if (input.isGrounded && input.jump) { // �ڵ����ϰ�����Ծ
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
        if (input.jump && isJumping && holdingJump) { // ���������׶Σ�������ס��Ծ��
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        //! ������ģ��
        if (canDoubleJump) {
            if (!input.isGrounded && !input.jump && !doubleJumped) { // �����ɿ���Ծ
                doubleJump = true;
                holdingJump = false;
            }
            if (input.jump && !input.isGrounded && doubleJump) { // ���е�һ���ٴΰ�����Ծ
                rb.velocity = Vector2.up * jumpForce;
                doubleJump = false;
                doubleJumped = true;
            }
        }

        //! dashģ��
        if (canDash) {
            dashColdTime -= Time.deltaTime;
            dashTimeCounter -= Time.deltaTime;
            if (input.dash) {
                input.dash = false;
                if (dashColdTime <= 0) { // ��ȴ���������Գ��
                    input.dashTrigger = true; // �������ź�
                    dashTimeCounter = dashTime;
                    dashColdTime = dashCD;  // ��ȴ����
                }
            }
        }
    }

    private void FixedUpdate() { // ������صĸ��·�������
        rb.velocity = new Vector2(input.xDir * (dashTimeCounter < 0 ? speed : dashSpeed), rb.velocity.y);
    }
}