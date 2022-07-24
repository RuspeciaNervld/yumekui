using UnityEngine;

public class AnimatorController : MonoBehaviour {

    [Header("=== objects ===")]
    private Rigidbody2D rb;
    private Animator anim;
    private IUserInput input;
    private KeyboardInput keyInput;
    private JoystickInput joyInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        keyInput = GetComponent<KeyboardInput>();
        joyInput = GetComponent<JoystickInput>();

        //if (Input.GetJoystickNames()[0] == "") { //Ã»²åÊÖ±ú
        //    input = GetComponent<KeyboardInput>();
        //} else {
        //    input = GetComponent<JoystickInput>();
        //}
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

        if (input.xDir == 1) {
            //transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1,1,1);
        } else if (input.xDir == -1) {
            //transform.eulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (input.isGrounded) {
            anim.SetBool("isGrounded", true);
        } else {
            anim.SetBool("isGrounded", false);
        }

        if (input.xDir != 0) {
            anim.SetBool("isMoving", true);
        } else {
            anim.SetBool("isMoving", false);
        }

        if (input.dashTrigger) {
            anim.SetTrigger("dash");
            input.dashTrigger = false;
        }
    }

    public void move() {
        AudioManager.Instance.playDub(AudioManager.Instance.slimeMove);
    }
}
