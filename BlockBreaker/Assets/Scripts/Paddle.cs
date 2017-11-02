using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Ball ball;

    private float xmin;
    private float xmax;

    // Use this for initialization
    void Start()
    {
        ball = FindObjectOfType<Ball>() as Ball;

        // float cameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float size = GetComponent<Collider2D>().bounds.size.x;
        xmin = leftmost.x + size / 2;
        xmax = rightmost.x - size / 2;

        ball.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float x = Mathf.Clamp(cursorPos, xmin, xmax);
        transform.position = new Vector3(x, transform.position.y);
    }
}
