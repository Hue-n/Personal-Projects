using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float fallSpeed = 0.01f;
    public float life = 1f;

    public float frequency = 3f;
    public float magnitude = 3f;

    // Update is called once per frame
    void Update()
    {
        Fall();
        Destroy(gameObject, life);
    }

    void Fall()
    {
        transform.Translate(new Vector2(-0.5f,-0.5f) * fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().currentLeavesInHand++;
            Destroy(gameObject);
        }
    }
}
