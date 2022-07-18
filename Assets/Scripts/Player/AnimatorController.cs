using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [Header("=== objects ===")]
    private Rigidbody2D rb;
    private Animator anim;
    private IUserInput input;


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (Input.GetJoystickNames()[0] == "") { //Ã»²åÊÖ±ú
            input = GetComponent<KeyboardInput>();
        } else {
            input = GetComponent<JoystickInput>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.xDir == 1) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else if(input.xDir == -1) {
            transform.eulerAngles = new Vector3(0, 180, 0);
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
}
