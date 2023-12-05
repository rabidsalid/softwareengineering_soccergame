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

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 frictionalForce = -rb.velocity.normalized * frictionCoefficient * rb.mass;
        rb.AddForce(frictionalForce, ForceMode2D.Force);
    }
}
