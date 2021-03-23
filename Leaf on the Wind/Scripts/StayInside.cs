using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    public float boundModifier = 8.4f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundModifier, boundModifier),
            Mathf.Clamp(transform.position.y, -10f, 10f), transform.position.z);
    }
}
