using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    [SerializeField] private State[] states;
    public State[] GetStates => states;
    private int _stateNum;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ÝsGameRestarted") == 0)
        {
            TransitionToNextState();
        }
        else
        {
            PlayerPrefs.SetInt("ÝsGameRestarted", 0);
            transitionToSpecificState(1);
        }
    }
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }
    public void TransitionToNextState()
    {
        if (_stateNum < states.Length)
        {
            ChangeState(states[_stateNum]);
            _stateNum++;
        }
    }
    public void transitionToSpecificState(int stateid)
    {
        ChangeState(states[stateid]);
        _stateNum = stateid + 1;
    }
    //public void CloseAllStates()
    //{
    //    for (int i = 0; i < states.Length; i++)
    //    {
    //        states[i].OnExit();
    //    }
    //}
}
