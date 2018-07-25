using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class hookDriver : MonoBehaviour {
    private FixedJoint m_fixedjoint;
    private bool hookFree = true;

    private void Start()
    {
        m_fixedjoint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("Fire1"))
        {
            if (hookFree == false)
            {
                ReleaseHook();
            }
        }
		
	}

    public void CloseHook(Load hookedLoad)
    {
        if (hookFree)
        {
            m_fixedjoint.connectedBody = hookedLoad.GetComponent<Rigidbody>();
            hookFree = false;
        }
    }
    private void ReleaseHook()
    {
        m_fixedjoint.connectedBody = null; //not working
        hookFree = true;
    }
}
