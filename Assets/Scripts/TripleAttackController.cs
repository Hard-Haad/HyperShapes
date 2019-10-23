using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleAttackController : MonoBehaviour {

    public Rigidbody2D rbLeft;
    public Rigidbody2D rbRight;

    public Transform trLeft;
    public Transform trRight;

    Vector3 posLeft;
    Vector3 posRight;
    bool startCollisions;

    [SerializeField]
    float addedForce;
    [SerializeField]
    float posLerpTime;
    [SerializeField]
    float engageCollisionDelay;
    [SerializeField]
    float maxSpawnGap;
    int direction;


    void Start () {
        direction = 1;
        posLeft = trLeft.position;
        posRight = trRight.position;
    }

    void FixedUpdate()
    {
        if (startCollisions)
        {
            rbLeft.velocity = direction * rbLeft.transform.right * addedForce;
            rbRight.velocity = direction * -rbLeft.transform.right * addedForce;
        }
    }

    public void StartCollisions()
    {
        startCollisions = true;
    }

    public void ResetObstacle()
    {
        rbLeft.velocity = Vector3.zero;
        rbRight.velocity = Vector3.zero;
        direction = 1;
    }

    public void ResetCollision()
    {
        direction *= -1;
    }

}
