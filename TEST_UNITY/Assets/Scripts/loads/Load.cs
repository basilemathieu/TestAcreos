using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour
{
    [SerializeField]
    private string m_loadName;
    [SerializeField]
    private float m_loadMass;
    [SerializeField]
    private float m_hookMaxRange = 0.5f;
    private HookDriver m_hook;
    private bool m_hookInRange = false;
    private bool m_hookInRangePrevValue;


    private void Start()
    {
        m_hook = FindObjectOfType<HookDriver>();
    }

    private void Update()
    {
        m_hookInRange = Vector3.Distance(this.transform.position, m_hook.transform.position) <= m_hookMaxRange; //checking the range
        if (m_hookInRange != m_hookInRangePrevValue) //only if the state changed
        {
            if (m_hookInRange)
            {
                m_hook.BeginHook(this); //try to hook load
            }
            else
            {
                m_hook.StopHook(this); //quit trying
            }
        }
        m_hookInRangePrevValue = m_hookInRange; //recording state
    }

}





