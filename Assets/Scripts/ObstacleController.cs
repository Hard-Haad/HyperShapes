using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleController : MonoBehaviour {

    Rigidbody2D rb;
    float directionX = 1;
    
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(directionX * speed, 0, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        directionX *= -1;
    }

}
