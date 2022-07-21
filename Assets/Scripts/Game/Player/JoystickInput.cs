using UnityEngine;

public class JoystickInput : IUserInput {

    [Header("=== key settings ===")]
    public string axisX = "X axis";
    public string axisY = "Y axis";
    public string axis2X = "4th axis"; // 右侧摇杆
    public string axis2Y = "5th axis";
    public string btnA = "joystick button 0";
    public string btnB = "joystick button 1";
    public string btnX = "joystick button 2";
    public string btnY = "joystick button 3";
    public string LB = "joystick button 4";
    public string RB = "joystick button 5";
    public string LRT = "3rd axis"; // 右正左负
    public string back = "joystick button 6";
    public string home = "joystick button 7";

    // Start is called before the first frame update
    private void Start() {
    }

    private void Update() {
        // 获取跳跃信号，且是连续的
        jump = Input.GetKey(btnA);
        jumpKeyDown = Input.GetKeyDown(btnA);

        // 获取水平方向移动信号
        xDir = (int)Input.GetAxis(axisX);

        // 冲刺信号
        if ((int)Input.GetAxis(LRT) == -1) {
            dash = true;
        }

        // 着地信号
        isGrounded = c2d.IsTouchingLayers(ground);
        // 挂壁信号
        isOnWall = c2d.IsTouchingLayers(wall);
    }
}
