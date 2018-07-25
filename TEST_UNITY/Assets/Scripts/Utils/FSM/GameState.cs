/// <summary>
/// Classe de base pour chacun des etats
/// </summary>

using System.Collections;

public abstract class GameState : FSMState
{
    private bool m_loaded = false;

    public bool Loaded
    {
        get { return m_loaded; }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected virtual IEnumerator Load()
    {
        yield return null;
    }

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Create());
    }

    protected override IEnumerator Enter(FSMState stateFrom)
    {
        return base.Enter(stateFrom);
    }

    protected override IEnumerator Exit(FSMState stateTo)
    {
        return base.Exit(stateTo);
    }

    private IEnumerator Create()
    {
        yield return StartCoroutine(Load());
        m_loaded = true;
    }
}