using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAnimation : MonoBehaviour
{
    private enum SheepState
    {
        Walking = 0,
        Stop = 1,
        Jump = 2
    }
    private SheepState _sheepState;
    private Animator _sheepAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _sheepAnimator = GetComponent<Animator>();

        //最初はWalkingしてる
        _sheepState = SheepState.Walking;
        _sheepAnimator.SetInteger("SheepState", (int)SheepState.Walking);
    }

    public void WalkingAnimation()
    {
        _sheepAnimator.SetInteger("SheepState", (int)SheepState.Walking);
    }

    public void JumpAnimation()
    {
        _sheepAnimator.SetInteger("SheepState", (int)SheepState.Jump);
    }
    
    public void StopAnimation()
    {
        _sheepAnimator.SetInteger("SheepState", (int)SheepState.Stop);
    }

    public void ChangeAnimation(int sheepStateNumber)
    {
        if (sheepStateNumber == (int)SheepState.Walking)
        {
            //Walking
            _sheepState = SheepState.Walking;
            _sheepAnimator.SetInteger("SheepState", (int)SheepState.Walking);
        }
        else if (sheepStateNumber == (int)SheepState.Stop)
        {
            //Stop
            _sheepState = SheepState.Stop;
            _sheepAnimator.SetInteger("SheepState", (int)SheepState.Stop);
        }
        else if (sheepStateNumber == (int)SheepState.Jump)
        {
            //Jump
            _sheepState = SheepState.Jump;
            _sheepAnimator.SetInteger("SheepState", (int)SheepState.Jump);
        }
        else
        {
            Debug.LogWarning("羊の状態が分かりません。とりあえず歩かせます。");
            _sheepState = SheepState.Walking;
            _sheepAnimator.SetInteger("SheepState", (int)SheepState.Walking);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //端に行ったらStopする
        /*if (Input.GetKey(KeyCode.S))
        {
            _sheepState = SheepState.Stop;
            sheep_Animator.SetInteger("SheepState", (int)SheepState.Stop);

            //犬が来たらWalkingしなおす
            if (Input.GetKey(KeyCode.W))
            {
                _sheepState = SheepState.Walking;
                sheep_Animator.SetInteger("SheepState", (int)SheepState.Walking);
            }
            //犬が来なかったらJumpする
            else if (Input.GetKey(KeyCode.Space))
            {
                _sheepState = SheepState.Jump;
                sheep_Animator.SetInteger("SheepState", (int)SheepState.Jump);
                //_sheepState = SheepState.Stop;
                //sheep_Animator.SetInteger("SheepState", (int)SheepState.Stop);
            }
            else
            {
                _sheepState = SheepState.Stop;
                sheep_Animator.SetInteger("SheepState", (int)SheepState.Stop);
            }
        }*/
        /*else
        {
            //基本的にはWalkingしてる
            _sheepState = SheepState.Walking;
            sheep_Animator.SetInteger("SheepState", (int)SheepState.Walking);
        }*/
    }
}
