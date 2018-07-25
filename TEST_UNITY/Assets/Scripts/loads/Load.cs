using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour
{
    [SerializeField]
    private string m_loadName;
    [SerializeField]
    private float m_loadMass;
    private SphereCollider m_sphereCollider;
    private float m_hookingDuration = 2.0f;
    private float m_hookMaxRange = 0.5f;

    private float m_hookingTime = 0.0f;
    private Coroutine m_hookTimer;
    private hookDriver m_hook;
    private bool m_hookInRange = false;


    private void Start()
    {
        m_hook = FindObjectOfType<hookDriver>();
    }

    private void Update()
    {
        m_hookInRange = Vector3.Distance(this.transform.position, m_hook.transform.position) <= m_hookMaxRange;

        if (m_hookInRange && m_hookingTime < m_hookingDuration)
        {
            m_hookingTime += Time.deltaTime;
        }
        else if (m_hookInRange)
        {
            m_hook.CloseHook(this);
        }
        else
        {
            m_hookingTime = 0.0f;
        }




    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            m_hookTimer = StartCoroutine(CountHookTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            if (m_hookingTime < m_hookingDuration)
            {
                StopCoroutine(m_hookTimer);
                m_hookingTime = 0.0f;
            }
        }
    }

    private IEnumerator CountHookTime()
    {
        while (m_hookingTime < m_hookingDuration)
        {
            m_hookingTime += Time.deltaTime;
            yield return null;
        }

        //hook
    }

}
