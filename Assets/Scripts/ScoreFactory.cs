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
        // _scoreMessages = new List<string> {"aaaaa", "bbbbb", "ccccc"};
    }

    async void Start()
    {
        var scoreTextPrefab = Resources.Load("ScoreText");
        for (var i = 0; i < _scoreMessages.Count; i++)
        {
            await Task.Delay(500);
            var scoreText = (GameObject) Instantiate(scoreTextPrefab, _canvas.transform);
            scoreText.transform.position = new Vector3(Screen.width + 100, Screen.height - 300 - 100 * i, 0);
            scoreText.GetComponent<Text>().text = _scoreMessages[i];
        }
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
