using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour {

    public GameObject onCrystalCollectEffect;

    [SerializeField]
    float rotatingSpeed;

	void FixedUpdate () {
        transform.Rotate(transform.forward * Time.deltaTime * rotatingSpeed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(onCrystalCollectEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }        
    }
}
