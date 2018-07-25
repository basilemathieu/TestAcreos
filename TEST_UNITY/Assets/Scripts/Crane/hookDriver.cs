using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HookDriver : MonoBehaviour
{
    private Rigidbody m_rigidBody;
    private FixedJoint m_fixedJoint;
    private bool hookFree = true;
    [SerializeField]
    private float m_hookDuration = 2.0f;
    private float m_hookingTime = 0.0f;
    private Load m_currentLoad;
    private Coroutine currentTimer;

    private void Start()
    {
        //set up rigid body for fixed joint
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.useGravity = false;
        m_rigidBody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (hookFree == false)
            {
                ReleaseHook();
            }
        }

    }
    public void BeginHook(Load hookedLoad)
    {
        if (hookFree)
        {
            m_currentLoad = hookedLoad;
            currentTimer = StartCoroutine(TimeHooking(hookedLoad));
        }
    }
    public void StopHook(Load hookedLoad)
    {
        if (hookFree && m_currentLoad == hookedLoad)
        {
            StopCoroutine(currentTimer);
            m_hookingTime = 0.0f;
            m_currentLoad = null;
        }
    }

    private IEnumerator TimeHooking(Load hookedLoad) 
    {
        while (m_hookingTime < m_hookDuration)//waiting the needed time
        {
            m_hookingTime += Time.deltaTime;
            yield return null;
        }
        CloseHook(hookedLoad);
        m_hookingTime = 0.0f;
        m_currentLoad = null;
        yield break;
    }

    public void CloseHook(Load hookedLoad)
    {

        hookedLoad.transform.position = transform.position; //center load on hook 
        m_fixedJoint = gameObject.AddComponent<FixedJoint>(); //creating the joint
        m_fixedJoint.connectedBody = hookedLoad.GetComponent<Rigidbody>();//attaching the load
        hookFree = false;

    }
    private void ReleaseHook()
    {
        Destroy(m_fixedJoint); //freeing the load

        hookFree = true;
    }
}
