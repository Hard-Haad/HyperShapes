using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject[] obstacles;

    Queue<GameObject> obstaclePool;

    [SerializeField]
    int[] direction;
    [SerializeField]
    int intialObstacleCount;
    [SerializeField]
    float spawnGap;
    [SerializeField]
    Vector2 minMaxSpeedObs;
    int currentPoolThreshold = 0;
    [SerializeField]
    int maxPoolThreshold;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        obstaclePool = new Queue<GameObject>();
        CreateLevel();
        SpawnTrigger.spawnTriggerEvent += InvokePool;
        PlayerController.playerDeathEvent += OnGameOver;
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
            _obstacleInstance.GetComponentInChildren<ObstacleController>().speed = direction[Random.Range(0, direction.Length)] * Random.Range(minMaxSpeedObs.x, minMaxSpeedObs.y);
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
            _obstacleInstance.GetComponentInChildren<ObstacleController>().speed = direction[Random.Range(0, direction.Length)] * Random.Range(minMaxSpeedObs.x, minMaxSpeedObs.y);
            transform.position = new Vector3(transform.position.x, transform.position.y + spawnGap, transform.position.z);
            _obstacleInstance.transform.position = transform.position;
            obstaclePool.Enqueue(_obstacleInstance);
        }
    }

    void OnGameOver()
    {
        currentPoolThreshold = 0;
        transform.position = startPos;
        ResetPool();
    }
    
}
