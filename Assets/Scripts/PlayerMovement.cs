#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float turnSensitivity = 2f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    

    Vector3 velocity;
    bool isGrounded;
    float xRotation = 0f;
    float yRotation = 0f;
    InputAction movement;
    InputAction jump;
    InputAction turn;

    void Start()
    {
        movement = new InputAction("PlayerMovement");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");
        
        jump = new InputAction("PlayerJump", binding: "<Keyboard>/space");

        /*turn = new InputAction("PlayerTurn");
        turn.AddCompositeBinding("Dpad")
        .With("Left", "<Keyboard>/q")
        .With("Right", "<Keyboard>/e");*/

        movement.Enable();
        jump.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        #region Translation
        float x;
        float z;

        var delta = movement.ReadValue<Vector2>();
        x = delta.x;
        z = delta.y;

        bool jumpPressed = Mathf.Approximately(jump.ReadValue<float>(), 1);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);



        controller.Move(gravity * Time.deltaTime*transform.up);

        #endregion

        #region Rotation
        float mouseX = 0, mouseY = 0;

        if (Mouse.current != null)
        {
            var deltaMouse = Mouse.current.delta.ReadValue();
            mouseX += deltaMouse.x;
            mouseY += deltaMouse.y;
        }

        mouseX *= turnSensitivity * Time.deltaTime;
        mouseY *= turnSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;


        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);


        //transform.Rotate(Vector3.up * mouseX);
        /*
                Vector2 turnVector = turn.ReadValue<Vector2>();
                Debug.Log("Turn vector = "+turnVector.ToString());
                transform.localRotation = Quaternion.Euler(0f,turnVector.y*turnSensitivity*Time.deltaTime , 0f);*/

        #endregion
    }
}
