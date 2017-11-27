using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int maxSpeed = 5;
    public float speedIncrease = 1.15f;
    public int speedIncreaseDelay = 5;

    private Rigidbody2D rigidBody;
    private Vector3 velocity = new Vector3(5, 3, 0);
    private int currentSpeed = 1;
    
    private int hitCounter = 0;
    private bool collided = false;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        collided = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided)
        {
            return;
        }
        collided = true;

        // TODO: factor out speed handling
        hitCounter += 1;
        if (hitCounter >= speedIncreaseDelay)
        {
            hitCounter = 0;
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += 1;
                velocity = velocity * speedIncrease;
            }
        }

        ContactPoint2D[] contacts = new ContactPoint2D[2];
        int numberOfContatcts = collision.GetContacts(contacts);
        if (numberOfContatcts == 0)
        {
            return;
        }

        ContactPoint2D contact = contacts[0];
        if (collision.gameObject.tag == "Player")
        {
            CollisionWithPlayer(contact);
        }
        else
        {
            CollisionWithOther(contact);
        }
    }

    void CollisionWithPlayer(ContactPoint2D contact)
    {
        if (!LevelManager.shared.started)
        {
            return;
        }

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

        Vector3 newDirection = new Vector3(directionX, directionY).normalized;
        velocity = newDirection * velocity.magnitude;
        rigidBody.velocity = velocity;
    }

    void CollisionWithOther(ContactPoint2D contact)
    {
        Vector3 contactNormal = contact.normal;

        // Check if we have a corner collision
        float absX = Mathf.Abs(contactNormal.x);
        float absY = Mathf.Abs(contactNormal.y);
        if (absX != 1 && absY != 1)
        {
            // Top - Down conversion
            if (absX <= absY)
            {
                contactNormal = (absX >= 0.5f) ? Vector3.up : Vector3.down;
            }
            // Left - Right conversion
            else
            {
                contactNormal = (absY >= 0.5f) ? Vector3.right : Vector3.left;
            }
        }

        velocity = Vector3.Reflect(velocity, contactNormal);
        rigidBody.velocity = velocity;
    }

}
