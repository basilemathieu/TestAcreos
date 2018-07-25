using UnityEngine;

public class CableRenderer : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    private LineRenderer m_lineRenderer = null;

    public Material CableMaterial = null;

    public float LineWidth = 0.03f;

    private void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();

        if (m_lineRenderer == null)
            m_lineRenderer = gameObject.AddComponent<LineRenderer>();

        m_lineRenderer.material = CableMaterial;
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.SetWidth(LineWidth, LineWidth);
    }

    private void Update()
    {
        m_lineRenderer.SetPosition(0, StartPoint.position);
        m_lineRenderer.SetPosition(1, EndPoint.position);
    }
}
