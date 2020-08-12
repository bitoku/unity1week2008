using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFactory : MonoBehaviour
{
    private float _elapsedTime;

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
    }
}
