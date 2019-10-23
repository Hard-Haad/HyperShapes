using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public delegate void PlayerDeath();
    public static event PlayerDeath playerDeathEvent;

    public GameObject deathEffectObject;
    public GameObject deathEffect;
    public GameObject jetEmissionEffect;
    public Transform playerStartPoint;

    Rigidbody2D rb;

    [SerializeField]
    float addedForce;
    [SerializeField]
    float timeToSlow;
    [SerializeField]
    float timeToSpeedUp;

    float originalForce;
    bool gameStarted;
    bool collidedOnce;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        originalForce = addedForce;
        GameManager.gameStartedEvent += OnGameStart;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateInputUpwardMotion();
	}

    void UpdateInputUpwardMotion()
    {
        if (Input.GetMouseButton(0))
        {
            addedForce = Mathf.Lerp(addedForce, 0, Time.deltaTime * timeToSlow);
        }
        else
        {
            addedForce = Mathf.Lerp(addedForce, originalForce, Time.deltaTime * timeToSpeedUp);
        }
        if (gameStarted)
        {
            rb.velocity = transform.up * addedForce;
            jetEmissionEffect.SetActive(true);
        }
    }

    void OnGameStart()
    {
        gameStarted = true;
        collidedOnce = false;
    }

    void OnGameOver()
    {
        rb.velocity = Vector3.zero;
        gameStarted = false;
        jetEmissionEffect.SetActive(false);
        gameObject.SetActive(false);
        //transform.position = playerStartPoint.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collidedOnce)
        {
            Handheld.Vibrate();
            Instantiate(deathEffectObject, transform.position, Quaternion.identity);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            OnGameOver();
            //playerDeathEvent();
            collidedOnce = true;
        }
    }
}
