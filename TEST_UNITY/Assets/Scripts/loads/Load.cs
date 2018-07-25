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
    [SerializeField]
    private Transform m_dropZoneTarget;
    private HookDriver m_hook;
    private bool m_hookInRange = false;
    private bool m_hookInRangePrevValue;
    private Renderer m_transparentModel;

    public string LoadName
    {
        get
        {
            return m_loadName;
        }

        private set
        {
            m_loadName = value;
        }
    }

    public float LoadMass
    {
        get
        {
            return m_loadMass;
        }

        private set
        {
            m_loadMass = value;
        }
    }

    private void Start()
    {
        FindObjectOfType<LoadCanvas>().AddLoad(this);
        m_hook = FindObjectOfType<HookDriver>();
       // GetComponentInChildren<Renderer>().material.color = new Color(1, 1, 1, 0.3f);
    }

    private void Update()
    {
        m_hookInRange = Vector3.Distance(this.transform.position, m_hook.transform.position) < m_hookMaxRange; //checking the range
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

    public void CreateDropZone()
    {
        foreach(Renderer model in GetComponentsInChildren<Renderer>())
        {
            m_transparentModel = Instantiate(model.gameObject, m_dropZoneTarget, false).GetComponent<Renderer>();
            m_transparentModel.material.shader= Shader.Find("Transparent/Diffuse");
            m_transparentModel.material.color = new Color(1, 1, 1, 0.5f);
        }
    }
    public void ClearDropZone()
    {
        foreach (Renderer model in m_dropZoneTarget.GetComponentsInChildren<Renderer>())
        {
            Destroy(model.gameObject);
        }
    }

}





