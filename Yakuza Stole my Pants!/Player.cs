using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    //! Change movement to reflect euler angles
    //+ Changed movement to reflect euler agles
    //! Fix "flying player" bug
    //! Add Jump
    //+ Added Jump mechanic

    /* Introduction Text:
     * Character Controller: Yakuza
     * Type of: Character Controller
     * Comments: Improved upon the base character controller iteration.
     */

    #region Dependencies & Variables
    [Space]
    [Header("Player's Variables")]
    public float Speed = 10f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public GameObject RightWing;
    public GameObject LeftWing;
    private Vector3 velocity;
    [SerializeField] private bool IsGrounded;

    [Header("Dependencies")]
    public AudioManager audioManager;
    private CharacterController CharControl;

    [Header("Player Stats")]
    public static float CurrentHealth;

    //Components
    Animator anim;

    //Audio
    public AudioClip[] sounds = new AudioClip[3];
    public AudioSource[] audiomanagerAudioSource;
    /// <summary>
    /// Sound Effects List:
    /// 0: Eagle Jump sound effect
    /// 1: Shoot sound effect
    /// 2: Player Step 1
    /// </summary>
    #endregion

    #region Start & Update
    private void Awake()
    {
        GameManager.player = gameObject;
        CurrentHealth = GameManager.maxPlayerHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        audiomanagerAudioSource = audioManager.GetComponents<AudioSource>();
        CharControl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        CalculatePlayerStats();
        if (Input.GetKeyDown(KeyCode.Minus)) CurrentHealth -= 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard")
        {
            CurrentHealth = 0;
            DeathCheck();
        }
    }

    #endregion

    void ProcessInputs()
    {
        GroundCheck();
        MovePlayer();
        Jump();
        DeathCheck();
    }

    #region Player Stats
    
    void CalculatePlayerStats()
    {
        Mathf.Clamp(CurrentHealth, 0, GameManager.maxPlayerHealth);
    }

    void DeathCheck()
    {
        if (CurrentHealth == 0)
        {
            Debug.Log("Dead");
            GetComponent<Animator>().SetTrigger("death");
        }
    }
    #endregion

    #region Player Movement
    void MovePlayer()
    {
        Vector3 MoveDir;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("BlendX", x);
        anim.SetFloat("BlendY", z);

        MoveDir = transform.right * x + transform.forward * z;
        CharControl.Move(MoveDir * Speed * Time.deltaTime);

        /// Title: Gravity Calc
        /// Description: Calculates how much the player is affected by gravity and passes it to the Character Controller method for moving the player.
        velocity.y += GameManager.gravity * Time.deltaTime;
        CharControl.Move(velocity * Time.deltaTime);
    }

    #endregion

    #region Jump
    void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            RightWing.SetActive(false);
            LeftWing.SetActive(false);
        }
    }

    void Jump()
    {   
        if (IsGrounded)
        {
            anim.SetBool("is_Jumping", false);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {

            RightWing.SetActive(true);
            LeftWing.SetActive(true);

            //Visual Feedback
            anim.SetBool("is_Jumping", true);

            //Physics
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * GameManager.gravity);
            
            //Audio
            audioManager.GetComponent<AudioManager>().PlaySoundEffect(sounds[0]);
        }
    }
    #endregion

}
