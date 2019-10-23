using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour {

    public delegate void SpawnTriggerEvent();
    public static event SpawnTriggerEvent spawnTriggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spawnTriggerEvent();
        }        
    }

}
