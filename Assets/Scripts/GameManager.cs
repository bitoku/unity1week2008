using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _score;
    private List<GameObject> _sheep;
    private SheepFactory _sheepFactory;
    private DogController _dogController;
    private BGMController _bgmController;
    private float _elapsedTime;
    private const int ScoreInterval = 1;
    private bool _isPlaying;

    // Start is called before the first frame update
    void Awake()
    {
        _sheep = new List<GameObject>();
        _sheepFactory = FindObjectOfType<SheepFactory>();
        _dogController = FindObjectOfType<DogController>();
        _bgmController = FindObjectOfType<BGMController>();
        _score = 0;
        _isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (!(_elapsedTime > ScoreInterval)) return;
        if (CanScore()) _score += SheepNumber();
        _elapsedTime = 0;
    }

    public void AddSheep(GameObject sheep)
    {
        _sheep.Add(sheep);
    }

    public int SheepNumber()
    {
        return _sheep.Count;
    }

    public int GetScore()
    {
        return _score;
    }

    private bool CanScore()
    {
        return _isPlaying;
    }
    
    public IEnumerator GameOver()
    {
        _sheepFactory.StopFactory();
        _isPlaying = false;
        _bgmController.Stop();
        foreach (var sheep in _sheep)
        {
            sheep.GetComponent<SheepController>().Die();
        }
        _dogController.Die();
        
        SceneManager.sceneLoaded += SceneCallback;

        yield return new WaitForSeconds(3);  // 3秒待つ

        SceneManager.LoadScene("GameOverScene");
    }

    private void SceneCallback(Scene next, LoadSceneMode mode)
    {
        var scoreFactory = FindObjectOfType<ScoreFactory>();
        scoreFactory.SetScore("スコア", GetScore());

        SceneManager.sceneLoaded -= SceneCallback;
    }
}
