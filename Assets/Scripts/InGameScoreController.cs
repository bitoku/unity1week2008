using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScoreController : MonoBehaviour
{
    private GameManager _gameManager;
    private Text _text;
    private int _displayedScore;
    private float _elapsedTimeAfterChange;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _text = GetComponent<Text>();
        _displayedScore = 0;
    }
    // Update is called once per frame
    void Update()
    {
        DisplayScore(_gameManager.SheepNumber(), _gameManager.GetScore());
    }

    private void DisplayScore(int sheepNumber, int score)
    {
        var restScore = score - _displayedScore;
        if (restScore > 0 && _elapsedTimeAfterChange > Math.Pow(2, -restScore))
        {
            _elapsedTimeAfterChange = 0;
            _displayedScore += Math.Max(restScore / 30, 1);
            _text.text = $"ヒツジ: {sheepNumber}\nスコア: {_displayedScore}";
        }
        else
        {
            _elapsedTimeAfterChange += Time.deltaTime;
        }
    }
}
