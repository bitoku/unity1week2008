using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    private enum State
    {
        Run,
        Stop,
        Die,
    }

    private State _state;
    private DogAnimation _dogAnimation;
    [SerializeField] private float speed;

    private CursorInputController _cursor;
    // Start is called before the first frame update
    void Start()
    {
        _cursor = FindObjectOfType<CursorInputController>();
        _dogAnimation = FindObjectOfType<DogAnimation>();
        Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == State.Die) return;
        if (!_cursor.IsEnabled()) return;
        if (_state == State.Stop && _cursor.IsEnabled()) Run();
        if (_state != State.Run) return;
        Vector2 diff = _cursor.transform.position - transform.position;
        if (diff.magnitude < 0.1f)
        {
            _cursor.DisableMove();
            Stop();
        }
        Vector3 direction = diff.normalized * speed;
        transform.Translate(direction);
        var position = transform.position;
        position.z = (position.y + 5) / 10;
        transform.position = position;
    }

    public void Die()
    {
        _state = State.Die;
        _dogAnimation.Stop();
    }

    private void Stop()
    {
        _state = State.Stop;
        _dogAnimation.Stop();
    }

    private void Run()
    {
        _state = State.Run;
        _dogAnimation.Run();
    }
}
