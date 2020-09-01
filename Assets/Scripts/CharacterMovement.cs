using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;


public class CharacterMovement : MonoBehaviour
{
    public bool isControl;
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public float atkSpeed = 1.5f;
    protected float atkCountingTime = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (isControl)
        {
            if(Input.GetAxis("Horizontal") != 0 || TCKInput.GetAxis("Joystick", EAxisType.Horizontal) != 0 ||
               Input.GetAxis("Vertical") != 0 || TCKInput.GetAxis("Joystick", EAxisType.Vertical) != 0 )
            {
                GetComponent<Controller_PC>().state = "Control";
            }

            if (GetComponent<Controller_PC>().state != "MoveToTarget")
            {
                //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                Vector3 move = new Vector3(TCKInput.GetAxis("Joystick", EAxisType.Horizontal) + Input.GetAxis("Horizontal"), 0, TCKInput.GetAxis("Joystick", EAxisType.Vertical) + Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * playerSpeed);

                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                }
            }
        }

        // Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 100))
        //    {
        //        Debug.DrawLine(ray.origin, hit.point);
        //        move = hit.point;
        //        transform.position = Vector3.MoveTowards(transform.position, move, playerSpeed);
        //    }
        //}
    }
}
