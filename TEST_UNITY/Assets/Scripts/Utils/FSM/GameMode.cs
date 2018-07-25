/// <summary>
/// Classe de base pour chacun des modes
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameMode : MonoBehaviour
{
    protected FSM m_stateMachine;

    private bool m_loaded = false;

    public bool Loaded
    {
        get { return m_loaded; }
    }

    public GameState GetGameState(System.Type t)
    {
        return m_stateMachine.GetState(t) as GameState;
    }

    public GameState CurrentState
    {
        get { return m_stateMachine.Current as GameState; }
    }

    protected virtual IEnumerator Load()
    {
        yield return null;
    }

    public virtual IEnumerator Init()
    {
        yield return null;
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void Awake()
    {
        enabled = false;

        m_stateMachine = new FSM(gameObject);
        StartCoroutine(Create());
    }

    public virtual void Dispose()
    {
        m_stateMachine.Clear();
    }
    
    protected virtual IEnumerator CreateStates(System.Type[] states)
    {
        for (int i = 0; i < states.Length; ++i)
        {
            if (!typeof(GameState).IsAssignableFrom(states[i]))
            {
                states[i] = null;
            }
        }

        List<FSMState> gameStates = m_stateMachine.CreateStates(states);

        for (int i = 0, count = gameStates.Count; i < count; ++i)
        {
            GameState gameState = gameStates[i] as GameState;
            while (gameState != null && !gameState.Loaded)
            {
                yield return null;
            }
        }
    }

    public void ChangeState(System.Type gamestate, params object[] a_userdata)
    {
        if (typeof(GameState).IsAssignableFrom(gamestate))
        {
            m_stateMachine.ChangeState(gamestate, a_userdata);
        }
    }

    public virtual void StartState()
    {

    }

    private IEnumerator Create()
    {
        yield return StartCoroutine(Load());
        m_loaded = true;
    }
}
