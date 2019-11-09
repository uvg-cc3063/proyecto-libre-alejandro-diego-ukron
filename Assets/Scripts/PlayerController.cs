using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public float bounceForce = 8f;

    private Vector3 moveDirection;

    public CharacterController charController;

    private Camera theCam;

    public GameObject playerModel;
    public float rotateSpeed;

    public Animator anim;

    public int JumpSound;

    //Knocking the player back
    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;

    public bool stopMove;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main; //gets the main camera in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking && !stopMove)
        {
            float yStore = moveDirection.y; //8. Adding Gravity        
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); //GetAxisRaw = gets what's been pressed at the moment
            //12. Moving based on camera rotation. Our vertical input (z.axis) is going to control wether we are facing forward or not.
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) +
                            (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize(); //make the speed of movement the same all the time.
            moveDirection =
                moveDirection * moveSpeed; //This multiplication makes moveDirection changes its symbol "-" or "+"
            moveDirection.y =
                yStore; //The y component of the move direction is updated so the y's position doesnt become 0 so fast.

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {
                    AudioManager.instance.PlaySfx(JumpSound);
                    moveDirection.y = jumpForce;
                }
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale; //Adding gravity

            //transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed); //Delta time is used to make the speed constant in every computer where the game is tested.
            charController.Move(moveDirection * Time.deltaTime);

            //10. Rotating the player with the camera
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                //playerModel.transform.rotation = newRotation; //This rotation is not smooth.

                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation,
                    rotateSpeed * Time.deltaTime);


            }
        }

        //THE PLAYER IS BEING KNOCKED BACK.
        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale; //Adding gravity

            charController.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale; //Adding gravity
            charController.Move(moveDirection);
        }

        anim.SetFloat("Speed",
            Mathf.Abs(moveDirection.x) +
            Mathf.Abs(moveDirection.z)); //to set the parameter of animation to the value of our "x" input or "z" input.
        anim.SetBool("Grounded", charController.isGrounded); //set true or false to de animation parameter.
    }

    public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockBackLength;
        Debug.Log("knocked back!!");
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }
}