using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int TeamoneScore { get { return _teamoneScore; } set { _teamoneScore = value; } }
    public int TeamtwoScore { get { return _teamtwoScore; } set { _teamtwoScore = value; } }
    public GameState CurrentGameState { get { return (GameState)_gameState; } set { _gameState = (int)value; } }
    public int ArrowCount { get { return _arrowCount; } set { _arrowCount = value; } }
    public delegate void ReleaseArrowDelegate();
    public ReleaseArrowDelegate releaseArrows;

    private int _gameState;
    private int _teamoneScore;
    private int _teamtwoScore;
    private int _arrowCount;
    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public void endTurn() {
        if (ArrowCount == 3) {
            releaseArrows();
        }
    }
}
