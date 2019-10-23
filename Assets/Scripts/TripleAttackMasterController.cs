using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleAttackMasterController : MonoBehaviour {

    public TripleAttackController[] tripleAttackControllers;

    Queue<TripleAttackController> tripleAttackControllerQ;
    TripleAttackController currentTripleAttackController;

    void Start()
    {
        tripleAttackControllerQ = new Queue<TripleAttackController>();
        foreach (TripleAttackController tripleAttackController in tripleAttackControllers)
        {
            tripleAttackControllerQ.Enqueue(tripleAttackController);
        }

        InvokeRepeating("StartRandomCollisions", 0.0f, 0.25f);
    }

    void StartRandomCollisions()
    {
        if(tripleAttackControllerQ.Count != 0)
        {
            currentTripleAttackController = tripleAttackControllerQ.Dequeue();
            currentTripleAttackController.StartCollisions();
        }
        else
        {
            CancelInvoke("StartRandomCollisions");
        }
        
    }
    
    public void ResetObstacle()
    {
        foreach(TripleAttackController tripleAttackController in tripleAttackControllers)
        {
            tripleAttackController.ResetObstacle();
        }
    }

    public void StartObstacleCollisions()
    {
        foreach (TripleAttackController tripleAttackController in tripleAttackControllers)
        {
            tripleAttackController.ResetCollision();
        }
    }

}
