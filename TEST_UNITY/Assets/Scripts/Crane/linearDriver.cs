using UnityEngine;

public class linearDriver : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    };

    public Axis myAxis = Axis.Y;
	public Transform objectJoint;
    public KeyCode keyPos = KeyCode.UpArrow;
    public KeyCode keyNeg = KeyCode.DownArrow;

    private Vector3 m_bodyInitialPos = Vector3.zero;
	private float m_input = 0;
	private string m_driveType = "velo";
    private float m_speed = 1f;
    
    linearDriver()
    {
        Init();
	}

    private void Start()
    {
        m_bodyInitialPos = objectJoint.localPosition;
    }

    void Update ()
    {
        if (Input.GetKey(keyPos))
            m_input = 1f;
        else if (Input.GetKey(keyNeg))
            m_input = -1f;
        else
            m_input = 0f;

        float objCoord = 0;
        float bodyCoord = 0f;
        bool positive = true;
        if (myAxis == Axis.X)
        {
            objCoord = objectJoint.localPosition.x;
            bodyCoord = m_bodyInitialPos.x;
            positive = true;
        }
        else if(myAxis == Axis.Y)
        {
            objCoord = objectJoint.localPosition.y;
            bodyCoord = m_bodyInitialPos.y;
            positive = false;
        }
        else
        {
            objCoord = objectJoint.localPosition.z;
            bodyCoord = m_bodyInitialPos.z;
            positive = true;
        }

        if ((positive ? (objCoord < bodyCoord) : (objCoord > bodyCoord)) && (positive ? (m_input < 0) : (m_input > 0)))
            m_input = 0f;

        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();
		if(m_input != 0)
        {
            float val = m_input * m_speed;
            joint.targetVelocity = new Vector3(myAxis == Axis.X ? val : 0, myAxis == Axis.Y ? val : 0, myAxis == Axis.Z ? val : 0);
            JointDrive drive;
            if (myAxis == Axis.X)
                drive = joint.xDrive;
            else if (myAxis == Axis.Y)
                drive = joint.yDrive;
            else
                drive = joint.zDrive;
            drive.positionSpring = 0;
			drive.positionDamper = 500000;

            if (myAxis == Axis.X)
            {
                joint.xDrive = drive;
            }
            else if (myAxis == Axis.Y)
            {
                joint.yDrive = drive;
            }
            else
            {
                joint.zDrive = drive;
            }
            m_driveType = "velo";
		}

		if(Mathf.Abs(m_input) == 0 && m_driveType == "velo")
        {
			m_driveType = "pos";
			joint.targetPosition = objectJoint.localPosition - m_bodyInitialPos;
			joint.targetVelocity = Vector3.zero;
            JointDrive drive;
            if (myAxis == Axis.X)
                drive = joint.xDrive;
            else if (myAxis == Axis.Y)
                drive = joint.yDrive;
            else
                drive = joint.zDrive;
            drive.positionSpring = 500000;
			drive.positionDamper = 0;
            if (myAxis == Axis.X)
            {
                joint.xDrive = drive;
            }
            else if (myAxis == Axis.Y)
            {
                joint.yDrive = drive;
            }
            else
            {
                joint.zDrive = drive;
            }
        }

        if(false)
            m_speed = (1 / (Time.deltaTime * m_input));
	}

    void Init()
    {
        m_bodyInitialPos = Vector3.zero;
        m_input = 0;
        m_driveType = "velo";
    }
}
