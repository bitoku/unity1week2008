using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score;
    private List<GameObject> _sheep;
    private float _elapsedTime;
    private const int ScoreInterval = 1;

    // Start is called before the first frame update
    void Awake()
    {
        _sheep = new List<GameObject>();
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (!(_elapsedTime > ScoreInterval)) return;
        _score += SheepNumber();
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
}
