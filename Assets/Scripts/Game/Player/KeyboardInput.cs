using UnityEngine;

public class KeyboardInput : IUserInput {

    [Header("=== key settings ===")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyDash = "left shift";
    public string keyJump = "space";
    public string keyAttack = "j";

    //public string keyJRight = "right";
    //public string keyJLeft = "left";
    //public string keyJDown = "down";
    //public string keyJUp = "up";

    [Header("===== Mouse Settings =====")]
    public bool mouseEnable = true;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    // Start is called before the first frame update
    private void Start() {
    }

    private void Update() {
        // 获取跳跃信号，且是连续的
        jump = Input.GetKey(keyJump);  //! GetKey是对应实际键值，GetButton是虚拟键值
        jumpKeyDown = Input.GetKeyDown(keyJump);

        // 获取水平方向移动信号
        if (Input.GetKey(keyRight)) {
            xDir = 1;
        } else if (Input.GetKey(keyLeft)) {
            xDir = -1;
        } else {
            xDir = 0;
        }

        // 冲刺信号
        if (Input.GetKeyDown(keyDash)) {
            dash = true;
        } else {
            dash = false;
        }

        
        attack = Input.GetKeyDown(keyAttack);
        

        // 着地信号
        isGrounded = c2d.IsTouchingLayers(ground);
        // 挂壁信号
        isOnWall = c2d.IsTouchingLayers(wall);
    }
}
