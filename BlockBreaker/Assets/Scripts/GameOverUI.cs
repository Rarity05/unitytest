using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        enabled = false;
    }

    public void OnStartAgain()
    {
        LevelManager.shared.OnGameStart();
    }
}
