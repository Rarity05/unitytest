using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    struct HoldingBall
    {
        public Ball ball;
        public Vector3 velocity;

        public HoldingBall(Ball ball, Vector3 velocity)
        {
            this.ball = ball;
            this.velocity = velocity;
        }
    }

    private List<HoldingBall> holdingBalls = new List<HoldingBall>();
    private float xmin;
    private float xmax;

    // Use this for initialization
    void Start()
    {
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Bounds colliderBounds = GetComponent<Collider2D>().bounds;

        float size = colliderBounds.size.x;
        xmin = leftmost.x + size / 2;
        xmax = rightmost.x - size / 2;

        Ball startingBall = FindObjectOfType<Ball>() as Ball;
        startingBall.transform.position = new Vector3(colliderBounds.center.x + colliderBounds.extents.x / 2, colliderBounds.max.y);
        // TODO: let the collision script calculate this vector.
        Vector3 startingVelocity = new Vector3(5, 3, 0);
        holdingBalls.Add(new HoldingBall(startingBall, startingVelocity));
    }

    // Update is called once per frame
    void Update()
    {
        float cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float newPaddleX = Mathf.Clamp(cursorPos, xmin, xmax);
        float oldPaddleX = transform.position.x;

        // Move the Paddle.
        transform.position = new Vector3(newPaddleX, transform.position.y);

        // Move all the Balls that are currently held by the Paddle.
        foreach (HoldingBall holdingBall in holdingBalls)
        {
            Ball ball = holdingBall.ball;
            float newBallX = ball.transform.position.x + (newPaddleX - oldPaddleX);
            ball.transform.position = new Vector3(newBallX, ball.transform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }

        ContactPoint2D[] contacts = new ContactPoint2D[2];
        int numberOfContatcts = collision.GetContacts(contacts);
        if (numberOfContatcts == 0)
        {
            return;
        }

        ContactPoint2D contact = contacts[0];
        float extent = contact.collider.bounds.extents.x;
        float center = contact.collider.bounds.center.x;
        float contactPoint = contact.point.x;

        float linearParameter;
        if (contactPoint <= center)
        {
            linearParameter = Mathf.InverseLerp(center - extent, center, contactPoint) - 1;
        }
        else
        {
            linearParameter = Mathf.InverseLerp(center, center + extent, contactPoint);
        }

        float directionX = linearParameter * 10;
        float directionY = (1 - Mathf.Abs(linearParameter)) * 10;
        if (directionY < 1)
        {
            directionY = 1;
        }

        Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector3 oldVelocity = ball.velocity;
        Vector3 newVelocity = new Vector3(directionX, directionY).normalized * oldVelocity.magnitude;
        ball.velocity = newVelocity;
    }

    void CollisionWithPlayer(ContactPoint2D contact)
    {
        
    }
}
