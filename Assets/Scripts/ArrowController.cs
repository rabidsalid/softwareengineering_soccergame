using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
public GameObject arrowPrefab;
    public float maxArrowDistance = 5f;
    public float maxForce = 10f;

    private GameObject currentArrow;
    private Vector3 arrowStartPos;
    private bool arrowActive = false;
    private GameManager _gameManager;

    void Update()
    {
        HandleInput();
    }

    void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverPlayer())
            {
                if (!arrowActive && _gameManager.ArrowCount < 3)
                {
                    StartDrawingArrow();
                }
            }
        }

        if (Input.GetMouseButton(0) && arrowActive)
        {
            UpdateArrow();
        }

        if (Input.GetMouseButtonUp(0) && arrowActive)
        {
            ReleaseArrow();
        }
    }

    bool IsMouseOverPlayer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);
        return hitCollider != null && hitCollider.gameObject == gameObject;
    }

    void StartDrawingArrow()
    {
        arrowActive = true;
        arrowStartPos = transform.position;
        currentArrow = Instantiate(arrowPrefab, arrowStartPos, Quaternion.identity);

        _gameManager.ArrowCount++;
    }

    void UpdateArrow()
    {
   Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // Calculate the direction from the center of the object to the mouse cursor
    Vector3 direction = mousePos - arrowStartPos;

    // Calculate the angle between the arrow and the direction to the cursor
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

    // Set the arrow's rotation to match the direction to the cursor
    currentArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    // Clamp the arrow's length to maxArrowDistance
    float clampedDistance = Mathf.Clamp(direction.magnitude, 1f, maxArrowDistance);

    // Set the arrow's scale based on clamped distance, scaling perpendicularly
    currentArrow.transform.localScale = new Vector3(1f, clampedDistance * 0.3f, 1f);
    }

    void ReleaseArrow()
    {
        arrowActive = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector3.Distance(arrowStartPos, mousePos);

        float force = Mathf.Clamp(distance, 0f, maxArrowDistance) / maxArrowDistance * maxForce;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce((mousePos - arrowStartPos).normalized * force, ForceMode2D.Impulse);

        _gameManager.ArrowCount--;

        Destroy(currentArrow);
    }
}
