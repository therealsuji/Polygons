using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    CharacterController characterController;
    Joystick joystick;
    public float moveSpeed;
    public float turnSpeed;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        joystick = GameObject.FindGameObjectWithTag("JoyStickMovement").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }
    void movePlayer()
    {
        Vector3 joystickDirection = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        if (joystickDirection.sqrMagnitude > 0.05)
        {
            float angle = Mathf.Atan2(joystickDirection.x, joystickDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), Time.deltaTime * turnSpeed);
            Vector3 moveVector = new Vector3(joystickDirection.x, 0, joystickDirection.y);
            characterController.Move(moveVector * Time.deltaTime * moveSpeed);
        }

    }
}
