﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SheepController : MonoBehaviour
{
    private enum State
    {
        Free,
        Chased,
        Stop,
        Run,
    }
    
    private Vector2 _direction;
    private DogController _dog;
    private GameManager _gameManager;
    private State _state;
    private float _speed;
    private float _dieTimer;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float changeDirectionRate;
    [SerializeField] private float changeSpeedRate;
    [SerializeField] private float restitution;
    [SerializeField] private float dieTime;

    void Start()
    {
        _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _speed = minSpeed;
        _dog = FindObjectOfType<DogController>();
        _gameManager = FindObjectOfType<GameManager>();
        _state = State.Free;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        ChangeMoveParams();
        CountDieTimer();
        transform.Translate(_direction * _speed);
    }

    private void ChangeState()
    {
        var distance = (_dog.transform.position - transform.position).magnitude;
        if (_state == State.Stop && distance < 0.5f)
        {
            TransitionState(State.Run);
        }
        else if (_state == State.Run && transform.position.magnitude > 4.2f)
        {
            return;
        }
        else if (transform.position.magnitude > 4.9f)
        {
            TransitionState(State.Stop);
        }
        else if (distance < 1f)
        {
            TransitionState(State.Chased);
        }
        else if (distance > 2f)
        {
            TransitionState(State.Free);
        }
    }

    private void TransitionState(State toState)
    {
        var fromState = _state;
        _state = toState;
        switch (_state)
        {
            case State.Free:
                break;
            case State.Chased:
                break;
            case State.Stop:
                _dieTimer = 0;
                break;
            case State.Run:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ChangeMoveParams()
    {
        switch (_state)
        {
            case State.Chased:
            {
                _speed = maxSpeed;
                
                var vecFromDog = transform.position - _dog.transform.position;
                _direction = (_direction + restitution * (Vector2) vecFromDog.normalized).normalized;
                break;
            }
            case State.Free:
            {
                _speed += Random.Range(-changeSpeedRate, changeSpeedRate) * minSpeed;
                _speed = Math.Min(Math.Max(_speed, minSpeed), maxSpeed);
                
                var randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                _direction = (_direction + changeDirectionRate * randomVector).normalized;
                break;
            }
            case State.Stop:
            {
                _speed = 0;
                break;
            }
            case State.Run:
            {
                _speed = maxSpeed;
                _direction = (-transform.position).normalized;
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CountDieTimer()
    {
        if (_state == State.Stop)
        {
            _dieTimer += Time.deltaTime;
            if (_dieTimer > dieTime)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _gameManager.GameOver();
    }
}
