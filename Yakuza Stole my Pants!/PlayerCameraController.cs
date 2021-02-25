using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraController : MonoBehaviour
{
    /* Camera Controller Iteration 1:
     * Type of: 3rd Person RTS
     * Comments: Messy but functional
     */

    #region Dependencies
    [Header("Dependencies")]
    Player setPlayer;
    public Transform Player;
    public Transform Pivot;

    [Header("Cinemachine Cameras")]
    public GameObject moveCamera;
    public GameObject aimCamera;
    public GameObject transitionCamera;
    public GameObject reticle;
    #endregion

    #region Camera Variables
    [Header("Camera Variables")]
    private float mouseY;
    private float mouseX;
    private float xRotation;
    private float yRotation;
    public float LookSpeed = 300f;
    public float ZoomSpeed = 3f;
    #endregion

    #region Start & Update
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCameraAngle();
        ChangeCameraZoom();
        ChangeCameraState();
    }
    #endregion

    #region Camera

     void ChangeCameraAngle()
     {
      //mouseY = Input.GetAxis("Mouse Y") * LookSpeed * Time.deltaTime;
      mouseX = Input.GetAxis("Mouse X") * LookSpeed * Time.deltaTime;

        Pivot.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        Player.Rotate(Vector3.up * mouseX);

        }

    void ChangeCameraState()
    {
        if (Input.GetMouseButton(1))
        {
            moveCamera.SetActive(false);
            aimCamera.SetActive(true);
            Player.GetComponent<Player>().Speed = GameManager.playerSpeed / 10;
            
        }

        else if (Input.GetMouseButtonUp(1))
        {
            moveCamera.SetActive(true);
            aimCamera.SetActive(false);
            Player.GetComponent<Player>().Speed = GameManager.playerSpeed;
            reticle.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            moveCamera.SetActive(false);
            aimCamera.SetActive(false);
            transitionCamera.SetActive(true);
        }

        if(aimCamera.activeInHierarchy) reticle.SetActive(true);
    }

    #region Camera Zoom
    void ChangeCameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Camera>().fieldOfView--;
            //transform.position = new Vector3(transform.position.x, transform.position.y - .6f, transform.position.z + .2f);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Camera>().fieldOfView++;
            //transform.position = new Vector3(transform.position.x, transform.position.y + .6f, transform.position.z - .2f);
        }
    }
    #endregion
    #endregion
}
