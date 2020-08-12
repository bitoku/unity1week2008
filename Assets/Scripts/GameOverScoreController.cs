using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GameOverScoreController : MonoBehaviour
{
    private static readonly Vector3 Direction = new Vector3(-1, 0, 0);
    [SerializeField] private float speed;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > (float)Screen.width / 2)
        {
            transform.Translate(Direction * speed);
        }
    }
}
