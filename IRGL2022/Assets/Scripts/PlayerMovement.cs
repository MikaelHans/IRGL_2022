using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public CharacterController controller;
    public float speed = 12.0f;
    public float sprintModifier = 1.5f;
    public float gravity = -9.81f;
    public float crouchModifier = 0.5f;
    public float walkingHeight = 2f;
    public float crouchingHeight = 0.5f;
    public float walkCamera = 0.4f;
    public float crouchCamera = 0.4f;
    public GameObject body;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 3f;


    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;
    public bool isCrouching;

    public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsObstacle;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            isSprinting = Input.GetKey(KeyCode.LeftShift) && !isCrouching;

            if (Input.GetKey(KeyCode.LeftControl) && !isSprinting)
            {
                isCrouching = true;
            }
            else
            {
                if (isCrouching && Physics.Raycast(controller.transform.position, controller.transform.up, out rayHit, walkingHeight - crouchingHeight, whatIsObstacle))
                {
                    isCrouching = true;
                    isSprinting = false;
                }
                else
                {
                    isCrouching = false;
                }
            }

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (isSprinting)
                move *= sprintModifier;

            if (isCrouching)
            {
                move *= crouchModifier;
                controller.height = crouchingHeight;
                body.transform.localScale = new Vector3(body.transform.localScale.x, (crouchingHeight / walkingHeight), body.transform.localScale.z);
                fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, crouchCamera, fpsCam.transform.localPosition.z);
            }
            else
            {
                controller.height = walkingHeight;
                body.transform.localScale = new Vector3(body.transform.localScale.x, 1, body.transform.localScale.z);
                fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, walkCamera, fpsCam.transform.localPosition.z);
            }

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}
