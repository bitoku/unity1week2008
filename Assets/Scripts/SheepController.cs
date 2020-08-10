using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 _direction;
    private float _speed;
    [SerializeField] private float changeRate;
    
    void Start()
    {
        _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _speed = 0.003f;
    }

    private void ChangeDirection()
    {
        _direction += new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * changeRate;
        _direction = _direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
        Vector3 direction3 = _speed * _direction;
        transform.Translate(direction3);
        Debug.Log(_direction);
    }
}
