using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class BackgroundScroller : MonoBehaviour
{
    public Rigidbody2D rb;

    private float length, startPos; 

    public float scrollSpeed;
    public float parallax;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScrollSpeed();

        if (transform.position.x < -length)
        {
            Vector2 resetPosition = new Vector3((length * 2f) - 1, 0);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }

    void UpdateScrollSpeed()
    {
        scrollSpeed = ScoreManager.scrollSpeed * parallax;
        rb.velocity = new Vector2(scrollSpeed, 0);
    }
}
