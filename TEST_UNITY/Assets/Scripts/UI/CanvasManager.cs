using UnityEngine;

public class CanvasManager : SingletonScene<CanvasManager>
{
	public enum CanvasType
    {
        LOAD,
        RESULTS
    }

    [SerializeField]
    private CanvasBase[] m_canvas;

    public CanvasBase GetCanvas(CanvasType type)
    {
        return m_canvas[(int)type];
    }
}
