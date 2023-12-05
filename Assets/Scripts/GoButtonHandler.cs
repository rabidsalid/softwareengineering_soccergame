using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonHandler : MonoBehaviour
{
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameManager.Instance;
    }
    public void OnButtonClick()
    {
        _gameManager.endTurn();
    }
}
