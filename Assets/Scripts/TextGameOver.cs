using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGameOver : MonoBehaviour
{
    private ScoreFactory _scoreFactory;

    // Start is called before the first frame update
    void Awake()
    {
        _scoreFactory = FindObjectOfType<ScoreFactory>();
        var scoreMessagesCount = _scoreFactory.ScoreMessagesCount();
        if(scoreMessagesCount % 2 == 1)
        {
            gameObject.transform.position = new Vector3(Screen.width / 2, 72 * (scoreMessagesCount + 1) /2 + Screen.height / 2, 0);
        }
        else
        {
            gameObject.transform.position = new Vector3(Screen.width / 2, 72 * (scoreMessagesCount + 2) / 2 - 36 + Screen.height / 2, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
