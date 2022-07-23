using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType{
    Idle,Patrol,Chase,React,Attack
}



public class FSM : MonoBehaviour
{
    public Transform target;

    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    // Start is called before the first frame update
    void Start()
    {
        states.Add(StateType.Idle, new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TranslateState(StateType type) {
        if (currentState != null) {
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }
}
