using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager shared;

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
        SceneManager.LoadScene("level_01");
    }

}
