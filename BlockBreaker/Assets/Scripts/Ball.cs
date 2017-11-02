using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int maxSpeed = 5;
    public float speedIncrease = 1.15f;
    public int speedIncreaseDelay = 5;

    private Rigidbody2D rigidBody;
    private Vector3 velocity;
    private int currentSpeed = 1;
    
    private int hitCounter = 0;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        velocity = rigidBody.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
        Vector3 contactNormal = contact.normal;

        velocity = Vector3.Reflect(velocity, contactNormal);
        rigidBody.velocity = velocity;
    }

}
