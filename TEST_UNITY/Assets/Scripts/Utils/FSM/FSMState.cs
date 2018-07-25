using System.Collections;
using UnityEngine;

public abstract class FSMState : MonoBehaviour
{
    public delegate void OnEnter();
    public delegate void OnExit();

    public event OnEnter onEnter;
    public event OnExit onExit;

    FSM m_fsm;

    public bool IsActive
    {
        get { return m_fsm.Current == this; }
    }
    
    protected virtual void OnDestroy()
    {
    }

    protected virtual void Awake()
    {
        enabled = false;
    }

    protected void ChangeState(System.Type stateAsking, params object[] userdata)
    {
        m_fsm.ChangeState(stateAsking, userdata);
    }

    protected virtual bool Init(params object[] userdata)
    {
        return true;
    }

    protected virtual void Release()
    {
    }

    protected virtual IEnumerator Enter(FSMState stateFrom)
    {
        enabled = true;
        if (onEnter != null)
            onEnter();
        yield return null;
    }

    protected virtual IEnumerator Exit(FSMState stateTo)
    {
        enabled = false;
        if (onExit != null)
            onExit();
        yield return null;
    }

    internal void CallEnter(FSMState stateFrom)
    {
        StartCoroutine(Enter(stateFrom));
    }

    internal void CallExit(FSMState stateTo)
    {
        StartCoroutine(Exit(stateTo));
    }

    internal bool CallInit(params object[] userdata)
    {
        return Init(userdata);
    }

    internal void CallRelease()
    {
        Release();
    }
    
    internal void SetFSM(FSM fsm)
    {
        m_fsm = fsm;
    }
}
