using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public CharacterController controller;
    public float speed = 12.0f;
    public float sprintModifier = 1.5f;
    public float gravity = -9.81f;
    public float crouchModifier = 0.5f;
    public float currentModifier = 1f;
    public float maxModifier = 1f;

    public float walkingHeight = 2f;
    public float crouchingHeight = 0.5f;
    public float terminalVelocity = 20f;
    public float walkCamera = 0.4f;
    public float crouchCamera = -0.6f;
    public float defaultCamera = 0f;
    public float applyCamera = 1f;
    public GameObject body;

    public Transform groundCheck;
    public float groundDistance = 0.01f;
    public LayerMask groundMask;

    public float jumpHeight = 1f;


    Vector3 velocity;
    public bool isGrounded;
    public bool isFalling;
    public bool isJumping;
    public bool isSprinting;
    public bool isCrouching;

    public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsObstacle;
    Animator animator;

    bool sprintKeyPressed = false;
    bool jumpKeyPressed = false;
    bool crouchKeyPressed = false;
    float moveHorizontal = 0;
    float moveVertical = 0;

    Vector2 moveSmoothing = new Vector2(0, 0);


    private void Awake()
    {
        animator = GetComponent<Player>().animator;
        defaultCamera = fpsCam.transform.localPosition.y;
        crouchCamera = defaultCamera - crouchCamera;
    }

    public void Sprint()
    {
        sprintKeyPressed = true;
    }

    public void Jump()
    {
        jumpKeyPressed = true;
    }

    public void Crouch()
    {
        crouchKeyPressed = true;
    }

    public void MoveHorizontal(float value)
    {
        moveHorizontal = value;
    }

    public void MoveVertical(float value)
    {
        moveVertical = value;
    }


    public void ResetKeys()
    {
        sprintKeyPressed = false;
        jumpKeyPressed = false;
        crouchKeyPressed = false;
        moveHorizontal = 0;
        moveVertical = 0;
    }


    void Update()
    {
        if (photonView.IsMine)
        {
            // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // lek glem spiderman
            isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
            isFalling = !isGrounded;

            float x = moveHorizontal;
            float z = moveVertical;

            Vector3 move = transform.right.normalized * x + transform.forward.normalized * z;

            currentModifier = 1f;
            maxModifier = sprintModifier;

            if (sprintKeyPressed)
            {
                move *= sprintModifier;
                currentModifier = sprintModifier;

            }

            if (crouchKeyPressed && !sprintKeyPressed)
            {
                isCrouching = true;
                move *= crouchModifier;
                applyCamera += (crouchCamera - applyCamera) * Time.deltaTime * 10;
                currentModifier = crouchModifier;
                maxModifier = crouchModifier;
            }
            else
            {
                isCrouching = false;
                applyCamera += (defaultCamera - applyCamera) * Time.deltaTime * 10;
            }

            if (jumpKeyPressed && isGrounded)
            {
                isJumping = true;
                isGrounded = false;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, applyCamera, fpsCam.transform.localPosition.z);

            velocity.y += gravity * Time.deltaTime;

            if (isGrounded)
            {
                isJumping = false;
                velocity.y = 0;
            }

            move.y = velocity.y;

            float normalizedModifier = (currentModifier / maxModifier);

            moveSmoothing.x += ((x * normalizedModifier) - moveSmoothing.x) * Time.deltaTime * 10;
            moveSmoothing.y += ((z * normalizedModifier) - moveSmoothing.y) * Time.deltaTime * 10;

            //Debug.Log(moveSmoothing);
            //Debug.Log(-velocity.y / terminalVelocity);

            animator.SetBool("IsJumping", isJumping);
            animator.SetBool("IsFalling", isFalling);
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetBool("IsCrouching", isCrouching);


            animator.SetFloat("MoveX", moveSmoothing.x);
            animator.SetFloat("MoveZ", moveSmoothing.y);
            animator.SetFloat("VelocityY", -velocity.y / terminalVelocity);

            controller.Move(move * speed * Time.deltaTime);

            ResetKeys();
        }
    }



    // Update is called once per frame
    // void Update()
    // {
    //     if (photonView.IsMine)
    //     {
    //         isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    //         isSprinting = Input.GetKey(KeyCode.LeftShift) && !isCrouching;

    //         if (Input.GetKey(KeyCode.LeftControl) && !isSprinting)
    //         {
    //             isCrouching = true;
    //         }
    //         else
    //         {
    //             if (isCrouching && Physics.Raycast(controller.transform.position, controller.transform.up, out rayHit, walkingHeight - crouchingHeight, whatIsObstacle))
    //             {
    //                 isCrouching = true;
    //                 isSprinting = false;
    //             }
    //             else
    //             {
    //                 isCrouching = false;
    //             }
    //         }

    //         if (isGrounded && velocity.y < 0)
    //         {
    //             velocity.y = -2f;
    //         }

    //         float x = Input.GetAxis("Horizontal");
    //         float z = Input.GetAxis("Vertical");

    //         Vector3 move = transform.right * x + transform.forward * z;

    //         if(x != 0 || z != 0)
    //         {
    //             animator.SetBool("Idle", false);
    //         }
    //         else
    //         {
    //             animator.SetBool("Idle", true);
    //         }

    //         if (isSprinting)
    //         {
    //             move *= sprintModifier;
    //             animator.SetBool("IsRunning", true);
    //         }
    //         else
    //         {
    //             animator.SetBool("IsRunning", false);
    //         }


    //         if (isCrouching)
    //         {
    //             move *= crouchModifier;
    //             controller.height = crouchingHeight;
    //             body.transform.localScale = new Vector3(body.transform.localScale.x, (crouchingHeight / walkingHeight), body.transform.localScale.z);
    //             fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, crouchCamera, fpsCam.transform.localPosition.z);
    //         }
    //         else
    //         {
    //             //controller.height = walkingHeight;
    //             body.transform.localScale = new Vector3(body.transform.localScale.x, body.transform.localScale.y, body.transform.localScale.z);
    //             //fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, walkCamera, fpsCam.transform.localPosition.z);
    //         }

    //         controller.Move(move * speed * Time.deltaTime);

    //         if (Input.GetButtonDown("Jump") && isGrounded)
    //         {
    //             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //         }

    //         velocity.y += gravity * Time.deltaTime;

    //         controller.Move(velocity * Time.deltaTime);
    //     }
    // }



}
