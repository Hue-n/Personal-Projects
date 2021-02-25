using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Title: Player Controller Iteration 3
    /// Type: Character Controller
    /// Comments: Updated to reflect the Input Processing, Input Calculation structure.
    /// Developed more to include a "States" section.
    /// </summary>

    [Header("Player Variables")]
    private CharacterController Characon;
    public int CurrentHealth;

    [Header("Jump Dependencies and Variables")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public bool isGrounded;
    private Vector3 velocity;

    [Header("Player Move and Run Variables")]
    [Range(0, 50)] public float Speed = 20f;
    public bool isSprinting;
    public float maxSprint = 50f;
    public float currentSprint;
    public bool canSprint;

    #region Start & Update
    // Start is called before the first frame update
    void Awake()
    {
        canSprint = true;
        CurrentHealth = GameManager.maxPlayerHealth;
        currentSprint = maxSprint;

        Characon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        GroundCheck();
    }
    #endregion

    #region Input Processing
    void ProcessInputs()
    {
        //Movement
        float playerX = Input.GetAxis("Horizontal");
        float playerY = Input.GetAxis("Vertical");
        Move(playerX, playerY);

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
        }

        Sprint(isSprinting);
    }
    #endregion

    #region Input Calculation
    void Move(float playerX, float playerY)
    {
        Vector3 moveDir = transform.right * playerX + transform.forward * playerY;

        velocity.y += GameManager.gravity * Time.deltaTime;
        Characon.Move(moveDir * Speed * Time.deltaTime);
        Characon.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region State Methods
    void Sprint(bool isSprinting)
    {
        if (isSprinting && canSprint)
        {
            Speed = 20;
        }

        if (!isSprinting)
        {
            Speed = 10;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {

        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Physics
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * GameManager.gravity);
        }

        #endregion
    } 
}
