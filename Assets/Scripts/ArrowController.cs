using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
public GameObject arrowPrefab;
    public float maxArrowDistance = 5f;
    public float maxForce = 10f;
    public int team = 0; // 1 for red 0 for orange

    private GameObject currentArrow;
    private Vector3 arrowStartPos;
    private bool arrowActive = false;
    private GameManager _gameManager;
    private SpriteRenderer _spriteRenderer;

    void Update()
    {
        HandleInput();
    }

    void Awake()
    {
        _gameManager = GameManager.Instance;
        if (team == 1) {
            _spriteRenderer = GetComponent<SpriteRenderer>();
                _spriteRenderer.color = Color.red;
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if ((_gameManager.CurrentGameState == GameState.PlayerOneTurn && team == 0) || _gameManager.CurrentGameState == GameState.PlayerTwoTurn && team == 1)
            {
                if (IsMouseOverPlayer())
                {
                    if (!arrowActive && _gameManager.ArrowCount < 3)
                    {
                        StartCoroutine(DrawArrowCoroutine());
                    }
                    else if (arrowActive)
                    {
                        Destroy(currentArrow);
                        arrowActive = false;
                        _gameManager.ArrowCount--;
                        _gameManager.releaseArrows -= ReleaseArrow;

                    }
                }
            }
        }
    }

    bool IsMouseOverPlayer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);
        return hitCollider != null && hitCollider.gameObject == gameObject;
    }

    IEnumerator DrawArrowCoroutine()
    {
        arrowActive = true;
        arrowStartPos = transform.position;
        currentArrow = Instantiate(arrowPrefab, arrowStartPos, Quaternion.identity);

        _gameManager.ArrowCount++;
        _gameManager.releaseArrows += ReleaseArrow;
        
        while (Input.GetMouseButton(0))
        {
            UpdateArrow();
            yield return null;
        }
    }

    void UpdateArrow()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - arrowStartPos;

        if (direction.magnitude > 1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            currentArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            float clampedDistance = Mathf.Clamp(direction.magnitude, 1f, maxArrowDistance);
            currentArrow.transform.localScale = new Vector3(1f, clampedDistance * 0.2f, 1f);
        }

    }

    void ReleaseArrow()
    {
        arrowActive = false;

        float angle = Mathf.Atan2(currentArrow.transform.up.y, currentArrow.transform.up.x) * Mathf.Rad2Deg + 90;
        float clampedDistance = Mathf.Clamp(currentArrow.transform.localScale.y / 0.2f, 1f, maxArrowDistance);

        Vector3 forceDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;

        float force = clampedDistance / maxArrowDistance * maxForce;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forceDirection * force, ForceMode2D.Impulse);

        _gameManager.ArrowCount--;

        Destroy(currentArrow);
    }
}
