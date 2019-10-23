using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfObstacleController : MonoBehaviour {

    public GameObject onCollisionEffect;

    public Transform upperCollisionPoint;
    public Transform lowerCollisionPoint;

    public Rigidbody2D rbLeft;
    public Rigidbody2D rbRight;

    public Vector3 leftBlockOriginalPosition;
    public Vector3 rightBlockOriginalPosition;

    [SerializeField]
    float addedForce;
    [SerializeField]
    float delayToSelfCollision;

    public void OnSelfCollision()
    {
        rbRight.velocity = Vector3.zero;
        rbLeft.velocity = Vector3.zero;
        Instantiate(onCollisionEffect, upperCollisionPoint.position, upperCollisionPoint.rotation);
        Instantiate(onCollisionEffect, lowerCollisionPoint.position, lowerCollisionPoint.rotation);
    }

    public void ResetHalfBlockPositions()
    {
        rbRight.velocity = Vector3.zero;
        rbLeft.velocity = Vector3.zero;
        rbLeft.transform.position = new Vector3(leftBlockOriginalPosition.x,transform.position.y,transform.position.z);
        rbRight.transform.position = new Vector3(rightBlockOriginalPosition.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("InvokeSelfCollision", delayToSelfCollision);
        }
    }

    void InvokeSelfCollision()
    {
        rbLeft.AddForce(rbLeft.transform.right * addedForce, ForceMode2D.Impulse);
        rbRight.AddForce(-rbLeft.transform.right * addedForce, ForceMode2D.Impulse);
    }
}
