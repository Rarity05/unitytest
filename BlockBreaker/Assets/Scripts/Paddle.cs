using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float xmin;
    private float xmax;

    private Ball ball;

    // Use this for initialization
    void Start()
    {
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Bounds colliderBounds = GetComponent<Collider2D>().bounds;

        float size = colliderBounds.size.x;
        xmin = leftmost.x + size / 2;
        xmax = rightmost.x - size / 2;

        ball = FindObjectOfType<Ball>() as Ball;
        ball.transform.position = new Vector3(colliderBounds.center.x + colliderBounds.extents.x / 2, colliderBounds.max.y);
    }

    // Update is called once per frame
    void Update()
    {
        float cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float newPaddleX = Mathf.Clamp(cursorPos, xmin, xmax);
        float oldPaddleX = transform.position.x;

        // Move the Paddle.
        transform.position = new Vector3(newPaddleX, transform.position.y);

        if (!LevelManager.shared.started)
        {
            float newBallX = ball.transform.position.x + (newPaddleX - oldPaddleX);
            ball.transform.position = new Vector3(newBallX, ball.transform.position.y);
        }
    }

    private void OnMouseDown()
    {
        if (LevelManager.shared.started)
        {
            return;
        }

        LevelManager.shared.started = true;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 3, 0);
    }
}
