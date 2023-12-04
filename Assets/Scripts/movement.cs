using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour   
{
    public float speed = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-speed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
