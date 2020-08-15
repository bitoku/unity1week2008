using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    private enum State
    {
        Move,
        Stop,
    }

    private State _state;
    [SerializeField] private float speed;

    private CursorInputController _cursor;
    // Start is called before the first frame update
    void Start()
    {
        _cursor = FindObjectOfType<CursorInputController>();
        _state = State.Move;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == State.Stop) return;
        if (!_cursor.IsEnabled()) return;
        Vector2 diff = _cursor.transform.position - transform.position;
        if (diff.magnitude < 0.1f)
        {
            _cursor.DisableMove();
        }
        Vector3 direction = diff.normalized * speed;
        transform.Translate(direction);
    }

    public void Stop()
    {
        _state = State.Stop;
    }
}
