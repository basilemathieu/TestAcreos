using UnityEngine;

[ExecuteInEditMode]
public class JointToModel : MonoBehaviour
{
    public Transform linkedModel = null;

    void FixedUpdate()
    {
        if (linkedModel)
        {
            linkedModel.position = transform.position;
            linkedModel.rotation = transform.rotation;
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (!Application.isPlaying)
        {
            FixedUpdate();
        }
    }
#endif
}