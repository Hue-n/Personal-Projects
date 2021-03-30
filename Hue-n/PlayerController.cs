using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool slammed = false;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    #region Get Input
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    public void OnSlam(InputAction.CallbackContext context)
    {
        slammed = context.action.triggered;
    }
    #endregion

    #region Process Input
    void ProcessMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    void ProcessJump()
    {
        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void ProcessSlam()
    {
        if (slammed && !groundedPlayer)
        {
            playerVelocity.y += -1 * jumpHeight * -3.0f * gravityValue;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    #endregion

    #region Update & Output
    void Update()
    {
        ProcessMovement();
        ProcessJump();
        ProcessSlam();
    }
    #endregion
}
