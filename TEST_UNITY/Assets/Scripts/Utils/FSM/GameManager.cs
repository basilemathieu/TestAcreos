/// <summary>
/// Gere les gamemodes
/// </summary>

using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected GameMode m_pGameMode = null;
    protected GameObject m_gameManagerObject = null;

    public GameMode GameMode
    {
        get { return m_pGameMode; }
    }

    public GameManager()
    {
        m_pGameMode = null;
        m_gameManagerObject = new GameObject("Game Manager");
        Object.DontDestroyOnLoad(m_gameManagerObject);
    }

    public override void Init()
    {

    }

    public void Reset()
    {
        if (m_pGameMode != null)
        {
            m_pGameMode.Dispose();
            GameObject.Destroy(m_pGameMode.gameObject);
            m_pGameMode = null;
        }
    }

    public void SetGameMode(System.Type gameMode)
    {
        if (typeof(GameMode).IsAssignableFrom(gameMode))
        {
            Reset();
            GameObject gameModeObj = new GameObject("Game Mode", gameMode);
            gameModeObj.transform.parent = m_gameManagerObject.transform;
            m_pGameMode = gameModeObj.GetComponent<GameMode>();
        }
        else
        {
            Debug.LogError(" the type send in parameter isn't a gameMode : " + gameMode);
        }
    }

    public T GetGameMode<T>() where T : GameMode
    {
        return m_pGameMode as T;
    }
}