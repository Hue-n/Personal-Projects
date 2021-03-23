using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum PlayerStates
    { 
        cutscene,
        running,
        invincible,
        jumping,
        sliding,
        crying 
    }

public class Player : MonoBehaviour
{
    [Header("Gameplay Dependencies")]
    public UIManager uiManager;

    [Header("Player Variables")]
    public int currentHealth;
    public float moveSpeed = 3f;
    public float jumpForce = 10;
    public float downForce = 20;
    public PlayerStates currentPlayerState;
    private float moveInput;
    private bool isSliding = false;

    public int currentLeavesInHand = 0;
    private int leavesNeededForLife = 1;

    public Animator anim;

    public BoxCollider2D regularColl;
    public BoxCollider2D slideColl;

    public ScoreManager scoreManager;
    public LayerMask groundLayerMask;
    public Transform groundCheck;
    
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = GameManager.maxPlayerHealth;
        scoreManager.UpdateHealth(currentHealth);

        currentPlayerState = new PlayerStates();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() //The only thing in update should be the switch statement and all that it's supposed to do for each state.
    {
        switch (currentPlayerState)
        {
            case PlayerStates.running:
                anim.SetBool("isFall", false);
                GroundCheck();
                Jump();
                Slide();
                DeathCheck();
                DevControls();
                LeavesCheck();
                ClampHealth();
                CalculateMovement();
                break;

            case PlayerStates.crying:
                Time.timeScale = 0.5f;
                break;

            case PlayerStates.sliding:
                break;
        }

    }

    public void UpdatePlayerState(PlayerStates newPlayerState)
    {
        currentPlayerState = newPlayerState;
    }

    #region Player Movement Methods
    void CalculateMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayerMask);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (!isGrounded) anim.SetBool("isJump", true);
        if (isGrounded) anim.SetBool("isJump", false);
    }

    void DeathCheck()
    {
        if (currentHealth == 0)
        {
            anim.SetBool("isFall", true);
            anim.SetBool("isSlide", false);
            anim.SetBool("isJump", false);

            currentPlayerState = PlayerStates.crying;
            uiManager.UpdateGameState(GameStates.game_over);
        }
    }

    void ClampHealth()
    {
        Mathf.Clamp(currentHealth, 0, GameManager.maxPlayerHealth);
    }

    void DevControls()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
            currentHealth--;
        scoreManager.UpdateHealth(currentHealth);
    }
    #endregion

    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.down * downForce;
            PreFromSlide();
        }
    }

    void PreFromSlide()
    {
        isSliding = true;

        anim.SetBool("isSlide", true);

        StartCoroutine(StopSlide());
    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isSlide", false);
        anim.Play("Running");
        isSliding = false;
    }

    void LeavesCheck()
    {
        if (currentLeavesInHand == leavesNeededForLife)
        {
            Debug.Log(currentLeavesInHand);
            currentHealth++;
            currentLeavesInHand = 0;
        }
    }

}
