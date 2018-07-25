using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    [SerializeField]
    private Text m_loadName;
    [SerializeField]
    private Text m_loadMass;

    public Text LoadName
    {
        get { return m_loadName; }
        set { m_loadName = value; }
    }

    public Text LoadMass
    {
        get { return m_loadMass; }
        set { m_loadMass = value; }
    }

    public void SetActive(bool active)
    {
        //A implementer
    }
}
