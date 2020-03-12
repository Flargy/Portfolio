using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState { get { return currentState; } }
    //public State[] States { get { return states; } }

    [SerializeField] private State[] states;
    private readonly Dictionary<Type, State> stateDictionary = new Dictionary<Type, State>();
    [SerializeField] private State currentState;
    private State[] stateClones;

    protected virtual void Awake()
    {
        stateClones = states;
        int StateIndex = 0;
        foreach (State state in states)
        {
            //Går igenom states, Gör kopia med instantiate
            //Initialize på Staten och lägger till dem i dictionary
            State instance = Instantiate(state);
            instance.Index = StateIndex;
            stateClones[StateIndex] = instance;
            StateIndex++;
            instance.Initialize(this);
            stateDictionary.Add(instance.GetType(), instance);
            if (currentState != null) // om vi inte har currentState lägger vi till den första staten som det
                continue;
            currentState = instance;
            currentState.Enter();
        }
    }

    public void Update()
    {
        currentState.Update(); // Kör nuvarande states update
    }

    public T GetState<T>()
    {
        Type type = typeof(T);
        if (!stateDictionary.ContainsKey(type))
            throw new NullReferenceException("No state of type: " + type + " found");
        return (T)Convert.ChangeType(stateDictionary[type], type);
    }

    public void TransitionTo<T>()
    {
        currentState.Exit();
        currentState = GetState<T>() as State;
        currentState.Enter();
    }

    public void TransitionTo(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void TransitionTo(int newstate)
    {
        currentState.Exit();
        Debug.Log(stateClones[newstate]);
        currentState = stateClones[newstate];
        currentState.Enter();
    }
}
