using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = GameManager.Instance.TeamoneScore + " - " + GameManager.Instance.TeamtwoScore;
    }
}
