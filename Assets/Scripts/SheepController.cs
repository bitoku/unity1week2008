using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class SheepController : MonoBehaviour
{
    private enum State
    {
        Free,
        Chased,
        Stop,
        Die,
    }
    
    private Vector2 _direction;
    private DogController _dog;
    private GameManager _gameManager;
    private State _state;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float changeRate;
    [SerializeField] private float restitution;

    void Start()
    {
        _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * minSpeed;
        _dog = FindObjectOfType<DogController>();
        _gameManager = FindObjectOfType<GameManager>();
        _state = State.Free;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        ChangeDirection();
        var direction3 = Vector3.ClampMagnitude(_direction, maxSpeed);
        transform.Translate(direction3);
    }

    private void ChangeState()
    {
        var distance = (_dog.transform.position - transform.position).magnitude;
        if (distance < 1f)
        {
            TransitionState(State.Chased);
        }
        else if (distance > 2f)
        {
            TransitionState(State.Free);
        }
        else if (transform.position.magnitude > 4.9f)
        {
            TransitionState(State.Stop);
        }
    }

    private void TransitionState(State toState)
    {
        var fromState = _state;
        _state = toState;
        if (fromState == State.Chased && toState == State.Free)
        {
            Vector2 edgeVector = transform.position.normalized;
            var randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _direction = (edgeVector + randomVector).normalized * minSpeed;
        }
    }

    private void ChangeDirection()
    {
        if (_state == State.Chased)
        {
            var vecFromDog = transform.position - _dog.transform.position;
            var r = vecFromDog.magnitude + 0.01f;
            _direction = Vector2.ClampMagnitude(_direction + restitution * (Vector2) vecFromDog.normalized / (r * r), maxSpeed);
        }
        else if (_state == State.Free)
        {
            var randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _direction = Vector2.ClampMagnitude(_direction + changeRate * randomVector, maxSpeed);
        }
    }
}
