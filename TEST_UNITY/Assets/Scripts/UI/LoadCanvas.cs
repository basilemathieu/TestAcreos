using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class LoadCanvas : CanvasBase
{
    [SerializeField]
    private GameObject m_UILoadPrefab;
    private GameObject m_UILoad;
    [SerializeField]
    private Color m_backgroundColorOn;
    [SerializeField]
    private Color m_backgroundColorOff;
    [SerializeField]
    private Color m_textColorOn;
    [SerializeField]
    private Color m_textColorOff;
    private List<Text> m_labels = new List<Text>();
    private Dictionary<Load, GameObject> m_UILoads = new Dictionary<Load, GameObject>();
	public void AddLoad(Load load)
    {

        m_UILoad = Instantiate(m_UILoadPrefab,this.transform);
        foreach (Text label in m_UILoad.GetComponentsInChildren<Text>()) // List of the prefab text fields
        {
            m_labels.Add(label);
        }

        m_labels[0].text = load.LoadName; //fill Name fied
        m_labels[2].text = load.LoadMass.ToString();// fill mass field
        m_labels.Clear();
        m_UILoads.Add(load, m_UILoad);

    }
    public void HighLightLoad(Load load, bool value)
    {
  
            m_UILoads[load].GetComponent<Image>().color = value? m_backgroundColorOn : m_backgroundColorOff;
            foreach (Text label in m_UILoads[load].GetComponentsInChildren<Text>()) // List of the  text fields
            {
                label.color = value ? m_textColorOn : m_textColorOff;
            }
        
    }
}
