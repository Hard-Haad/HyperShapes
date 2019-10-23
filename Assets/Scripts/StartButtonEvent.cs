using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonEvent : MonoBehaviour {

    public GameObject startButton;

    public void EnableStartButton()
    {
        startButton.SetActive(true);
    }
}
