using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private GameManager _gameManager;
    private Text _text;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _text = GetComponent<Text>();

    }
    // Update is called once per frame
    void Update()
    {
        var sheepNumber = _gameManager.SheepNumber();
        var score = _gameManager.GetScore();
        _text.text = $"羊の数: {sheepNumber}\nスコア: {score}";
    }
}
