## Welcome to my contributions to Cavern of Stars!

### Project Description
Cavern of Stars is a prototype built by Trixelbit Arcade, the company that I am a Junior Developer for. <br />
The game is still in its early stages, but these have been my contributions so far. <br />

### Saw System
[Link to Video](https://www.youtube.com/watch?v=mxe6qlLTjE4)

#### How it works
- A base saw object is created that rotates spins the saw at a given speed.

```C#
    void Update()
    {
        if (clockwise)
        {
            transform.Rotate(Vector3.forward * -spinSpeed * Time.deltaTime);
        }

        if (!clockwise)
        {
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        }    
    }
```

- That base saw is utilized to created the other saws like the oscillating saw.

```C#
//Sets Point A and Point B to the transforms of user-defined gameobjects.
   void Start()
    {
        Vector3[] LinePositions = { LinePointA.position, LinePointB.position };
        LineRend.SetPositions(LinePositions);
        nextPos = IsAStartingPoint? posA.position : posB.position; //startPos.position;
        transform.position = posA.position;
    }

//Oscillates the saw between those two points.
    // Update is called once per frame
    void Update()
    {
        if (transform.position == posA.position)
        {
            nextPos = posB.position;
        }

        if (transform.position == posB.position)
        {
            nextPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
```

### Bullet System
[Link to Video](https://www.youtube.com/watch?v=a91tjGwjt8g)

####How it works
- Takes a base bullet class
```C#
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Title: Bullet Iteration 1
    /// Type: Bullet
    /// Description: A bullet prefab that can take in direction and speed from an outside
    /// variable and utilize it to shoot it in a direction specified.
    /// Comments: thank you, Marco.
    /// </summary>
    /// 

    #region Public Variables

    [Header("Bullet Variables")]
    [Range(0,1000)] public float Speed = 5f;
    [Range(0,5)] public float LifeSpan = 1f;
    public Vector3 Rotation;

    #endregion

    #region Orient the bullet and move it.

    private void Start()
    {
        transform.rotation = Quaternion.Euler(Rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // move with given velocity
        transform.Translate(Speed * Vector3.forward, Space.Self);
        Destroy(gameObject, LifeSpan);
    }
    #endregion
```
- And includes a function to be able to dynamically change its speed and direction to achieve different bullet patterns later.
```C#
    public void UpdateVelocity(float speed, Vector3 rotation)
    {
        // update the values for speed and rotation
        transform.rotation = Quaternion.Euler(rotation);
        Speed = speed;
        Rotation = rotation;
    }
```

### Minimap System
[Link to Video](https://youtu.be/MbOs4Dmgps8)
