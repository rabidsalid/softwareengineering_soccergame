using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject managerObject = new GameObject("GameManager");
                _instance = managerObject.AddComponent<GameManager>();
                DontDestroyOnLoad(managerObject);
            }
            return _instance;
        }
    }


    public int TeamoneScore { get { return _teamoneScore; } set { _teamoneScore = value; } }
    public int TeamtwoScore { get { return _teamtwoScore; } set { _teamtwoScore = value; } }
    public GameState CurrentGameState { get { return (GameState)_gameState; } set { _gameState = (int)value; } }
    public int ArrowCount { get { return _arrowCount; } set { _arrowCount = value; } }
    public delegate void ReleaseArrowDelegate();
    public ReleaseArrowDelegate releaseArrows;
    public int reqWins = 2;

    private int _gameState;
    public int _teamoneScore;
    public int _teamtwoScore;
    private int _arrowCount;
    private ScoreController _scoreController;
    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        CurrentGameState = GameState.PlayerOneTurn;
        _scoreController = GameObject.Find("Score").GetComponent<ScoreController>();

    }

    public void endTurn() {
        if (ArrowCount == 3) {
            releaseArrows();
            releaseArrows = null;
            if (CurrentGameState == GameState.PlayerOneTurn) {
                CurrentGameState = GameState.PlayerTwoTurn;
            } else if (CurrentGameState == GameState.PlayerTwoTurn) {
                CurrentGameState = GameState.PlayerOneTurn;
            }
        }
    }

    public void resetRound(int whoScored) 
    {
        SceneManager.LoadScene("Game");
        if (whoScored == 1) {
            TeamtwoScore++;
            CurrentGameState = GameState.PlayerOneTurn;
        } else if (whoScored == 0) {
            TeamoneScore++;
            CurrentGameState = GameState.PlayerTwoTurn;
        }

        if (TeamoneScore == reqWins || TeamtwoScore == reqWins) {
            SceneManager.LoadScene("GameOver");
            TeamoneScore = 0;
            TeamtwoScore = 0;
        }

    }
}
