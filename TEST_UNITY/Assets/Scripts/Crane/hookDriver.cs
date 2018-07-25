using System.Collections;
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
    private Load m_loadCandidate;
    private Load m_currentload;
    private LoadCanvas m_loadCanvas;

    private Coroutine currentTimer;

    private void Start()
    {

        //set up rigid body for fixed joint
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.useGravity = false;
        m_rigidBody.isKinematic = true;

        m_loadCanvas = FindObjectOfType<LoadCanvas>();
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
            m_loadCandidate = hookedLoad;
            currentTimer = StartCoroutine(TimeHooking(hookedLoad));
        }
    }
    public void StopHook(Load hookedLoad)
    {
        if (hookFree && m_loadCandidate == hookedLoad)
        {
            StopCoroutine(currentTimer);
            m_hookingTime = 0.0f;
            m_loadCandidate = null;
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
        m_loadCandidate = null;
        yield break;
    }

    public void CloseHook(Load hookedLoad)
    {

        hookedLoad.transform.position = transform.position; //center load on hook 
        m_fixedJoint = gameObject.AddComponent<FixedJoint>(); //creating the joint
        m_fixedJoint.connectedBody = hookedLoad.GetComponent<Rigidbody>();//attaching the load
        hookFree = false;
        m_currentload = hookedLoad;
        m_loadCanvas.HighLightLoad(m_currentload, true);
        hookedLoad.CreateDropZone();

    }
    private void ReleaseHook()
    {
        Destroy(m_fixedJoint); //freeing the load

        hookFree = true;
        m_currentload.ClearDropZone();
        m_loadCanvas.HighLightLoad(m_currentload, false);
        m_currentload = null;

    }
}
