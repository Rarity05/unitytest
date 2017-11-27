using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        LevelManager.shared.AddBrick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LevelManager.shared.RemoveBrick();
        Destroy(gameObject, 0.05f);
    }
}
