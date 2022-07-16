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
    public bool isGrounded;
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

    private void Awake()
    {
        animator = GetComponent<Player>().animator;
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
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            isSprinting = sprintKeyPressed && !isCrouching;

            if (crouchKeyPressed && !isSprinting)
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

            float x = moveHorizontal;
            float z = moveVertical;

            Vector3 move = transform.right * x + transform.forward * z;

            if (x != 0 || z != 0)
            {
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("Idle", true);
            }

            if (isSprinting)
            {
                move *= sprintModifier;
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }


            if (isCrouching)
            {
                move *= crouchModifier;
                controller.height = crouchingHeight;
                body.transform.localScale = new Vector3(body.transform.localScale.x, (crouchingHeight / walkingHeight), body.transform.localScale.z);
                fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, crouchCamera, fpsCam.transform.localPosition.z);
            }
            else
            {
                //controller.height = walkingHeight;
                body.transform.localScale = new Vector3(body.transform.localScale.x, body.transform.localScale.y, body.transform.localScale.z);
                //fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, walkCamera, fpsCam.transform.localPosition.z);
            }

            controller.Move(move * speed * Time.deltaTime);

            if (jumpKeyPressed && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

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
