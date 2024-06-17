using CodeBase;
using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class HeroAnimator : MonoBehaviour, IAnimationStateReader
{
    [SerializeField] public Animator _animator;
    [SerializeField] private Rigidbody _rb;

    private static readonly int MoveHash = Animator.StringToHash("Walking");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _walkingStateHash = Animator.StringToHash("Run");
    private readonly int _jumpStateHash = Animator.StringToHash("Jump");

    public AnimatorState State { get; private set; }

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    private void Update()
    {
        
    }

    public void PlayRun(float speed)
    {
        //_animator.SetFloat(MoveHash, speed, 0.1f, Time.deltaTime);
        //_animator.SetBool(_walkingStateHash, true);
        //_animator.SetTrigger(_walkingStateHash);
        _animator.SetFloat(MoveHash, speed, 0.1f, Time.deltaTime);
    }

    public void ResetToIdle()
    {
        _animator.Play(_idleStateHash, -1);
    }

    public void PlayJump()
    {
        _animator.SetTrigger(_jumpStateHash);
    }

    public void EnteredState(int stateHash)
    {
         State = StateFor(stateHash);
         StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash)
    {
        StateExited?.Invoke(StateFor(stateHash));
    }

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
        if (stateHash == _idleStateHash)
        {
            state = AnimatorState.Idle;
        }
        /*else if (stateHash == _attackStateHash)
        {
            state = AnimatorState.Attack;
        }*/
        else if (stateHash == _walkingStateHash)
        {
            state = AnimatorState.Walking;
        }
        else if(stateHash == _jumpStateHash)
        {
            state = AnimatorState.Jump;
        }
        /*else if (stateHash == _deathStateHash)
        {
            state = AnimatorState.Died;
        }*/
        else
        {
            state = AnimatorState.Unknown;
        }
       
        return state;
    }
}
