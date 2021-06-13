using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class StateMachine<StateType>
{
    public delegate void StateBehaviorCallback();
    public delegate bool StateTransitionCallback();

    private StateType _currentState;
    private Dictionary<StateType, StateBehaviorCallback> _stateBehaviorCallbacks;
    private Dictionary<StateType, StateBehaviorCallback> _stateFixedBehaviorCallbacks;
    private Dictionary<StateType, StateBehaviorCallback> _stateEntryCallbacks;
    private Dictionary<StateType, StateBehaviorCallback> _stateExitCallbacks;
    private Dictionary<StateType, Dictionary<StateType, StateTransitionCallback>> _stateTransitionCallbacks;

    public StateMachine(StateType initialState = default(StateType))
    {
        _currentState = initialState;
        _stateBehaviorCallbacks = new Dictionary<StateType, StateBehaviorCallback>();
        _stateFixedBehaviorCallbacks = new Dictionary<StateType, StateBehaviorCallback>();
        _stateEntryCallbacks = new Dictionary<StateType, StateBehaviorCallback>();
        _stateExitCallbacks = new Dictionary<StateType, StateBehaviorCallback>();
        _stateTransitionCallbacks = new Dictionary<StateType, Dictionary<StateType, StateTransitionCallback>>();
        foreach (StateType type in Enum.GetValues(typeof(StateType)))
        {
            _stateTransitionCallbacks[type] = new Dictionary<StateType, StateTransitionCallback>();
        }
    }

    public StateType GetCurrentState()
    {
        return _currentState;
    }
    
    public void SetStateBehaviorCallback(StateType state, StateBehaviorCallback behaviorCallback)
    {
        _stateBehaviorCallbacks[state] = behaviorCallback;
    }

    public void SetStateFixedBehaviorCallback(StateType state, StateBehaviorCallback behaviorCallback)
    {
        _stateFixedBehaviorCallbacks[state] = behaviorCallback;
    }

    public void SetStateEntryCallback(StateType state, StateBehaviorCallback entryCallback)
    {
        _stateEntryCallbacks[state] = entryCallback;
    }

    public void SetStateExitCallback(StateType state, StateBehaviorCallback exitCallback)
    {
        _stateExitCallbacks[state] = exitCallback;
    }

    public void SetStateTransitionCallback(StateType fromState, StateType toState,
                                           StateTransitionCallback transitionCallback)
    {
        _stateTransitionCallbacks[fromState][toState] = transitionCallback;
    }

    public void SetStateTransitionCallback(StateType[] fromStates, StateType toState,
                                           StateTransitionCallback transitionCallback)
    {
        foreach(StateType fromState in fromStates)
        {
            _stateTransitionCallbacks[fromState][toState] = transitionCallback;
        }
    }

    public void Run()
    {
        Behavior();
        foreach(KeyValuePair<StateType, StateTransitionCallback> transition in _stateTransitionCallbacks[_currentState])
        {
            if(transition.Value())
            {
                Exit();
                _currentState = transition.Key;
                Entry();
            }
        }
    }

    public void FixedRun()
    {
        FixedBehavior();
        foreach(KeyValuePair<StateType, StateTransitionCallback> transition in _stateTransitionCallbacks[_currentState])
        {
            if(transition.Value())
            {
                Exit();
                _currentState = transition.Key;
                Entry();
            }
        }
    }

    private void Behavior()
    {
        if(_stateBehaviorCallbacks.ContainsKey(_currentState))
        {
            _stateBehaviorCallbacks[_currentState]();
        }
    }

    private void FixedBehavior()
    {
        if(_stateFixedBehaviorCallbacks.ContainsKey(_currentState))
        {
            _stateFixedBehaviorCallbacks[_currentState]();
        }
    }

    private void Entry()
    {
        if(_stateEntryCallbacks.ContainsKey(_currentState))
        {
            _stateEntryCallbacks[_currentState]();
        }
    }

    private void Exit()
    {
        if (_stateExitCallbacks.ContainsKey(_currentState))
        {
            _stateExitCallbacks[_currentState]();
        }
    }
}
