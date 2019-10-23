using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleAttackBlockController : MonoBehaviour {


    TripleAttackController parentTripleAttackController;

	void Start () {
        parentTripleAttackController = GetComponentInParent<TripleAttackController>();
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
        parentTripleAttackController.ResetCollision();
    }
}
