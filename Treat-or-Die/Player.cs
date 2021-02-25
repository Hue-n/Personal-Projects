using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gm;
    public AudioManager am;

    [Header("Player Variables")]
    public float speed = 3.0f;
    public Rigidbody2D rb;
    public Text lives;
    public int health = 3;

    public Text ammoAmountText;

     public GameObject [] spawners; //Set the amount of spawners in inspector

    //public Text scoreText;
    //public int points = 0;

    [Header("Gun Variables")]
    //public GameObject gun;
    public bool canShoot;
    public float timeTilFire;
    public float fireCooldownAmount;

    //public Rigidbody2D ammo;
    public GameObject ammo;
    public int ammoCount = 500;
    public float ammoSpeed = 100.0f;
    public Transform gunDirection;

    [Header("References")]
    public Animator animator;
    public Vector2 movementDirection;
    public Transform gunaim;
    public Text gameOver;
    public GameObject house;
    public House actualHouse;

    [Header("For Aim Direction")]
    public Vector2 look;


    // Start is called before the first frame update
    void Start()
    {
        lives.text = "Lives: " + health.ToString();
        ammoAmountText.text = "Candy: " + ammoCount.ToString(); //Display amount of amo
        canShoot = true; //Can only shoot when gun is activated
        gm.acceptingInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessLook();
        ProcessInputs();
        Animate();
        Move();

        //Candy shot cooldown
        if (canShoot == true)
        {
            timeTilFire -= Time.deltaTime;
        }

        //Escape
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        //Offer Restart Menu
        if (health == 0)
        {
            gm.acceptingInput = false;
            gameOver.text = "Game Over! \n" +
            "Try Again? \n" +
            "'F' to continue \n" +
            "'ESC' to quit \n";

            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("Backup Scene");
            }
        }

        
        //Shoot with the arrow keys
        if (timeTilFire <= 0 && canShoot == true && ammoCount != 0 && gm.acceptingInput)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                float x;
                float y;

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    x = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    x = 1;
                }
                else
                {
                    x = 0;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    y = 1;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    y = -1;
                }
                else
                {
                    y = 0;
                }
                GameObject tempAmmo = Instantiate(ammo, gunaim.position, Quaternion.identity);
                Rigidbody2D ammoRigid = tempAmmo.GetComponent<Rigidbody2D>();
                ammoRigid.velocity = Vector2.zero;
                ammoRigid.AddForce(new Vector2(x, y) * ammoSpeed, ForceMode2D.Force);

                am._as.PlayOneShot(am.playerClipArray[3]);

                ammoCount -= 1;
                Debug.Log(ammoCount);
                timeTilFire = fireCooldownAmount;
                ammoAmountText.text = "Candy: " + ammoCount.ToString(); //Display amount of ammo
            }
        }

        //Spawn spawner from a certain distance
        foreach (var s in spawners)
        {
            //Calculate distance between each spawner and the player
            float spawnDist = Vector2.Distance(this.transform.position, s.transform.position);

            Debug.Log(spawnDist);

            //Activate the spawner when the player is less than or equal to 6ft?
            if (spawnDist <= 6)
            {
                s.SetActive(true);
            }
        }
    }

    void ProcessLook()
    {
        if (gm.acceptingInput)
        { 
        //Change sprite orientation with arrow keys
        Vector2 looktemp = look;
        look = new Vector2(Input.GetAxis("x"), Input.GetAxis("y"));

        if (look != Vector2.zero)
        {
            looktemp = look;
        }

        else look = looktemp;
        }
    }

    void ProcessInputs()
    {
        if (gm.acceptingInput)
        {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        }
    }

    void Move()
    {
        if (gm.acceptingInput)
        { 
        rb.velocity = movementDirection * speed * Time.deltaTime;
        }
    }

    void Animate()
    {
        animator.SetFloat("x", look.x);
        animator.SetFloat("y", look.y);
        animator.SetInteger("health", health);

    }

}

