using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager shared;
    public bool started = false;
    private int brickCount = 0;

    // Use this for initialization
    void Start()
    {
        if (shared == null)
        {
            LevelManager.shared = this;
            enabled = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnGameOver()
    {
        SceneManager.LoadScene("game_over");
    }

    public void OnGameStart()
    {
        brickCount = 0;
        SceneManager.LoadScene("level_01");
    }

    public void AddBrick()
    {
        brickCount++;
    }

    public void RemoveBrick()
    {
        brickCount--;
        if (brickCount == 0)
        {
            OnGameOver();
        }
    }

}
