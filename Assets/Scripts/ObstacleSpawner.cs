using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject[] obstacles;

    Queue<GameObject> obstaclePool;

    [SerializeField]
    float delayToResetPool;
    [SerializeField]
    int[] direction;
    [SerializeField]
    int intialObstacleCount;
    [SerializeField]
    float spawnGap;
    [SerializeField]
    Vector2 minMaxSpeedObs;
    [SerializeField]
    Vector2 minMaxSpeedLimit;
    [SerializeField]
    float rateOfIncreaseOfSpeed;
    [SerializeField]
    int speedIncreaseThreshold;
    int currentPoolThreshold = 0;
    [SerializeField]
    int maxPoolThreshold;
    Vector3 startPos;
    Vector2 originalMinMaxSpeed;

    private void Start()
    {
        startPos = transform.position;
        obstaclePool = new Queue<GameObject>();
        CreateLevel();
        SpawnTrigger.spawnTriggerEvent += InvokePool;
        PlayerController.playerDeathEvent += OnGameOver;
        originalMinMaxSpeed = minMaxSpeedObs;
    }

    void CreateLevel()
    {
        for(int i=0; i<intialObstacleCount; i++)
        {
            CreateObstacle();
        }
    }

    void CreateObstacle()
    {
        GameObject _obstacleToSpawn = obstacles[Random.Range(0, obstacles.Length)];
        GameObject _obstacleInstance = (GameObject)Instantiate(_obstacleToSpawn, new Vector3(transform.position.x, transform.position.y + spawnGap, transform.position.z), Quaternion.identity);
        if(_obstacleInstance.GetComponentInChildren<ObstacleController>() != null)
            _obstacleInstance.GetComponentInChildren<ObstacleController>().speed = direction[Random.Range(0, direction.Length)] * Random.Range(minMaxSpeedObs.x, minMaxSpeedObs.y);
        transform.position = new Vector3(transform.position.x, transform.position.y + spawnGap, transform.position.z);
        obstaclePool.Enqueue(_obstacleInstance);
    }

    void InvokePool()
    {
        currentPoolThreshold++;
        if(currentPoolThreshold >= maxPoolThreshold)
        {
            GameObject _obstacleInstance = obstaclePool.Dequeue();

            if (currentPoolThreshold >= speedIncreaseThreshold)
            {
                minMaxSpeedObs.x += rateOfIncreaseOfSpeed;
                minMaxSpeedObs.y += rateOfIncreaseOfSpeed;
                minMaxSpeedObs.x = Mathf.Clamp(minMaxSpeedObs.x, 0f, minMaxSpeedLimit.x);
                minMaxSpeedObs.y = Mathf.Clamp(minMaxSpeedObs.y, 0f, minMaxSpeedLimit.y);
            }

            if (_obstacleInstance.GetComponentInChildren<ObstacleController>() != null)
                _obstacleInstance.GetComponentInChildren<ObstacleController>().speed = direction[Random.Range(0, direction.Length)] * Random.Range(minMaxSpeedObs.x, minMaxSpeedObs.y);
            else if (_obstacleInstance.GetComponentInChildren<HalfObstacleController>() != null)
                _obstacleInstance.GetComponentInChildren<HalfObstacleController>().ResetHalfBlockPositions();
            else if (_obstacleInstance.GetComponentInChildren<TripleAttackMasterController>() != null)
                _obstacleInstance.GetComponentInChildren<TripleAttackMasterController>().ResetObstacle();

            transform.position = new Vector3(transform.position.x, transform.position.y + spawnGap, transform.position.z);
            _obstacleInstance.transform.position = transform.position;

            obstaclePool.Enqueue(_obstacleInstance);
        }
    }

    void ResetPool()
    {
        for (int i = 0; i < intialObstacleCount; i++)
        {
            GameObject _obstacleInstance = obstaclePool.Dequeue();

            if (_obstacleInstance.GetComponentInChildren<ObstacleController>() != null)
                _obstacleInstance.GetComponentInChildren<ObstacleController>().speed = direction[Random.Range(0, direction.Length)] * Random.Range(minMaxSpeedObs.x, minMaxSpeedObs.y);
            else if (_obstacleInstance.GetComponentInChildren<HalfObstacleController>() != null)
                _obstacleInstance.GetComponentInChildren<HalfObstacleController>().ResetHalfBlockPositions();
            else if (_obstacleInstance.GetComponentInChildren<TripleAttackMasterController>() != null)
                _obstacleInstance.GetComponentInChildren<TripleAttackMasterController>().ResetObstacle();

            transform.position = new Vector3(transform.position.x, transform.position.y + spawnGap, transform.position.z);
            _obstacleInstance.transform.position = transform.position;

            obstaclePool.Enqueue(_obstacleInstance);
        }
    }

    void OnGameOver()
    {
        minMaxSpeedObs = originalMinMaxSpeed;
        currentPoolThreshold = 0;
        transform.position = startPos;
        Invoke("ResetPool", delayToResetPool);
    }
    
}
