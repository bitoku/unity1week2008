using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFactory : MonoBehaviour
{
    private float _elapsedTime;
    private GameObject _canvas;
    private List<int> _scores;
    private RestartButtonController _restartButtonController;
    private TweetButtonController _tweetButtonController;

    void Awake()
    {
        _canvas = GameObject.Find("Canvas");
        _scores = new List<int>();
        _restartButtonController = FindObjectOfType<RestartButtonController>();
        _restartButtonController.gameObject.SetActive(false);
        _tweetButtonController = FindObjectOfType<TweetButtonController>();
        _tweetButtonController.gameObject.SetActive(false);
        // テスト用
        // _scoreMessages = new List<string> {"aaaaa", "bbbbb", "ccccc"};
    }

    void Start()
    {
        StartCoroutine(CreateText());
        StartCoroutine(CreateButton());
    }

    private IEnumerator CreateText()
    {
        var scoreTextPrefab = Resources.Load("ScoreText");
        for (var i = 0; i < _scores.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var scoreText = (GameObject) Instantiate(scoreTextPrefab, _canvas.transform);
            if (_scores.Count % 2 == 1)
            {
                scoreText.transform.position = new Vector3(Screen.width + 100, 72 * (_scores.Count + 1) / 2 + Screen.height / 2 - 72 * (i + 2), 0);
            }
            else
            {
                scoreText.transform.position = new Vector3(Screen.width + 100, 72 * (_scores.Count + 2) / 2 - (36 + 72 * (i + 2)) + Screen.height / 2, 0);
            }
            scoreText.GetComponent<Text>().text = $"スコア: {_scores[i]}";
        }
    }

    private IEnumerator CreateButton()
    {
        yield return new WaitForSeconds(1.5f);
        _restartButtonController.gameObject.SetActive(true);
        _tweetButtonController.gameObject.SetActive(true);
    }

    public int ScoreMessagesCount()
    {
        return _scores.Count;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public void SetScore(int score)
    {
        _scores.Add(score);
    }

    public List<int> GetScores()
    {
        return _scores;
    }
}
