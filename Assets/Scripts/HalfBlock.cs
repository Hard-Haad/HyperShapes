using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBlock : MonoBehaviour {

    public HalfObstacleController hOC;

	void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            hOC.OnSelfCollision();
        }
    }
}
