using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour
{
    private Rigidbody2D rb;
    public float frictionCoefficient = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForce(getFrictionalForce(), ForceMode2D.Force);
    }

    public Vector2 getFrictionalForce() {
        return -rb.velocity.normalized * frictionCoefficient * rb.mass;
    }

    // only created for testing purposes, never use for actual game functions
    public void setRigidbody(Rigidbody2D rigidbody) {
        rb = rigidbody;
    }
}
