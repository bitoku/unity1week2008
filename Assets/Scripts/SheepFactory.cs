using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFactory : MonoBehaviour
{
    private bool _canFactory;
    [SerializeField] private float interval;
    private float _timeElapsed;
    private GameObject _sheep;
    
    // Start is called before the first frame update
    void Start()
    {
        _sheep = (GameObject) Resources.Load("Sheep");
        StartFactory();
    }

    private void StartFactory()
    {
        _canFactory = true;
        _timeElapsed = 0f;
    }

    private void StopFactory()
    {
        _canFactory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canFactory) return;
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed > interval)
        {
            Instantiate(_sheep, Vector3.zero, Quaternion.identity);
            _timeElapsed = 0f;
        }
    }
}
