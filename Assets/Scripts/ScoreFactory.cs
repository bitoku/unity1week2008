using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFactory : MonoBehaviour
{
    private float _elapsedTime;
    private GameObject _canvas;
    private List<string> _scoreMessages;

    void Awake()
    {
        _canvas = GameObject.Find("Canvas");
        _scoreMessages = new List<string>();
        // テスト用
        //_scoreMessages = new List<string> {"aaaaa", "bbbbb", "ccccc"};
    }

    async void Start()
    {
        var scoreTextPrefab = Resources.Load("ScoreText");
        for (var i = 0; i < _scoreMessages.Count; i++)
        {
            await Task.Delay(500);
            var scoreText = (GameObject) Instantiate(scoreTextPrefab, _canvas.transform);
            if (_scoreMessages.Count % 2 == 1)
            {
                scoreText.transform.position = new Vector3(Screen.width + 100, 72 * (_scoreMessages.Count + 1) / 2 + Screen.height / 2 - 72 * (i + 2), 0);
            }
            else
            {
                scoreText.transform.position = new Vector3(Screen.width + 100, 72 * (_scoreMessages.Count + 2) / 2 - (36 + 72 * (i + 2)) + Screen.height / 2, 0);
            }
            //scoreText.transform.position = new Vector3(Screen.width /2, Screen.height /2 - 36 * (i-1), 0);
            scoreText.GetComponent<Text>().text = _scoreMessages[i];
        }
    }

    public int ScoreMessagesCount()
    {
        return _scoreMessages.Count;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public void SetScore(string scoreName, int score)
    {
        _scoreMessages.Add($"{scoreName}: {score}");
    }
}
