using UnityEngine;

public class angularDriver : MonoBehaviour
{
	public Transform objectJoint;
    public KeyCode keyPos = KeyCode.LeftArrow;
    public KeyCode keyNeg = KeyCode.RightArrow;

    private float m_input = 0;
	private string m_driveType = "velo";
	private float m_offset = 0;
    private float m_speed = 1f;

    angularDriver()
    {
        Init();
    }
				
    void Update ()
    {
        if (Input.GetKey(keyNeg))
            m_input = -1f;
        else if (Input.GetKey(keyPos))
            m_input = 1f;
        else
            m_input = 0f;

        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();

        if (Mathf.Abs(m_input) > 0)
        {
			joint.targetAngularVelocity = new Vector3(0f, -m_input * m_speed, 0f);
			JointDrive drive = joint.angularYZDrive;
			drive.positionSpring = 0;
			drive.positionDamper = 500000; //force pour atteindre la vitesse
			joint.angularYZDrive = drive;	
            m_driveType = "velo";
		}

        if (Mathf.Abs(m_input) == 0 && m_driveType == "velo")
        {
            m_driveType = "pos";
			joint.targetRotation = objectJoint.transform.localRotation;
			joint.targetAngularVelocity = Vector3.zero;
			JointDrive drive = joint.angularYZDrive;
			drive.positionSpring = 500000; //force pour fixer la position
			drive.positionDamper = 0;
			joint.angularYZDrive = drive;
		}

        if(false)
            m_speed = (1 / (Time.deltaTime * m_input));
	}

    void Init()
    {
        m_input = 0;
        m_driveType = "velo";
    }
}
