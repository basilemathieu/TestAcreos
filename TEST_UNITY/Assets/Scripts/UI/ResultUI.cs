using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private Text m_criterion;
    [SerializeField]
    private Text m_result;

    public Text Criterion
    {
        get { return m_criterion; }
        set { m_criterion = value; }
    }

    public Text Result
    {
        get { return m_result; }
        set { m_result = value; }
    }
}
