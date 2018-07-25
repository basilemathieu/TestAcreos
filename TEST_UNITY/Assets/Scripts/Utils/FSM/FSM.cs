using UnityEngine;
using System.Collections.Generic;
using System;

///<summary>
/// FSM
///</summary>
public class FSM
{
    GameObject m_gameObject;

    //! stack of states
    Stack<FSMState> m_stackedStates;

    private readonly Dictionary<Type, FSMState> m_cachedStates;

    public List<FSMState> CreateStates(System.Type[] states)
    {
        List<FSMState> result = new List<FSMState>();

        for (int i = 0; i < states.Length; ++i)
        {
            if (typeof(GameState).IsAssignableFrom(states[i]))
            {
                FSMState state;

                if (!m_cachedStates.TryGetValue(states[i], out state))
                {
                    state = GameObject.FindObjectOfType(states[i]) as FSMState;
                    if (state == null)
                    {
                        state = m_gameObject.AddComponent(states[i]) as FSMState;
                    }
                    state.SetFSM(this);
                    m_cachedStates.Add(states[i], state);
                }
            }
        }

        foreach (KeyValuePair<Type, FSMState> state in m_cachedStates)
        {
            result.Add(state.Value);
        }
        return result;
    }

    public FSM(GameObject go)
    {
        m_gameObject = go;
        m_cachedStates = new Dictionary<Type, FSMState>();
        m_stackedStates = new Stack<FSMState>();
    }

    public void Clear()
    {
        _Clear();
    }

    internal void ChangeState(System.Type stateAsking, params object[] userdata)
    {
        if (typeof(FSMState).IsAssignableFrom(stateAsking))
        {
            _ChangeState(stateAsking, userdata);
        }
    }

    void _ChangeState(System.Type stateType, params object[] userdata)
    {
        if (typeof(FSMState).IsAssignableFrom(stateType))
        {
            FSMState stateTo = GetState(stateType);

            if (stateTo)
            {
                if (stateTo.CallInit(userdata))
                {
                    FSMState stateFrom = (m_stackedStates.Count > 0) ? m_stackedStates.Peek() : null;
                    if (stateFrom != null)
                    {
                        stateFrom.CallExit(stateTo);
                        m_stackedStates.Pop();
                    }

                    m_stackedStates.Push(stateTo);
                    stateTo.CallEnter(stateFrom);

                    if (stateFrom != null)
                    {
                        stateFrom.CallRelease();
                    }

                }
                else
                {
                    Debug.LogError("FSM : state " + stateType.GetType().Name + " failed to initialize.");
                    stateTo.CallRelease();
                }
            }
        }
    }

    public FSMState Current
    {
        get { return (m_stackedStates.Count > 0) ? m_stackedStates.Peek() : null; }
    }

    void _Clear()
    {
        if (m_stackedStates.Count > 0)
        {
            FSMState currentState = m_stackedStates.Pop();
            currentState.CallExit(null);
            currentState.CallRelease();

            while (m_stackedStates.Count > 0)
            {
                FSMState stateFrom = m_stackedStates.Pop();
                stateFrom.CallRelease();
            }
        }
    }

    public FSMState GetState(System.Type stateType)
    {
        FSMState state = null;
        if (typeof(FSMState).IsAssignableFrom(stateType))
        {
            bool stateFound = m_cachedStates.TryGetValue(stateType, out state);
            if (stateFound == false)
            {
                return null;
            }
        }
        return state;
    }
}
