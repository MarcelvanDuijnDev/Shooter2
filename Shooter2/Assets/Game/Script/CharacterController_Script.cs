using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_Script : MonoBehaviour {

    //Movement
    [SerializeField]private float speed = 6.0F;
    [SerializeField]private float jumpSpeed = 8.0F;
    [SerializeField]private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    //Look around
    [SerializeField]private float cameraSensitivity = 90;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    [SerializeField]private Transform head;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() 
    {
        //Look around
        rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp (rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        head.transform.localRotation = Quaternion.AngleAxis(rotationY, Vector3.left);

        //Movement
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
