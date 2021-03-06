﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SheepController : MonoBehaviour
{
    private enum State
    {
        Born,
        Free,
        Chased,
        Stop,
        Run,
        Die,
        Jump,
        Rotate,
    }
    
    private Vector2 _direction;
    private DogController _dog;
    private GameManager _gameManager;
    private float _speed;
    private float _dieTimer;
    private SheepAnimation _sheepAnimation;
    private const float JumpSpeed = 0.01f;
    private float _fieldRadius;
    private Transform _rotatingSheep;
    private float _bornTimer;
    [SerializeField] private GameObject sheepFall;
    [SerializeField] private State state;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float changeDirectionRate;
    [SerializeField] private float changeSpeedRate;
    [SerializeField] private float restitution;
    [SerializeField] private float dieTime;
    [SerializeField] private float bornTime;

    void Start()
    {
        _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _speed = minSpeed;
        _dog = FindObjectOfType<DogController>();
        _gameManager = FindObjectOfType<GameManager>();
        state = State.Born;
        _bornTimer = 0;
        transform.localScale = Vector3.zero;
        var sheepWithAnimation = gameObject.transform.Find("SheepWithAnimation");
        _sheepAnimation = sheepWithAnimation.GetComponent<SheepAnimation>();
        var field = GameObject.Find("FieldCircle");
        _fieldRadius = field.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _rotatingSheep = transform.Find("SheepWithAnimation");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Die) return;
        ChangeState();
        Move();
        CountDieTimer();
        CountBornTimer();
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y, (transform.position.y + 5) / 10);

        Vector3 scale = transform.localScale;
        if (0 >= _direction.x)
        {
            scale.x = 1;
            transform.localScale = scale;
        }
        else if (0 < _direction.x)
        {
            scale.x = -1;
            transform.localScale = scale;
        }
    }

    private void ChangeState()
    {
        var distance = ((Vector2)(_dog.transform.position - transform.position)).magnitude;
        var distanceFromZero = ((Vector2) transform.position).magnitude;
        switch (state)
        {
            case State.Born:
                if (_bornTimer > bornTime)
                {
                    TransitionState(State.Free);
                }

                break;
            case State.Free: 
                if (distanceFromZero > _fieldRadius * 0.98f)
                {
                    TransitionState(State.Stop);
                }
                else if (distance < _fieldRadius * 0.5f)
                {
                    TransitionState(State.Chased);
                }
                break;
            case State.Chased:
                if (distanceFromZero > _fieldRadius * 0.98f)
                {
                    TransitionState(State.Stop);
                }
                else if (distance > _fieldRadius * 0.7f)
                {
                    TransitionState(State.Free);
                }
                break;
            case State.Stop:
                if (_dieTimer > dieTime)
                {
                    TransitionState(State.Jump);
                } 
                else if (distance < _fieldRadius * 0.2f)
                {
                    TransitionState(State.Run);
                }
                break;
            case State.Run:
                if (distanceFromZero < _fieldRadius * 0.95f)
                {
                    TransitionState(State.Free);
                }
                break;
            case State.Die:
                break;
            case State.Jump:
                if (distanceFromZero > _fieldRadius * 1.07f)
                {
                    TransitionState(State.Rotate);
                }
                break;
            case State.Rotate:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void TransitionState(State toState)
    {
        var fromState = state;
        state = toState;
        switch (state)
        {
            case State.Free:
                if (fromState == State.Born)
                {
                    transform.localScale = Vector3.one;
                }
                break;
            case State.Chased:
                break;
            case State.Stop:
                if (fromState != State.Stop)
                {
                    _sheepAnimation.StopAnimation();
                    _dieTimer = 0;
                }
                break;
            case State.Run:
                if (fromState != State.Run)
                {
                    _sheepAnimation.WalkingAnimation();
                }
                break;
            case State.Die:
                break;
            case State.Jump:
                if (fromState != State.Jump)
                {
                    _sheepAnimation.JumpAnimation();
                    StartCoroutine(_gameManager.GameOver());
                }
                break;
            case State.Rotate:
                if (fromState != State.Rotate)
                {
                    Instantiate(sheepFall);
                }
                break;
            default:
                break;
        }
    }

    private void Move()
    {
        switch (state)
        {
            case State.Born:
            {
                transform.localScale = Vector3.one * (_bornTimer / bornTime);
                break;
            }
            case State.Chased:
            {
                _speed = maxSpeed;
                
                Vector2 vecFromDog = transform.position - _dog.transform.position;
                _direction = (_direction + restitution * vecFromDog.normalized).normalized;
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
                _direction = (-(Vector2)transform.position).normalized;
                break;
            }
            case State.Jump:
            {
                _direction = ((Vector2) transform.position).normalized;
                _speed = JumpSpeed;
                break;
            }
            case State.Rotate:
            {
                _rotatingSheep.Rotate(new Vector3(0, 0, 1), 5);
                _rotatingSheep.localScale *= 0.99f;
                return;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
        transform.Translate(_direction * _speed);
    }

    private void CountDieTimer()
    {
        if (state != State.Stop) return;
        _dieTimer += Time.deltaTime;
    }

    private void CountBornTimer()
    {
        if (state != State.Born) return;
        _bornTimer += Time.deltaTime;
    }

    public void Die()
    {
        if (state != State.Jump) TransitionState(State.Die);
    }
}
