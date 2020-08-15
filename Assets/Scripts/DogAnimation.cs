using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimation : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Run()
    {
        _animator.SetBool("Run", true);
        _animator.SetBool("Stop", false);
    }
    public void Stop()
    {
        _animator.SetBool("Stop", true);
        _animator.SetBool("Run", false);
    }
}
