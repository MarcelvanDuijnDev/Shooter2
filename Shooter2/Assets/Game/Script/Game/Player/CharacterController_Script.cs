using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_Script : MonoBehaviour {

    //Movement
    [SerializeField]private float normalSpeed, sprintSpeed;
    [SerializeField]private float jumpSpeed;
    [SerializeField]private float gravity;
    private Vector3 moveDirection = Vector3.zero;
    //Look around
    public float cameraSensitivity;
    [SerializeField]private Transform head,cameraObj;
    [SerializeField]private Vector3 headPos_Fps, headPos_Third;
    private bool headMode;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private float speed;

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

        //Sprint
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
        //camera mode
        if (Input.GetKeyDown(KeyCode.B))
        {
            headMode = !headMode;
        }
        if (headMode)
        {
            cameraObj.localPosition = headPos_Fps;
        }
        else
        {
            cameraObj.localPosition = headPos_Third;
        }
    }
}
