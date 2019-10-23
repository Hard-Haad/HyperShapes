using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;

    [SerializeField]
    float goBackToPlayerTimeDelay;
    [SerializeField]
    float timeToLerp;
    bool gameStarted;

    private void Start()
    {
        GameManager.gameStartedEvent += OnGameStart;
        PlayerController.playerDeathEvent += OnGameOver;
    }

    void FixedUpdate () {
        if (gameStarted)
        {
            if (player != null)
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y + 2f, transform.position.z), Time.deltaTime * timeToLerp);
        }
    }

    void OnGameStart()
    {
        gameStarted = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnGameOver()
    {
        gameStarted = false;
        Invoke("GoBackToPlayer", goBackToPlayerTimeDelay);
    }

    void GoBackToPlayer()
    {
        gameStarted = true;
    }

}
