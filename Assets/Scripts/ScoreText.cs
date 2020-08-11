using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private GameManager _gameManager;
    private Text _text;
    private int _displayedScore;
    private float _elapsedTimeAfterChange = 0f;
    private const float UpdateInterval = 1f;

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

    // IEnumerator ScoreAnimation(int addScore, float time)
    // {
    //     var before = _displayedScore;
    //     var after = _displayedScore + addScore;
    //
    //     _displayedScore += addScore;
    //     var elapsedTime = 0f;
    //
    //     while (elapsedTime < time)
    //     {
    //         var rate = elapsedTime / time;
    //         DisplayScore(_gameManager.SheepNumber(), before + (after - before) * (int)rate);
    //         elapsedTime += Time.deltaTime;
    //         yield return new WaitForSeconds(0.01f);
    //     }
    //     DisplayScore(_gameManager.SheepNumber(), _displayedScore);
    // }

    private void DisplayScore(int sheepNumber, int score)
    {
        if (score > _displayedScore && _elapsedTimeAfterChange > 0.03f)
        {
            _elapsedTimeAfterChange = 0;
            _displayedScore += 1;
            _text.text = $"羊の数: {sheepNumber}\nスコア: {_displayedScore}";
        }
        else
        {
            _elapsedTimeAfterChange += Time.deltaTime;
        }
    }
}
