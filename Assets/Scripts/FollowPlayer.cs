using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;

    [SerializeField]
    float timeToLerp;
    [SerializeField]
    float delayToResetFollowStatus;
    bool gameStarted;

    private void Start()
    {
        GameManager.gameStartedEvent += OnGameStart;
        PlayerController.playerDeathEvent += OnGameOver;
        CheckAspect();
    }

    void CheckAspect()
    {
        if (Camera.main.aspect >= 0.49f && Camera.main.aspect <= 0.55f)
        {
            Camera.main.orthographicSize = 5.5f;
            return;
        }else if(Camera.main.aspect >= 0.55f)
        {
            Camera.main.orthographicSize = 5.0f;
            return;
        }
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
        Invoke("ResetFollowStatus", delayToResetFollowStatus);
    }

    void ResetFollowStatus()
    {
        gameStarted = true;
    }

}
